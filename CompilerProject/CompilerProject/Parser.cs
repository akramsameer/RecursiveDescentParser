using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerProject
{
    class Parser
    {
        private string _cur;
        private Scanner _scanner;
        private TreeView _treeView;
        private Node curNode;
        public Node root;

        public Parser(string input)
        {
            _scanner = new Scanner(){_text = input};
        }

        public void Run()
        {
           
            root = new Node();
            curNode = root;

            Program();
        }

        void Relop()
        {
            //DONE Display Relop
            curNode.Text = "Relop";
            Node method = curNode;

            if (_cur == ">" || _cur == ">" || _cur == "<" || _cur == "<=" || _cur == "==" || _cur == "!=")
            {
                //DONE Display it
                Node temp1 = new Node();
                method.Nodes.Add(temp1);
                curNode = temp1;
                temp1.Text = _cur;
                _cur = _scanner.NextLexeme();
            }
            else
            {
                //DONE : check Error
                var err = new Node
                {
                    Text = "Not Mathed with any rules",
                    Error = true
                };
                method.Nodes.Add(err);
            }
        }

        void Program()
        {
            //Done Display Program
            curNode.Text = "Program";

            Node temp = new Node();

            curNode.Nodes.Add(temp);

            curNode = temp;

            StmtList();
        }

        private void StmtList()
        {
            //Done Display StmtList
            curNode.Text = "Statment List";
            Node method = curNode;

            Node temp = new Node();
            method.Nodes.Add(temp);
            curNode = temp;

            _cur = _scanner.NextLexeme();
            while (_cur != "")
            {

                Stmt();
            }
        }

        private void Stmt()
        {
            //Done Display Stmt
            curNode.Text = "Statment";
            Node method = curNode;
            

           
            if (accept("if"))
            {
                _cur = _scanner.NextLexeme();
                if (accept("("))
                {
                    _cur = _scanner.NextLexeme();

                    Node temp = new Node();
                    method.Nodes.Add(temp);
                    curNode = temp;

                    LogicExpression();

                    if (accept(")"))
                    {
                        _cur = _scanner.NextLexeme();

                        Node temp1 = new Node();
                        method.Nodes.Add(temp1);
                        curNode = temp1;

                        Stmt();
                    }
                    else
                    {
                        //DONE error missed close breanthesis
                        var err = new Node
                        {
                            Text = "Missed Close Branthesis",
                            Error = true
                        };
                        method.Nodes.Add(err);
                    }
                }
                
                else
                {
                    //DONE : missed open branthesis
                    var err = new Node
                    {
                        Text = "Missed Open Branthesis",
                        Error = true
                    };
                    method.Nodes.Add(err);
                }
            }
            else if (accept("while"))
            {
                _cur = _scanner.NextLexeme();
                if (accept("("))
                {
                    _cur = _scanner.NextLexeme();

                    Node temp = new Node();
                    method.Nodes.Add(temp);
                    curNode = temp;

                    LogicExpression();
                    _cur = _scanner.NextLexeme();
                    if (accept(")"))
                    {
                        _cur = _scanner.NextLexeme();
                        Stmt();
                    }
                    else
                    {
                        //DONE: missed close branthsis
                        var err = new Node
                        {
                            Text = "Missed Close Branthesis",
                            Error = true
                        };
                        method.Nodes.Add(err);
                    }
                }
                else
                {
                    //DONE :  error missed open branthesis
                    var err = new Node
                    {
                        Text = "Missed Open Branthesis",
                        Error = true
                    };
                    method.Nodes.Add(err);
                }
            }
            else if (accept("{"))
            {
                _cur = _scanner.NextLexeme();

                Node temp = new Node();
                method.Nodes.Add(temp);
                curNode = temp;

                StmtList();

                _cur = _scanner.NextLexeme();
                if (accept("}"))
                {
                    _cur = _scanner.NextLexeme();
                }
                else
                {
                    //DONE : missed close brecket
                    var err = new Node
                    {
                        Text = "Missed Close Bracket",
                        Error = true
                    };
                    method.Nodes.Add(err);
                }
            }
            else if (_scanner.IsId(_cur))
            {
                Node temp3 = new Node();
                method.Nodes.Add(temp3);
                temp3.Text = "Identefier";

                Node temp4 = new Node();
                temp3.Nodes.Add(temp4);
                temp4.Text = _cur;

                _cur = _scanner.NextLexeme();
                if (accept("="))
                {
                    Node temp5 = new Node();
                    method.Nodes.Add(temp5);
                    temp5.Text = _cur;

                    _cur = _scanner.NextLexeme();

                    Node temp = new Node();
                    method.Nodes.Add(temp);
                    curNode = temp;

                    MathExpression();

                    _cur = _scanner.NextLexeme();
                    if (accept(";"))
                    {
                        _cur = _scanner.NextLexeme();
                    }
                    else
                    {
                        //DONE: error semicolon
                        var err = new Node
                        {
                            Text = "Missed Semi-Colon",
                            Error = true
                        };
                        method.Nodes.Add(err);

                    }
                }
                else
                {
                    //DONE: Error missed equal sign
                    var err = new Node
                    {
                        Text = "Missed Equal-Sign",
                        Error = true
                    };
                    method.Nodes.Add(err);

                }
            }
            else
            {
                //DONE :  error not matched with any rules
                var err = new Node
                {
                    Text = "Not Matched with any rules",
                    Error = true
                };
                method.Nodes.Add(err);
            }
        }

        private void LogicExpression()
        {
            //DONE Display Logic Expression
            curNode.Text = "Logic Expression";
            Node method = curNode;

            Node temp = new Node();
            method.Nodes.Add(temp);
            curNode = temp;

            MathExpression();
            

            Node temp1 = new Node();
            method.Nodes.Add(temp1);
            curNode = temp1;
            

            Relop();

            Node temp2 = new Node();
            method.Nodes.Add(temp2);
            curNode = temp2;
            MathExpression();
        }

        private void MathExpression()
        {
            //DONE Display MathExpression
            curNode.Text = "Math Expression";
            Node method = curNode;


            Node temp = new Node();
            method.Nodes.Add(temp);
            curNode = temp;

            Term();
            while (accept("+") || accept("-"))
            {
                Node temp1 = new Node();
                method.Nodes.Add(temp1);
                curNode = temp1;

                _cur = _scanner.NextLexeme();
                Term();
            }
        }

        private void Term()
        {
            //DONE Display Term
            curNode.Text = "Term";
            Node method = curNode;

            Node temp = new Node();
            method.Nodes.Add(temp);
            curNode = temp;

            Factor();
            while (accept("*") || accept("/"))
            {
                Node temp1 = new Node();
                method.Nodes.Add(temp1);
                curNode = temp1;

                _cur = _scanner.NextLexeme();
                Factor();

            }
        }

        private void Factor()
        {
            //DONE : Display factor in the tree 
            curNode.Text = "Factor";
            Node method = curNode;

            Node temp1 = new Node();
            
            curNode = temp1;
            method.Nodes.Add(curNode);

            if (Number())
            {
                _cur = _scanner.NextLexeme();

            }
            else if (_scanner.IsId(_cur))
            {
                
                //DONE : display it in the tree
                
                curNode.Text = "Identifier";
                

                Node temp3 = new Node();
                curNode.Nodes.Add(temp3);
                temp3.Text = _cur;
                _cur = _scanner.NextLexeme();
            }
            else if(accept("("))
            {
                _cur = _scanner.NextLexeme();

                Node temp = new Node();
                method.Nodes.Add(temp);
                curNode = temp;

                MathExpression();
                _cur = _scanner.NextLexeme();
                if (accept(")"))
                {
                    _cur = _scanner.NextLexeme();
                }
                else
                {
                    //DONE : error missing closing close branthesis
                    var err = new Node
                    {
                        Text = "Missed Closing Close Branthesis",
                        Error = true
                    };
                    method.Nodes.Add(err);
                }
            }
            else
            {
                //DONE : display error not mathched with any rules
                var err = new Node
                {
                    Text = "Not Mathed with any rules",
                    Error = true
                };
                method.Nodes.Add(err);
            }

        }

        private bool Number()
        {
            
            int x;
            double y;
            if (int.TryParse(_cur,out x))
            {
                //DONE : Display Number in the tree
                curNode.Text = "Number";
                Node method = curNode;

                //DONE : Display it in the tree
                Node temp1 = new Node();
                temp1.Text = "Integer";
                method.Nodes.Add(temp1);
               
                Node temp2 = new Node();
                temp2.Text = _cur;
                temp1.Nodes.Add(temp2);

                return true;
            }
            else if (double.TryParse(_cur ,out y))
            {
                //DONE : Display Number in the tree
                curNode.Text = "Number";
                Node method = curNode;

                //DONE : Display it in the tree
                Node temp1 = new Node();
                temp1.Text = "Double";
                method.Nodes.Add(temp1);

                Node temp2 = new Node();
                temp2.Text = _cur;
                temp1.Nodes.Add(temp2);

                return true;
            }

            return false;
        }

        private bool accept(string s)
        {
            if (_cur == s)
                return true;
            else
            {
                return false;
            }
        }
    }
}
