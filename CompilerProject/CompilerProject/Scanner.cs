using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerProject
{
    class Scanner
    {
        private string _cur;
        private int prevPos;
        private int pos;
        public string _text;
        private int _state;

        public List<Token> Tokens { get; set; }

        public Scanner()
        {
            pos = prevPos = 0;
            _state = 1;
            Tokens = new List<Token>();
        }

        public void Run()
        {
            while (true)
            {
                _cur = NextLexeme();
                switch (_state)
                {
                    case 1:

                        if (_cur == "if")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "If" });
                            _state = 3;
                        }
                        else if (_cur == "while")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "While" });
                            _state = 3;
                        }
                        else if (_cur == "{")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "{" });
                            _state = 5;
                        }
                        else if (IsId())
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "Id" });
                            _state = 2;
                        }
                        else
                            return;
                        break;
                    case 2:
                        if (_cur == "=")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "Equal Sign" });
                            _state = 6;
                        }
                        break;
                    case 6:
                        if (IsFactor())
                            _state = 7;
                        else if (_cur == "(")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "Open Branthes" });
                            _state = 6;
                        }
                        break;
                    case 7:
                        if (_cur == "*")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "*" });
                            _state = 8;
                        }
                        else if (_cur == "/")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "/" });
                            _state = 8;
                        }
                        else if (_cur == "+")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "+" });
                            _state = 8;
                        }
                        else if (_cur == "-")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "-" });
                            _state = 8;
                        }
                        else if (IsRelop())
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "Relational Operation" });
                            _state = 8;
                        }
                        else if (_cur == ")")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "Closed Branthesis" });
                            _state = 7;
                        }
                        else if (_cur == "")
                        {
                            _state = 9;
                        }
                        else if (_cur == "if")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "If" });
                            _state = 3;
                        }
                        else if (_cur == "while")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "While" });
                            _state = 3;
                        }
                        else if (_cur == "{")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "{" });
                            _state = 5;
                        }
                        else if (IsId())
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "Id" });
                            _state = 2;
                        }
                        break;
                    case 8:
                        if (IsFactor())
                        {
                            _state = 7;
                        }

                        break;
                    case 9:
                        if (_cur == ")")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "Closed Branthes" });
                            //DONE check here after finish 
                        }

                        _state = 10;
                        break;

                    case 3:
                        if (_cur == "(")
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "Open Branthes" });
                            _state = 6;
                        }
                        break;
                    case 10:
                        if (IsRelop())
                        {
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "Logical Operation" });
                            _state = 6;
                        }
                        if (_cur == ")")
                            Tokens.Add(new Token() { Text = _cur, StartPosition = prevPos, Type = "Closed Branthes" });
                        _state = 1;
                        break;
                    case 5:
                        _state = 1;
                        break;

                }





            }
        }



        private bool IsFactor()
        {

            if (Number())
            {
                Tokens.Add(new Token() { Text = _cur, Type = "Number", StartPosition = prevPos });
                return true;
            }
            else if (IsId())
            {
                Tokens.Add(new Token() { Text = _cur, Type = "Id", StartPosition = prevPos });
                return true;
            }
            return false;
        }

        private bool Number()
        {
            int x;
            double y;
            return int.TryParse(_cur, out x) || double.TryParse(_cur, out y);
        }

        private bool IsRelop()
        {
            return _cur == ">" || _cur == ">=" || _cur == "<" || _cur == "<=" || _cur == "==" || _cur == "!=";
        }

        public bool IsId(params string [] str)
        {
            if (str.Length > 0)
                _cur = str[0];

            if (_cur == "")
                return false;
            foreach (var c in _cur)
            {
                if (c >= 'a' && c <= 'z')
                    continue;
                else if (c >= 'A' && c <= 'Z')
                    continue;
                else if (c >= '0' && c <= '9')
                    continue;
                else if (c == '_')
                    continue;
                else
                    return false;
            }
            return true;
        }

        public string NextLexeme()
        {
            string ret = "";
            _cur = ret;

            while (pos < _text.Length && _text[pos] == ' ')
                pos++;

            prevPos = pos;  // saving the start of the word

            for (pos = prevPos; pos < _text.Length; pos++)
            {
                if (_text[pos] == ' ')
                    break;

                if ((ret == ">" || ret == "<") && (char.IsDigit(_text[pos]) || char.IsLetter(_text[pos])))
                    break;

                if ((_text[pos] == '(' || _text[pos] == ')' || _text[pos] == '+' || _text[pos] == '-' || _text[pos] == ';' || _text[pos] == '*' || _text[pos] == '/') && (ret == String.Empty))
                {
                    ret += _text[pos++];
                    break;
                }

                if ((ret == "!" || ret == "=" || ret == ">" || ret == "<" || ret == ""))
                {
                    if ((_text[pos] == '=' || _text[pos] == '>' || _text[pos] == '<' || _text[pos] == '!') && ret.Length < 2)
                    {
                        ret += _text[pos];
                        if (ret.Length == 2)
                        {
                            pos++;
                            break;
                        }
                        continue;
                    }

                    
                }

                if (_text[pos] == '(' || _text[pos] == ')' || _text[pos] == '+' || _text[pos] == '-' || _text[pos] == ';' || _text[pos] == '*' || _text[pos] == '/' || (_text[pos] == '!' || _text[pos] == '=' || _text[pos] == '>' || _text[pos] == '<'))
                    break;

                if ((ret == "!" || ret == "=" || ret == ">" || ret == "<"))
                    break;

                ret += _text[pos];
            }

            return ret;
        }
    }
}
