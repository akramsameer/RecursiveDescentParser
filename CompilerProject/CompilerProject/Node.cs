using System.Collections.Generic;

namespace CompilerProject
{
    class Node
    {
        public string Text { get; set; }
        public bool Error { get; set; }
        public List<Node> Nodes { get; set; }

        public Node()
        {
            Error = false;
            Nodes = new List<Node>();
            Text = "??";
        }
    }
}