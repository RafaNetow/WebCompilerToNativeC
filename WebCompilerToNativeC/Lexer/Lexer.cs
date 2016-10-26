using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Lexer
{
    public class Lexer
    {
        public StringContent Conent;
        private Symbol _currentSymbol;
        private bool CMode;
        private  ReserverdWords rWords = new ReserverdWords();

        public Lexer(StringContent content)
        {
            Conent = content;
            CMode = false;
            _currentSymbol = content.NextSymbol();
        }

        public void MapperToLexeme(Lexeme currentLexeme )
        {
            currentLexeme.Column = _currentSymbol.Column;
            currentLexeme.Row = _currentSymbol.Row;
            currentLexeme.Value += _currentSymbol.CSymbol;
            _currentSymbol = Conent.NextSymbol();

        }

        public Token MapperToTokenWithLexeme(TokenTypes token, Lexeme currentLexeme)
        {
            return new Token
            {
                Type = token,
                Lexeme = currentLexeme.Value,
                Column = currentLexeme.Column,
                Row = currentLexeme.Row
            };
        }
        public Token GetNextToken()
        {
            var state = 0;
            var currentLexeme = new Lexeme();

            while (true)
            {
                switch (state)
                {

                    case 0: 
                    if (_currentSymbol.CSymbol == '\0')
                        {
                            return new Token
                            {
                                Type = TokenTypes.Eof,
                                Column = currentLexeme.Column,
                                Row = currentLexeme.Row,
                                Lexeme = "$"
                            };
                        }
                    else if (char.IsLetter(_currentSymbol.CSymbol))
                    {
                        state = 1;
                       MapperToLexeme(currentLexeme);
                    }
                    else if (char.IsDigit(_currentSymbol.CSymbol))
                    {
                        state = 2;
                            MapperToLexeme(currentLexeme);

                        }
                    else if (_currentSymbol.CSymbol == '\"')
                    {
                        state = 3;
                            MapperToLexeme(currentLexeme);
                    }
                    else if (_currentSymbol.CSymbol == ':')
                    {
                       
                        state = 4;
                            MapperToLexeme(currentLexeme);
                        }
                    else if (rWords.Operators.ContainsKey(_currentSymbol.CSymbol.ToString()))
                    {
                        state = 5;
                         MapperToLexeme(currentLexeme);

                        }
                        break;

                    case 1:
                        if (char.IsLetterOrDigit(_currentSymbol.CSymbol))
                        {
                            state = 1;
                            MapperToLexeme(currentLexeme);
                        }
                        else if (rWords.ReserverWords.ContainsKey(currentLexeme.Value))
                        {
                            return new Token
                            {
                                Type = rWords.ReserverWords[currentLexeme.Value],
                                Lexeme = currentLexeme.Value,
                                Column = currentLexeme.Column,
                                Row = currentLexeme.Row
                            };
                        }
                        else
                        {
                            return new Token {Type = TokenTypes.Id, Lexeme = currentLexeme.Value,Column = currentLexeme.Column};
                        }
                
                        break;

                    case 2:
                        if (char.IsDigit(_currentSymbol.CSymbol))
                        {
                            state = 2;
                            MapperToLexeme(currentLexeme);
                          
                        }
                        else if (_currentSymbol.CSymbol == '.')
                        {
                            state = 9;
                            MapperToLexeme(currentLexeme);

                        }
                        else
                        {
                            return new Token { Type = TokenTypes.NumericalLiteral,
                                Lexeme = currentLexeme.Value,
                                Column = currentLexeme.Column,
                                Row = currentLexeme.Row
                            };
                        
                        }
                        break;

                         //State when is literal string
                    case 3:
                        if (_currentSymbol.CSymbol != '\"')
                        {
                            MapperToLexeme(currentLexeme);
                        }
                       else if (_currentSymbol.CSymbol == '\\')
                       {
                           state = 20;
                           MapperToLexeme(currentLexeme);
                        }
                       else if (_currentSymbol.CSymbol == '\"')
                       {
                           MapperToLexeme(currentLexeme);
                           return MapperToTokenWithLexeme(TokenTypes.StringLiteral, currentLexeme);
                       }

                        break;
                    case 4:
                        return MapperToTokenWithLexeme(TokenTypes.Common, currentLexeme);

                   //state to special operator and alone operators
                    case 5:
                        if (rWords.SpecialSymbols.Contains(currentLexeme.Value[0]))
                        {
                            MapperToLexeme(currentLexeme);

                            if (rWords.Operators.ContainsKey(currentLexeme.Value))
                            {
                                return MapperToTokenWithLexeme(rWords.SpecialOperators[currentLexeme.Value],
                                    currentLexeme);
                            }
                            return MapperToTokenWithLexeme(rWords.Operators[currentLexeme.Value[0].ToString()],
                                currentLexeme);
                        }
                        return MapperToTokenWithLexeme(rWords.Operators[currentLexeme.Value[0].ToString()],
                                currentLexeme);


                    //state when string have scape operator
                    case 20:
                        MapperToLexeme(currentLexeme);
                        state = 3;
                        break;
                        

                }
            }



            
        }

    }
}
