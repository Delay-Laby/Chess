using System;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Net.Sockets;
using System.Net;

namespace ChessGame
{   //offline сначала номер игрока, затем номер фигуры, 0 - пустая клетка 
    public partial class Chess : Form
    {
        public static string path = @"..\..\img\";
        public static string skins = "alpha";
        public static Color W = Color.White;
        public static Color B = Color.CadetBlue;
        public bool isMoved = false;
        public Button prevButton;
        public Image chessSprites;
        public int[,] map;
        public Button[,] buttons = new Button[8, 8];
        public int currPlayer;

        public Chess()
        {
            InitializeComponent();







            Init();
        }
        public void Init()
        {

            map = new int[8, 8]
        {
            { 15,14,13,12,11,13,14,15},
            { 16,16,16,16,16,16,16,16},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0},
            { 26,26,26,26,26,26,26,26},
            { 25,24,23,22,21,23,24,25},

        };
            currPlayer = 1;
            CreateMap();

        }

        public void CreateMap()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    buttons[i, j] = new Button();

                    Button button = new Button();
                    button.Size = new Size(80, 80);
                    button.Location = new Point(j * 80, i * 80);

                    switch (map[i, j] / 10)
                    {
                        case 1:
                            if (map[i, j] % 10 == 1)
                            {
                                Image part = new Bitmap(path + skins + "\\white_king.png");
                                button.BackgroundImage = part;

                            }
                            if (map[i, j] % 10 == 2)
                            {
                                Image part = new Bitmap(path + skins + "\\white_queen.png");
                                button.BackgroundImage = part;

                            }
                            if (map[i, j] % 10 == 3)
                            {
                                Image part = new Bitmap(path + skins + "\\white_bishop.png");
                                button.BackgroundImage = part;

                            }
                            if (map[i, j] % 10 == 4)
                            {
                                Image part = new Bitmap(path + skins + "\\white_knight.png");
                                button.BackgroundImage = part;

                            }
                            if (map[i, j] % 10 == 5)
                            {
                                Image part = new Bitmap(path + skins + "\\white_rock.png");
                                button.BackgroundImage = part;

                            }
                            if (map[i, j] % 10 == 6)
                            {
                                Image part = new Bitmap(path + skins + "\\white_pawn.png");
                                button.BackgroundImage = part;

                            }
                            break;


                        case 2:
                            if (map[i, j] % 10 == 1)
                            {
                                Image partb = new Bitmap(path + skins + "\\black_king.png");
                                button.BackgroundImage = partb;

                            }
                            if (map[i, j] % 10 == 2)
                            {
                                Image partb = new Bitmap(path + skins + "\\black_queen.png");
                                button.BackgroundImage = partb;

                            }
                            if (map[i, j] % 10 == 3)
                            {
                                Image partb = new Bitmap(path + skins + "\\black_bishop.png");
                                button.BackgroundImage = partb;

                            }
                            if (map[i, j] % 10 == 4)
                            {
                                Image partb = new Bitmap(path + skins + "\\black_knight.png");
                                button.BackgroundImage = partb;

                            }
                            if (map[i, j] % 10 == 5)
                            {
                                Image partb = new Bitmap(path + skins + "\\black_rock.png");
                                button.BackgroundImage = partb;

                            }
                            if (map[i, j] % 10 == 6)
                            {
                                Image partb = new Bitmap(path + skins + "\\black_pawn.png");
                                button.BackgroundImage = partb;

                            }
                            break;
                    }
                    if ((i + j) % 2 == 0)
                        button.BackColor = W;
                    else
                        button.BackColor = B;
                    button.Click += new EventHandler(OnFigurePress);
                    this.Controls.Add(button);
                    buttons[i, j] = button;
                }

            }


        }

        public void OnFigurePress(Object sender, EventArgs e)
        {


            Button pressButton = sender as Button;


            if (prevButton != null)
                RedrawColorBoard();

            if (map[pressButton.Location.Y / 80, pressButton.Location.X / 80] != 0 && map[pressButton.Location.Y / 80, pressButton.Location.X / 80] / 10 == currPlayer)
            {
                RedrawColorBoard();
                pressButton.BackColor = Color.Red;
                DeactivateAllButtons();
                pressButton.Enabled = true;
                ShowSteps(pressButton.Location.Y / 80, pressButton.Location.X / 80, map[pressButton.Location.Y / 80, pressButton.Location.X / 80]);

                if (isMoved)
                {
                    RedrawColorBoard();

                    ActivateAllButtons();
                    isMoved = false;

                }
                else
                    isMoved = true;

            }
            else
            {
                if (isMoved)
                {

                    int temp = map[pressButton.Location.Y / 80, pressButton.Location.X / 80];
                    if (temp == 21 || temp == 11)
                        Win();
                    if (map[prevButton.Location.Y / 80, prevButton.Location.X / 80] % 10 == 6 && (pressButton.Location.Y / 80 == 7 || pressButton.Location.Y / 80 == 0))
                        PawnUp(prevButton.Location.Y / 80, prevButton.Location.X / 80);

                    map[pressButton.Location.Y / 80, pressButton.Location.X / 80] = map[prevButton.Location.Y / 80, prevButton.Location.X / 80];

                    map[prevButton.Location.Y / 80, prevButton.Location.X / 80] = 0;

                    pressButton.BackgroundImage = prevButton.BackgroundImage;
                    prevButton.BackgroundImage = null;

                    ActivateAllButtons();
                    RedrawColorBoard();
                    SwichPlayer();
                    isMoved = false;

                }
            }

            prevButton = pressButton;

        }
        public void SwichPlayer()
        {
            if (currPlayer == 1)
                currPlayer = 2;
            else currPlayer = 1;
        }

        private void Restart(object sender, EventArgs e)
        {
            this.Controls.Clear();
            Init();
            InitializeComponent();
        }

        public void DeactivateAllButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    buttons[i, j].Enabled = false;

                }
            }
        }

        public void ActivateAllButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    buttons[i, j].Enabled = true;

                }
            }
        }

        public void RedrawColorBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                        buttons[i, j].BackColor = W;
                    else
                        buttons[i, j].BackColor = B;

                }

            }

        }


        public void ShowSteps(int IcurrFigure, int JcurrFigure, int currFigure)
        {
            int dir = currPlayer == 1 ? 1 : -1;
            switch (currFigure % 10)
            {
                case 6:

                    if (InsideBorder(IcurrFigure + 1 * dir, JcurrFigure))
                    {
                        if (map[IcurrFigure + 1 * dir, JcurrFigure] == 0)
                        {
                            buttons[IcurrFigure + 1 * dir, JcurrFigure].BackColor = Color.Yellow;
                            buttons[IcurrFigure + 1 * dir, JcurrFigure].Enabled = true;

                            if (InsideBorder(IcurrFigure + 2 * dir, JcurrFigure) && map[IcurrFigure + 2 * dir, JcurrFigure] == 0)
                            {

                                if (dir == -1 && IcurrFigure == 6)
                                {
                                    buttons[IcurrFigure + 2 * dir, JcurrFigure].BackColor = Color.Yellow;
                                    buttons[IcurrFigure + 2 * dir, JcurrFigure].Enabled = true;
                                }
                                if (dir == 1 && IcurrFigure == 1)
                                {
                                    buttons[IcurrFigure + 2 * dir, JcurrFigure].BackColor = Color.Yellow;
                                    buttons[IcurrFigure + 2 * dir, JcurrFigure].Enabled = true;
                                }

                            }
                        }


                    }



                    if (InsideBorder(IcurrFigure + 1 * dir, JcurrFigure + 1))
                    {
                        if (map[IcurrFigure + 1 * dir, JcurrFigure + 1] != 0 && map[IcurrFigure + 1 * dir, JcurrFigure + 1] / 10 != currPlayer)
                        {
                            buttons[IcurrFigure + 1 * dir, JcurrFigure + 1].BackColor = Color.Yellow;
                            buttons[IcurrFigure + 1 * dir, JcurrFigure + 1].Enabled = true;
                        }
                    }
                    if (InsideBorder(IcurrFigure + 1 * dir, JcurrFigure - 1))
                    {
                        if (map[IcurrFigure + 1 * dir, JcurrFigure - 1] != 0 && map[IcurrFigure + 1 * dir, JcurrFigure - 1] / 10 != currPlayer)
                        {
                            buttons[IcurrFigure + 1 * dir, JcurrFigure - 1].BackColor = Color.Yellow;
                            buttons[IcurrFigure + 1 * dir, JcurrFigure - 1].Enabled = true;
                        }
                    }
                    break;
                case 5:
                    ShowVerticalHorizontal(IcurrFigure, JcurrFigure);
                    break;
                case 3:
                    ShowDiagonal(IcurrFigure, JcurrFigure);
                    break;
                case 2:
                    ShowVerticalHorizontal(IcurrFigure, JcurrFigure);
                    ShowDiagonal(IcurrFigure, JcurrFigure);
                    break;
                case 1:
                    ShowVerticalHorizontal(IcurrFigure, JcurrFigure, true);
                    ShowDiagonal(IcurrFigure, JcurrFigure, true);
                    break;
                case 4:
                    ShowHorseSteps(IcurrFigure, JcurrFigure);
                    break;




            }
        }

        public bool InsideBorder(int ti, int tj)
        {
            if (ti >= 8 || tj >= 8 || ti < 0 || tj < 0)
                return false;
            return true;
        }
        public void ShowHorseSteps(int IcurrFigure, int JcurrFigure)
        {

            if (InsideBorder(IcurrFigure - 2, JcurrFigure + 1))
            {
                DeterminePath(IcurrFigure - 2, JcurrFigure + 1);
            }
            if (InsideBorder(IcurrFigure - 2, JcurrFigure - 1))
            {
                DeterminePath(IcurrFigure - 2, JcurrFigure - 1);
            }
            if (InsideBorder(IcurrFigure + 2, JcurrFigure + 1))
            {
                DeterminePath(IcurrFigure + 2, JcurrFigure + 1);
            }
            if (InsideBorder(IcurrFigure + 2, JcurrFigure - 1))
            {
                DeterminePath(IcurrFigure + 2, JcurrFigure - 1);
            }
            if (InsideBorder(IcurrFigure - 1, JcurrFigure + 2))
            {
                DeterminePath(IcurrFigure - 1, JcurrFigure + 2);
            }
            if (InsideBorder(IcurrFigure + 1, JcurrFigure + 2))
            {
                DeterminePath(IcurrFigure + 1, JcurrFigure + 2);
            }
            if (InsideBorder(IcurrFigure - 1, JcurrFigure - 2))
            {
                DeterminePath(IcurrFigure - 1, JcurrFigure - 2);
            }
            if (InsideBorder(IcurrFigure + 1, JcurrFigure - 2))
            {
                DeterminePath(IcurrFigure + 1, JcurrFigure - 2);
            }
        }
        public bool DeterminePath(int IcurrFigure, int j)
        {
            if (map[IcurrFigure, j] == 0)
            {
                buttons[IcurrFigure, j].BackColor = Color.Yellow;
                buttons[IcurrFigure, j].Enabled = true;
            }
            else
            {
                if (map[IcurrFigure, j] / 10 != currPlayer)
                {
                    buttons[IcurrFigure, j].BackColor = Color.Yellow;
                    buttons[IcurrFigure, j].Enabled = true;
                }
                return false;
            }
            return true;
        }

        public void ShowDiagonal(int IcurrFigure, int JcurrFigure, bool isOneStep = false)
        {
            int j = JcurrFigure + 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (InsideBorder(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (InsideBorder(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (InsideBorder(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure + 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (InsideBorder(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }
        }

        public void ShowVerticalHorizontal(int IcurrFigure, int JcurrFigure, bool isOneStep = false)
        {
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (InsideBorder(i, JcurrFigure))
                {
                    if (!DeterminePath(i, JcurrFigure))
                        break;
                }
                if (isOneStep)
                    break;
            }
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (InsideBorder(i, JcurrFigure))
                {
                    if (!DeterminePath(i, JcurrFigure))
                        break;
                }
                if (isOneStep)
                    break;
            }
            for (int j = JcurrFigure + 1; j < 8; j++)
            {
                if (InsideBorder(IcurrFigure, j))
                {
                    if (!DeterminePath(IcurrFigure, j))
                        break;
                }
                if (isOneStep)
                    break;
            }
            for (int j = JcurrFigure - 1; j >= 0; j--)
            {
                if (InsideBorder(IcurrFigure, j))
                {
                    if (!DeterminePath(IcurrFigure, j))
                        break;
                }
                if (isOneStep)
                    break;
            }
        }


        public void Win()
        {
            MessageBox.Show("         Player " + currPlayer + " Win", "Win", MessageBoxButtons.OK);
            Controls.Clear();
            Init();
            InitializeComponent();
        }


        public void PawnUp(int Ifigure, int Jfigure)
        {
            Image part;
            map[Ifigure, Jfigure] -= 4;
            if (map[Ifigure, Jfigure] / 10 == 1)
                part = new Bitmap(path + skins + "\\white_queen.png");
            else
                part = new Bitmap(path + skins + "\\black_queen.png");

            buttons[Ifigure, Jfigure].BackgroundImage = part;
        }

        private void White(object sender, EventArgs e)
        {
            colorDialogC.ShowDialog();
            W = colorDialogC.Color;
            RedrawColorBoard();
        }

        private void Black(object sender, EventArgs e)
        {
            colorDialogC.ShowDialog();
            B = colorDialogC.Color;
            RedrawColorBoard();
        }



    }

}
