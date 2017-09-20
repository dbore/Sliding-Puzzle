using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sliding_Puzzle
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            cboBoard.Items.Add("8 Puzzle (3x3)");
            cboBoard.Items.Add("15 Puzzle (4x4)");
            cboBoard.Items.Add("24 Puzzle (5x5)");

            cboBoard.SelectedIndex = 0;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (cboBoard.Text != String.Empty)
            {

                //get the board size
                //and number of puzzles
                Point board_size = new Point();
                int number_of_pizzles = 0;
                switch (cboBoard.Text)
                {
                    case "8 Puzzle (3x3)":
                        number_of_pizzles = 8;
                        board_size = new Point(3, 3);
                        break;
                    case "15 Puzzle (4x4)":
                        number_of_pizzles = 15;
                        board_size = new Point(4, 4);
                        break;
                    case "24 Puzzle (5x5)":
                        number_of_pizzles = 24;
                        board_size = new Point(5, 5);
                        break;

                }

                

                Game game = new Game(number_of_pizzles, board_size);
                game.Show();
            }
        }
    }
}
