using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Lexer
{
  public  class StringContent
    {
        private int _row;
        private int _column;
        public string Input;
        private int _currentIndex;

        public StringContent(string input)
        {
            this.Input= input;
            _currentIndex = 0;
            _row = 1;
            _column = 0;
        }

        public Symbol NextSymbol()
        {
            if (_currentIndex >= Input.Length)
            {
                return new Symbol {Row = _row, Column = _column, CSymbol = '\0' };
            }
            var currentSym = new Symbol {Row = _row, Column = _column, CSymbol = Input[_currentIndex++]};
            if (currentSym.CSymbol.Equals('\n'))
            {
                _column = 0;
                _row += 1;
            }
            else
            {
                _column += 1;
            }
            return currentSym;
        }
    }
}
