using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Configuration;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
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
        public bool OnlyHexInString(string test)
        { 
             return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"); 
       }

        public bool IsNumberOctal(string number)
        {
              return System.Text.RegularExpressions.Regex.IsMatch(number, @"\A\b^[1 - 7][0 - 7] *$");  /*^[1 - 7][0 - 7] *$*/
        }
    public void MapperToLexeme(Lexeme currentLexeme )
        {
            currentLexeme.Column = _currentSymbol.Column;
            currentLexeme.Row = _currentSymbol.Row;
            currentLexeme.Value += _currentSymbol.CSymbol;
            _currentSymbol = Conent.NextSymbol();

        }

        public void MoveRowAndColumnOfLexeme(Lexeme currentLexeme)
        {
            currentLexeme.Column = _currentSymbol.Column;
            currentLexeme.Row = _currentSymbol.Row;
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
            var state = 100;
            var currentLexeme = new Lexeme();

            if (CMode)
                state = 0;

            while (true)
            {
                switch (state)
                {

                    case 100:
                        if (_currentSymbol.CSymbol == '<')
                        {
                            MapperToLexeme(currentLexeme);
                            if (_currentSymbol.CSymbol == '%')
                            {
                                MapperToLexeme(currentLexeme);
                                CMode = true;
                                return MapperToTokenWithLexeme(TokenTypes.Html, currentLexeme);
                            }
                        }
                        else if (_currentSymbol.CSymbol == '\0')
                        {
                            return MapperToTokenWithLexeme(TokenTypes.Eof, currentLexeme);
                        }
                        else
                        {     
                                MapperToLexeme(currentLexeme);
                            
                        }
                        break;
          

                    case 0: 
                    if (_currentSymbol.CSymbol == '\0')
                        {
                            return new Token
                            {
                                Type = TokenTypes.Eof,
                                Column = _currentSymbol.Column,
                                Row = _currentSymbol.Row,
                                Lexeme = "$"
                            };
                        }
                        if (char.IsWhiteSpace(_currentSymbol.CSymbol))
                        {
                       
                            _currentSymbol = Conent.NextSymbol();
                        }
                       
                       else if (char.IsLetter(_currentSymbol.CSymbol) || _currentSymbol.CSymbol == '_')
                        {
                            state = 1;
                            MapperToLexeme(currentLexeme);
                        }
                        else if (char.IsDigit(_currentSymbol.CSymbol))
                        {
                            state = 2;
                            if (_currentSymbol.CSymbol == '0')
                            {//state to octal and hex
                                MapperToLexeme(currentLexeme);
                                state = 19;
                            }
                           

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
                        else if (_currentSymbol.CSymbol == '/')
                        {
                            
               
                            state = 11;
                            MapperToLexeme(currentLexeme);
                        }
                        else if (rWords.Operators.ContainsKey(_currentSymbol.CSymbol.ToString()))
                        {
                            state = 5;
                            MapperToLexeme(currentLexeme);

                        }
                       
                        else if (_currentSymbol.CSymbol == '#')
                        {
                            state = 14;
                            MapperToLexeme(currentLexeme);
                        }
                        else if (_currentSymbol.CSymbol == '\'')
                        {
                            state = 15;
                            MapperToLexeme(currentLexeme);
                        }
                        else if (_currentSymbol.CSymbol.Equals((char) 13))
                        {
                            _currentSymbol = Conent.NextSymbol();
                        }
                        
                        else
                        {
                            throw new LexerException(
                  $"Symbol {_currentSymbol.CSymbol} not recognized at Row:{_currentSymbol.Row} Col: {_currentSymbol.Column}");
                        }
                        break;

                    case 1:
                        if (char.IsLetterOrDigit(_currentSymbol.CSymbol) || _currentSymbol.CSymbol == '_')
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
                            return new Token {Type = TokenTypes.Id, Lexeme = currentLexeme.Value,Column = currentLexeme.Column, Row = currentLexeme.Row};
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
                        else if (_currentSymbol.CSymbol == 'e')
                        {
                            state = 16;
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
                        if (char.IsWhiteSpace(_currentSymbol.CSymbol) && !_currentSymbol.CSymbol.Equals((char)13))
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
                      else if (_currentSymbol.CSymbol.Equals((char) 13) || _currentSymbol.CSymbol == '\0')
                      {
                            throw new LexerException(
                               $"Symbol {_currentSymbol.CSymbol} not recognized at Row:{_currentSymbol.Row} Col: {_currentSymbol.Column}");
                        }
                       else
                       {
                            MapperToLexeme(currentLexeme);
                        }

                        break;
                    case 4:
                        return MapperToTokenWithLexeme(TokenTypes.Common, currentLexeme);

                   //state to special operator and alone operators
                    case 5:
                        if (rWords.SpecialSymbols.Contains(currentLexeme.Value[0]))
                        {
                          //  MapperToLexeme(currentLexeme);
                            currentLexeme.Value += _currentSymbol.CSymbol;
                            currentLexeme.Column = _currentSymbol.Column;
                            currentLexeme.Row = _currentSymbol.Row;

                            if (rWords.SpecialOperators.ContainsKey(currentLexeme.Value))
                            {
                                _currentSymbol = Conent.NextSymbol();
                                return MapperToTokenWithLexeme(rWords.SpecialOperators[currentLexeme.Value],
                                    currentLexeme);
                            }
                            if (currentLexeme.Value == "%>")
                            {
                                CMode = false;

                                state = 100;
                                break;
                            }
                            currentLexeme.Value = currentLexeme.Value[0].ToString();
                            return MapperToTokenWithLexeme(rWords.Operators[currentLexeme.Value[0].ToString()],
                                currentLexeme);
                        }
                        return MapperToTokenWithLexeme(rWords.Operators[currentLexeme.Value[0].ToString()],
                                currentLexeme);


                    //Cuando es EOF
                    case 6:
                        return MapperToTokenWithLexeme(TokenTypes.Eof, currentLexeme);


                       //when is decimal
                    case 7:
                        if (!char.IsDigit(_currentSymbol.CSymbol))
                             throw new LexerException($"Symbol {_currentSymbol.CSymbol} not recognized at Row:{_currentSymbol.Row} Col: {_currentSymbol.Column}");
                            state = 8;
                        MapperToLexeme(currentLexeme);
                        break;

                    //when decimal have more that one digit and could be float
                    case 8:
                        if (!char.IsDigit(_currentSymbol.CSymbol))
                            return MapperToTokenWithLexeme(TokenTypes.DecimalLiteral, currentLexeme);
                        if (_currentSymbol.ToString() == "e")
                        {
                            state = 9;
                            MapperToLexeme(currentLexeme);
                        }
                        else
                            MapperToLexeme(currentLexeme);
                        
                        break;

                  //when  is integer decimal
                    case 9:
                        if (char.IsDigit(_currentSymbol.CSymbol))
                        {
                            MapperToLexeme(currentLexeme);
                            // regex to float f(Regex.IsMatch(lexeme, @"^[-]?(0|[1-9][0-9]*)(\.[0-9]+)?([eE][+-]?[0-9]+)?$"))
                        }
                        else
                        {
                            if (Regex.IsMatch(currentLexeme.Value, @"[0-9]+(\.[0-9][0-9]?)?"))
                            {
                                return MapperToTokenWithLexeme(TokenTypes.DecimalLiteral, currentLexeme);
                            }
                            throw new LexerException($"Symbol {_currentSymbol.CSymbol} not recognized at Row:{_currentSymbol.Row} Col: {_currentSymbol.Column}");
                        }

                        //if (!char.IsDigit(_currentSymbol.CSymbol) && _currentSymbol.CSymbol.ToString() == "-")
                        //{
                        //    return MapperToTokenWithLexeme(TokenTypes.FloatLiteral, currentLexeme);
                        //}
                        //if (char.IsDigit(_currentSymbol.CSymbol))
                        //{
                        //    MapperToLexeme(currentLexeme);
                        //    return MapperToTokenWithLexeme(TokenTypes.FloatLiteral, currentLexeme);
                        //}
                      
                        break;
                    //When float have sub
                    case 10:
                        if (!char.IsDigit(_currentSymbol.CSymbol))
                            throw new LexerException(
                                $"Symbol {_currentSymbol.CSymbol} not recognized at Row:{_currentSymbol.Row} Col: {_currentSymbol.Column}");
                        MapperToLexeme(currentLexeme);
                        return MapperToTokenWithLexeme(TokenTypes.FloatLiteral, currentLexeme);

                    //Begin Of Comment
                    case 11:

                        if (_currentSymbol.CSymbol == '*')
                            
                        {
                            _currentSymbol = Conent.NextSymbol();
                            state = 12;
                        }
                        else if (rWords.Operators.ContainsKey(currentLexeme.Value[0].ToString()))
                        {
                            return MapperToTokenWithLexeme(rWords.Operators[currentLexeme.Value], currentLexeme);
                        }

                        else
                        {
                            throw new LexerException(
                                $"Symbol {_currentSymbol.CSymbol} not recognized at Row:{_currentSymbol.Row} Col: {_currentSymbol.Column}");
                        }
                        break;
                        
                    //when code is comented
                    case 12:
                        if (char.IsWhiteSpace(_currentSymbol.CSymbol))
                        {

                            _currentSymbol = Conent.NextSymbol();
                        }
                        else if (_currentSymbol.CSymbol == '*')
                        {
                            state = 13;
                            _currentSymbol = Conent.NextSymbol();
                        }
                          
                        else
                        _currentSymbol = Conent.NextSymbol();
                        break;


                     //when comment could be closed
                    case 13:
                        if (char.IsWhiteSpace(_currentSymbol.CSymbol))
                        {

                            _currentSymbol = Conent.NextSymbol();
                        }
                        else  if (_currentSymbol.CSymbol == '/')
                        {
                            state = 0;
                            _currentSymbol = Conent.NextSymbol();
                            currentLexeme.Value = "";
                        }
                        else if (_currentSymbol.CSymbol == '\0')
                        {
                            throw new LexerException(
                              $"Symbol {_currentSymbol.CSymbol} not recognized at Row:{_currentSymbol.Row} Col: {_currentSymbol.Column}");
                        }
                        else { 
                        _currentSymbol = Conent.NextSymbol();
                        state = 12;
                        }
                        break;

                    //When could be to include 
                    case 14:
                        if (char.IsLetterOrDigit(_currentSymbol.CSymbol) || _currentSymbol.CSymbol =='-')
                        {
                            MapperToLexeme(currentLexeme);
                        }
                        else if (_currentSymbol.CSymbol == '#')
                        {
                            state = 18;
                            _currentSymbol = Conent.NextSymbol();

                        }
                        else
                        {
                            if (rWords.ReserverWords.ContainsKey(currentLexeme.Value))
                                return MapperToTokenWithLexeme(TokenTypes.RwInclude, currentLexeme);
                            throw new LexerException(
                            $"Symbol {_currentSymbol.CSymbol} not recognized at Row:{_currentSymbol.Row} Col: {_currentSymbol.Column}");

                        }
                        break;
                        //Cuando tendria que venir un char ;
                    case 15:
                        if (_currentSymbol.CSymbol != '\'')
                        {
                            MapperToLexeme(currentLexeme);
                        }
                        else
                        {
                            MapperToLexeme(currentLexeme);
                            if (currentLexeme.Value.Length == 3)
                            {
                               return  MapperToTokenWithLexeme(TokenTypes.CharLiteral, currentLexeme);
                            }
                            if (currentLexeme.Value[1] == '\\')
                            {
                                return MapperToTokenWithLexeme(TokenTypes.CharLiteral, currentLexeme);
                            }
                            throw new LexerException(
                                $"Symbol {_currentSymbol.CSymbol} not recognized at Row:{_currentSymbol.Row} Col: {_currentSymbol.Column}");
                        }
                        break;

                    case 16:

                        //when literal is float

                        if (_currentSymbol.CSymbol == '-')
                        {
                            MapperToLexeme(currentLexeme);
                            state = 17;
                        }
                     else  if (char.IsDigit(_currentSymbol.CSymbol))
                    {
                        MapperToLexeme(currentLexeme);
                        return MapperToTokenWithLexeme(TokenTypes.FloatLiteral, currentLexeme);
                        }
                        else {

                        return MapperToTokenWithLexeme(TokenTypes.FloatLiteral, currentLexeme);
                        }
                        break;
                    case 17:
                        if (char.IsDigit(_currentSymbol.CSymbol))
                        {
                            MapperToLexeme(currentLexeme);
                            return MapperToTokenWithLexeme(TokenTypes.FloatLiteral, currentLexeme);
                        }
                        throw new LexerException(
                            $"Symbol {_currentSymbol.CSymbol} not recognized at Row:{_currentSymbol.Row} Col: {_currentSymbol.Column}");

                    case 18:
                       var stringOfDate = currentLexeme.Value.Remove(0,1);
                        string[] list =stringOfDate.Split('-');

                        try
                        {
                            DateTime value = new DateTime(Convert.ToInt32(list[2]), Convert.ToInt32(list[1]),
                                Convert.ToInt32(list[0]));
                            currentLexeme.Value += "#";
                            return MapperToTokenWithLexeme(TokenTypes.DateLiteral, currentLexeme);
                        }
                        catch (Exception e)
                        {
                            throw new LexerException(
                                $"Symbol {_currentSymbol.CSymbol} bad format:{_currentSymbol.Row} Col: {_currentSymbol.Column}");
                        }
                    //   string date[] = currentLexeme.

                    //BugHexadecimal
                    case 19:
                        if (_currentSymbol.CSymbol == 'x')
                        {
                            state = 21;
                            MapperToLexeme(currentLexeme);
                        }else if (char.IsDigit(_currentSymbol.CSymbol))
                        {
                            MapperToLexeme(currentLexeme);
                        }
                        else
                        {
                            
                            return (currentLexeme.Value.Length == 1) ? MapperToTokenWithLexeme(TokenTypes.NumericalLiteral,currentLexeme) : MapperToTokenWithLexeme(TokenTypes.OctalLietral, currentLexeme);
                        }



                        break;
                    case 21:
                        if (char.IsLetterOrDigit(_currentSymbol.CSymbol))
                        {
                            MapperToLexeme(currentLexeme);
                        }
                        else
                        {
                            if (OnlyHexInString(currentLexeme.Value))
                            {
                                return MapperToTokenWithLexeme(TokenTypes.HexadecimalLiteral, currentLexeme);

                            }
                            throw new LexerException(
                                $"A number hex doesnt support the symbol {_currentSymbol.CSymbol}  Row:{_currentSymbol.Row} Col: {_currentSymbol.Column}");
                        }
                        break;
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
