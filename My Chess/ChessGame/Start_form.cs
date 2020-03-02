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
    public partial class Start_form : Form
    {
        public Start_form()
        {
            InitializeComponent();
        }

        private void Setting(object sender, EventArgs e)
        {
            Setting_form Setting = new Setting_form();
            Setting.Show();
            
        }

        private void Start(object sender, EventArgs e)
        {
            Hide();
          
            Chess Chess = new Chess();
            
            Chess.ShowDialog();
            Show();
           
        }

        private void Onlinegame(object sender, EventArgs e)
        {
            Connect Ch = new Connect();
            Ch.Show();
            
        }
    }
}
