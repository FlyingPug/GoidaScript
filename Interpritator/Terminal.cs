using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpritator
{
    public class Terminal : Token
    {
        public enum TerminalType
        {
            Lambda = 0,
            Empty,
            Number,
            Integer,
            Boolean,
            String,
            Identificator,
            OpenBracket,
            OpenSquareBracket,
            OpenFigureBracket,
            CloseBracket,
            Equal,
            AdditionOperation,
            MultiplieOperation,
            CompareOperaion,
            OrOperation,
            AndOperation,
            NotOperation,
            IfStatement,
            WhileStatement,
            ImbededFunction,
            Comma,
            Line
        }

        public Terminal(TerminalType _type)
        {
            this.Type = _type;
        }

        public readonly string Value = "";
        public readonly TerminalType Type;
    }
}
