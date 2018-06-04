using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CompilerProject
{
    public partial class Form1 : Form
    {
        public string Result { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            grdView.Rows.Clear();

            Parser parser = new Parser(txtInput.Text);
            parser.Run();
            TreeNode root = new TreeNode()
            {
                Text = "Parse Tree"
            };
            dfs(parser.root, 2, root);
            treeView1.Nodes.Add(root);
            Scanner scanner = new Scanner() { _text = txtInput.Text };
            scanner.Run();
            foreach (var token in scanner.Tokens)
            {
                string[] x = { token.Text, token.Type, token.StartPosition.ToString() };
                grdView.Rows.Add(x);
            }
        }


        void dfs(Node cur, int tab, TreeNode p)
        {
            //lst.Items.Add(cur.Text);
            for (int i = 0; i < tab; i++)
                Result += "-";
            Result += "> " + cur.Text + "\n";

            TreeNode n = new TreeNode()
            {
                Text = cur.Text
            };
            p.Nodes.Add(n);
            foreach (var node in cur.Nodes)
            {
                dfs(node, tab + 2, n);
            }
        }


        private void grdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }


}
