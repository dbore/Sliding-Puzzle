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
    public partial class Game : Form
    {

        private int number_of_puzzles;
        private Point board_size;
        private int[] arr_of_numbers;
        private int[,] game_board;
        private int nullBlock;
      
        private Random rnd;

        private int x = 10;
        private int y = 10;
        private int end_x;
        private int end_y;

        private int puzzle_width = 100;
        private int puzzle_height = 100;
        private int space = 0;

        private bool game_over = false;


        public Game(int number_of_puzzles, Point board_size)
        {
            InitializeComponent();
            this.number_of_puzzles = number_of_puzzles;
            this.board_size = board_size;

            rnd = new Random();

            arr_of_numbers = new int[number_of_puzzles];
            game_board = new int[board_size.X, board_size.Y];

            end_x = 10 + ((board_size.X - 1) * puzzle_width) + ((board_size.X - 1) * space);
            end_y = 10 + ((board_size.Y - 1) * puzzle_height) + ((board_size.Y - 1) * space);
            createArray();
            randomizeArray();
            setNullBlock();
            createGameBoard();
            //printGameBoard();
            makeInterface();
 
        }

        public void createArray()
        {
            for (int i = 0; i < number_of_puzzles; i++)
            {
                arr_of_numbers[i] = i + 1;
            }

        }
        public void randomizeArray()
        {
            arr_of_numbers = arr_of_numbers.OrderBy(x => rnd.Next()).ToArray();  
        }
        public void setNullBlock()
        {
            nullBlock = rnd.Next(0, number_of_puzzles + 1);
        }

        public void createGameBoard()
        {
            int index = 0;
            int count = 0;
            for (int i = 0; i < board_size.X; i++)
            {
                for (int j = 0; j < board_size.Y; j++)
                {
                    if (count != nullBlock)
                    {
                        game_board[i, j] = arr_of_numbers[index];
                        index++;
                    }
                    else
                    {
                        game_board[i, j] = 0;
                    }
                    count++;
                }
            }


        }

        public void printGameBoard()
        {
            for (int i = 0; i < board_size.X; i++)
            {
                for (int j = 0; j < board_size.Y; j++)
                {
                    Console.Write(game_board[i, j] + " ");
                }
                Console.WriteLine();

            }

            Console.WriteLine("--------------------------");


        }
        public void makeInterface()
        {
            //resize the window to fit the board
            this.Width = (board_size.X * puzzle_width) + (board_size.X * space) + 30;
            this.Height = (board_size.Y * puzzle_height) + (board_size.Y * space) + 50;

            //draw the puzzle
            Font f = new Font(FontFamily.GenericSansSerif, 16.0F, FontStyle.Bold);
   
            for (int i = 0; i < board_size.X; i++)
            {
                for (int j = 0; j < board_size.Y; j++)
                {

                    int number = game_board[i, j];

                    if (number != 0)
                    {
                        //create the puzzle
                        Button puzzle = new Button();
                        puzzle.FlatStyle = FlatStyle.Flat;
                        puzzle.Width = puzzle_width;
                        puzzle.Height = puzzle_height;
                        puzzle.BackColor = Color.FromArgb(255, 0, 128, 255);
                        puzzle.ForeColor = Color.White;
                        puzzle.Font = f;
                        puzzle.Text = number.ToString();
                        puzzle.Location = new Point(x, y);
                        puzzle.Click += new EventHandler(puzzle_Click);
                        this.Controls.Add(puzzle);
                        puzzle.BringToFront();
  
                    }
           
                    //update coordinates
                    x = x + puzzle_width + space;
                   
                }

                //update coordinates
                x = 10;
                y = y + puzzle_height + space;


            }
          
        }

        public void drawLastPuzzle()
        {
            Font f = new Font(FontFamily.GenericSansSerif, 16.0F, FontStyle.Bold);

            Button z = new Button();
            z.FlatStyle = FlatStyle.Flat;
            z.Width = puzzle_width;
            z.Height = puzzle_height;
            z.BackColor = Color.FromArgb(255, 0, 128, 255);
            z.ForeColor = Color.White;
            z.Font = f;
            z.Location = new Point(end_x, end_y);
            
            this.Controls.Add(z);
            z.BringToFront();

            MessageBox.Show("Game Over!" + Environment.NewLine + "Congratulations!!!","Game Over",MessageBoxButtons.OK,MessageBoxIcon.Information);


        }

      public void puzzle_Click(Object sender, EventArgs e)
      {
      


          if (!game_over)
          {
              //get the x and y of clicked button
              //the button text is unique so this can help
              Button b = (Button)sender;
              int number = int.Parse(b.Text);
              Point b_cord = getButtonXandY(number);

              //check if b can move left, right, up, down
              if (b_cord.Y - 1 >= 0 && game_board[b_cord.X, b_cord.Y - 1] == 0)
              {
                  //can move left
                  game_board[b_cord.X, b_cord.Y] = 0;
                  game_board[b_cord.X, b_cord.Y - 1] = number;
                  //printGameBoard();

                  //change location
                  b.Location = new Point(b.Location.X - (puzzle_width + space), b.Location.Y);

                  //check game over
                  if (check_game_over())
                  {
                      game_over = true;
                      drawLastPuzzle();

                
                  }




              }
              else if (b_cord.Y + 1 <= board_size.Y - 1 && game_board[b_cord.X, b_cord.Y + 1] == 0)
              {
                  //can move right
                  game_board[b_cord.X, b_cord.Y] = 0;
                  game_board[b_cord.X, b_cord.Y + 1] = number;
                  //printGameBoard();

                  //change location
                  b.Location = new Point(b.Location.X + (puzzle_width + space), b.Location.Y);

                  //check game over
                  if (check_game_over())
                  {
                      game_over = true;
                      drawLastPuzzle();
                  
           
                  }


              }
              else if (b_cord.X - 1 >= 0 && game_board[b_cord.X - 1, b_cord.Y] == 0)
              {
                  //can move up
                  game_board[b_cord.X, b_cord.Y] = 0;
                  game_board[b_cord.X - 1, b_cord.Y] = number;
                  //printGameBoard();

                  //change location
                  b.Location = new Point(b.Location.X, b.Location.Y - (puzzle_height + space));

                  //check game over
                  if (check_game_over())
                  {
                      game_over = true;
                      drawLastPuzzle();
                  
                   
                  }


              }
              else if (b_cord.X + 1 <= board_size.X - 1 && game_board[b_cord.X + 1, b_cord.Y] == 0)
              {
                  //can move down
                  game_board[b_cord.X, b_cord.Y] = 0;
                  game_board[b_cord.X + 1, b_cord.Y] = number;
                  //printGameBoard();

                  //change location
                  b.Location = new Point(b.Location.X, b.Location.Y + (puzzle_height + space));

                  //check game over
                  if (check_game_over())
                  {
                      game_over = true;
                      drawLastPuzzle();
                      
                     
                  }


              }
          }


      }

        public Point getButtonXandY(int number){
            Point p = new Point();
                for (int i = 0; i < board_size.X; i++)
                {
                     for (int j = 0; j < board_size.Y; j++)
                    {
                         if(game_board[i,j] == number){
                             p.X = i;
                             p.Y = j;
                             
                         }

                    }
                }

            return p;

        }

        public bool check_game_over()
        {
            int start = 1;
            for (int i = 0; i < board_size.X; i++)
            {
                for (int j = 0; j < board_size.Y; j++)
                {

                    if (i == (board_size.X - 1) && j == (board_size.Y - 1))
                        start = 0;

                    //check in order
                    if (game_board[i, j] == start)
                    {
                        start++;
                    }
                    else
                    {
                        return false;
                    }


                }

            }

            return true;
        }
      
    }
}
