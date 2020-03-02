using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessGame;


namespace ChessGame
{
    public partial class Setting_form : Form
    {
        public Setting_form()
        {
            InitializeComponent();
        }

        private void White(object sender, EventArgs e)
        {
            CD.ShowDialog();
            Chess.W = CD.Color;
            
        }

        private void Black(object sender, EventArgs e)
        {
            CD.ShowDialog();
            Chess.B = CD.Color;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowser.SelectedPath;
                string folder = new System.IO.DirectoryInfo(path).Name;

                Chess.skins = folder;

            }
        }
    }
}
