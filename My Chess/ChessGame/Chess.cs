using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace ChessGame
{   //сначала номер игрока, затем номер фигуры, 0 - пустая клетка 
    public partial class Chess : Form
    {
        public static string path = @"..\..\img\";
        public static string skins = "alpha";
        public static Color W = Color.White;
        public static Color B = Color.CadetBlue;
        public bool isMove = false;
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

                if (isMove)
                {
                    RedrawColorBoard();

                    ActivateAllButtons();
                    isMove = false;

                }
                else
                    isMove = true;

            }
            else
            {
                if (isMove)
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
                    isMove = false;

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

        //ОНЛАЙН
        
        private Socket socket;
        public Chess(string ip, int port)
        {
            InitializeComponent();
            connectServer(ip, port);
            InitOn();
        }
        public Chess(int port)
        {
            InitializeComponent();
            createServer(port);
            InitOn();
        }

        bool CheckWin = false;
        Place[,] Gamespace = new Place[8, 8]; //Игровое поле

        public void InitOn()
        {
            for (int x = 0; x < 8; x++) //Растановка фигур на поле
            {

                for (int y = 0; y < 8; y++)
                {

                    buttons[x, y] = new Button();

                    Button button = new Button();
                    button.Size = new Size(80, 80);
                    button.Location = new Point(x * 80, y * 80);

                    if (Gamespace[x, y].isBlack != true)
                    {
                        if (Gamespace[x, y].type == ChessType.King)
                        {
                            Image part = new Bitmap(path + skins + "\\white_king.png");
                            button.BackgroundImage = part;

                        }
                        if (Gamespace[x, y].type == ChessType.Queen)
                        {
                            Image part = new Bitmap(path + skins + "\\white_queen.png");
                            button.BackgroundImage = part;

                        }
                        if (Gamespace[x, y].type == ChessType.Bishop)
                        {
                            Image part = new Bitmap(path + skins + "\\white_bishop.png");
                            button.BackgroundImage = part;

                        }
                        if (Gamespace[x, y].type == ChessType.Knight)
                        {
                            Image part = new Bitmap(path + skins + "\\white_knight.png");
                            button.BackgroundImage = part;

                        }
                        if (Gamespace[x, y].type == ChessType.Rock)
                        {
                            Image part = new Bitmap(path + skins + "\\white_rock.png");
                            button.BackgroundImage = part;

                        }
                        if (Gamespace[x, y].type == ChessType.Pawn)
                        {
                            Image part = new Bitmap(path + skins + "\\white_pawn.png");
                            button.BackgroundImage = part;

                        }
                    }


                        else
                            if (Gamespace[x, y].type == ChessType.King)
                            {
                                Image partb = new Bitmap(path + skins + "\\black_king.png");
                                button.BackgroundImage = partb;

                            }
                            if (Gamespace[x, y].type == ChessType.Queen)
                            {
                                Image partb = new Bitmap(path + skins + "\\black_queen.png");
                                button.BackgroundImage = partb;

                            }
                            if (Gamespace[x, y].type == ChessType.Bishop)
                            {
                                Image partb = new Bitmap(path + skins + "\\black_bishop.png");
                                button.BackgroundImage = partb;

                            }
                            if (Gamespace[x, y].type == ChessType.Knight)
                            {
                                Image partb = new Bitmap(path + skins + "\\black_knight.png");
                                button.BackgroundImage = partb;

                            }
                            if (Gamespace[x, y].type == ChessType.Rock)
                            {
                                Image partb = new Bitmap(path + skins + "\\black_rock.png");
                                button.BackgroundImage = partb;

                            }
                            if (Gamespace[x, y].type == ChessType.Pawn)
                            {
                                Image partb = new Bitmap(path + skins + "\\black_pawn.png");
                                button.BackgroundImage = partb;

                            }
                    if ((x+y) % 2 == 0)
                        button.BackColor = W;
                    else
                        button.BackColor = B;
                    button.Click += new EventHandler(FigurePress);
                    this.Controls.Add(button);
                    buttons[x, y] = button;
                }


                        

            }
        }

        public void FigurePress(Object sender, EventArgs e)
        {
            Button pressButton = sender as Button;
            if (Gamespace[pressButton.Location.Y / 80, pressButton.Location.X / 80].type != ChessType.Empety && pressButton.Location.Y / 80 < 8 && pressButton.Location.Y / 80 >= 0)
            {
                RedrawColorBoard();
                pressButton.BackColor = Color.Red;
                DeactivateAllButtons();
                pressButton.Enabled = true;
                for (int x = 0; x < 8; x++) //Растановка фигур на поле
                {

                    for (int y = 0; y < 8; y++)
                    {
                     if( canMove(Gamespace, pressButton.Location.X / 80, pressButton.Location.Y / 80, x , y))
                            buttons[x,y].Enabled = true;
                            buttons[x, y].BackColor = Color.Yellow; 

                    }
                }
                if (isMove)
                {
                    RedrawColorBoard();

                    ActivateAllButtons();
                    isMove = false;

                }
                else
                    isMove = true;
            }
            else
            {
                if (isMove)
                {

                    moveTo(Gamespace, prevButton.Location.X / 80, prevButton.Location.Y / 80, pressButton.Location.X / 80, pressButton.Location.Y / 80, Gamespace[pressButton.Location.X / 80, pressButton.Location.Y / 80].isBlack);
                   
                    pressButton.BackgroundImage = prevButton.BackgroundImage;
                    prevButton.BackgroundImage = null;

                    ActivateAllButtons();
                    RedrawColorBoard();
                    
                    isMove = false;
                    InGame(Gamespace, true, false, prevButton.Location.X / 80, prevButton.Location.Y / 80, pressButton.Location.X / 80, pressButton.Location.Y / 80, socket);
                    
                }
            }

            prevButton = pressButton;



        }
            bool canMove(Place[,] gs, int x1, int y1, int x2, int y2)
        {
            if (x1 < 8 && y1 < 8 && x1 > 0 && y1 > 0 && x2 < 8 && y2 < 8 && x2 > 0 && y2 > 0)
            {
                //Общая проверка
                if (gs[x1, y1].type != ChessType.Empety && (gs[x2, y2].type == ChessType.Empety || gs[x2, y2].isBlack != gs[x1, y1].isBlack))
                {
                    //Проверка для пешки
                    if (gs[x1, y1].type == ChessType.Pawn && (gs[x2, y2].type == ChessType.Empety && x1 == x2 && (!gs[x1, y1].isMoved && ((gs[x1, y1].isBlack && ((y1 - y2 == -2 && gs[x1, y1 - 1].type == ChessType.Empety) || y1 - y2 == -1)) || (!gs[x1, x2].isBlack && ((y1 - y2 == 2 && gs[x1, y1 + 1].type == ChessType.Empety) || y1 - y2 == 1))) || (gs[x1, y1].isMoved && (gs[x1, y1].isBlack && y1 - y2 == -1) || (!gs[x1, x2].isBlack && y1 - y2 == 1)))) || (gs[x2, y2].type != ChessType.Empety && (gs[x1, y1].isBlack && Math.Abs(x1 - x2) == 1 && y1 - y2 == -1) || ((!gs[x1, y1].isBlack && Math.Abs(x1 - x2) == 1 && y1 - y2 == 1)))) { return true; }
                    //Проверка для слона
                    if (gs[x1, y1].type == ChessType.Bishop && x1 - x2 != 0 && y1 - y2 != 0 && ((Math.Abs(x1 - x2) == 1 && Math.Abs(y1 - y2) == 1) || (Math.Abs(x1 - x2) == Math.Abs(y1 - y2))))
                    {
                        if (x2 > x1 && y2 > y1) { for (int x = x1; x < x2; x++) { for (int y = y1; y < y2; y++) { if (gs[x, y].type != ChessType.Empety) { return false; } } } }
                        if (x2 > x1 && y2 < y1) { for (int x = x1; x < x2; x++) { for (int y = y1; y > y2; y--) { if (gs[x, y].type != ChessType.Empety) { return false; } } } }
                        if (x2 < x1 && y2 < y1) { for (int x = x1; x > x2; x--) { for (int y = y1; y > y2; y--) { if (gs[x, y].type != ChessType.Empety) { return false; } } } }
                        if (x2 < x1 && y2 > y1) { for (int x = x1; x > x2; x--) { for (int y = y1; y < y2; y++) { if (gs[x, y].type != ChessType.Empety) { return false; } } } }
                        return true;
                    }
                    //Проверка для коня
                    if (gs[x1, x2].type == ChessType.Knight && ((Math.Abs(x1 - x2) == 3 && Math.Abs(y1 - y2) == 2) || (Math.Abs(x1 - x2) == 3 && Math.Abs(y1 - y2) == 2))) { return true; }
                    //Проверка для ладьи
                    if (gs[x1, x2].type == ChessType.Rock && ((x1 - x2 == 0 && y1 - y2 != 0) || (x1 - x2 != 0 && y1 - y2 == 0)))
                    {
                        if (x2 > x1 && y1 == y2) { for (int x = x1; x < x2; x++) { if (gs[x, y1].type != ChessType.Empety) { return false; } } }
                        if (x2 < x1 && y1 == y2) { for (int x = x1; x > x2; x--) { if (gs[x, y1].type != ChessType.Empety) { return false; } } }
                        if (y2 > y1 && x1 == x2) { for (int y = y1; y < y2; y++) { if (gs[x1, y].type != ChessType.Empety) { return false; } } }
                        if (y2 > y1 && x1 == x2) { for (int y = y1; y < y2; y++) { if (gs[x1, y].type != ChessType.Empety) { return false; } } }
                        return true;
                    }
                    //Для королевы
                    if (gs[x1, x2].type == ChessType.Queen && (x1 - x2 != 0 && y1 - y2 != 0 && ((Math.Abs(x1 - x2) == 1 && Math.Abs(y1 - y2) == 1) || (Math.Abs(x1 - x2) == Math.Abs(y1 - y2)))) && ((x1 - x2 == 0 && y1 - y2 != 0) || (x1 - x2 != 0 && y1 - y2 == 0)))
                    {
                        if (x2 > x1 && y2 > y1) { for (int x = x1; x < x2; x++) { for (int y = y1; y < y2; y++) { if (gs[x, y].type != ChessType.Empety) { return false; } } } }
                        if (x2 > x1 && y2 < y1) { for (int x = x1; x < x2; x++) { for (int y = y1; y > y2; y--) { if (gs[x, y].type != ChessType.Empety) { return false; } } } }
                        if (x2 < x1 && y2 < y1) { for (int x = x1; x > x2; x--) { for (int y = y1; y > y2; y--) { if (gs[x, y].type != ChessType.Empety) { return false; } } } }
                        if (x2 < x1 && y2 > y1) { for (int x = x1; x > x2; x--) { for (int y = y1; y < y2; y++) { if (gs[x, y].type != ChessType.Empety) { return false; } } } }
                        if (x2 > x1 && y1 == y2) { for (int x = x1; x < x2; x++) { if (gs[x, y1].type != ChessType.Empety) { return false; } } }
                        if (x2 < x1 && y1 == y2) { for (int x = x1; x > x2; x--) { if (gs[x, y1].type != ChessType.Empety) { return false; } } }
                        if (y2 > y1 && x1 == x2) { for (int y = y1; y < y2; y++) { if (gs[x1, y].type != ChessType.Empety) { return false; } } }
                        if (y2 > y1 && x1 == x2) { for (int y = y1; y < y2; y++) { if (gs[x1, y].type != ChessType.Empety) { return false; } } }
                        return true;
                    }
                    //Для короля

                }
                if (gs[x1, y1].type == ChessType.King && ((Math.Abs(x1 - x2) == 1 || Math.Abs(y1 - y2) == 1 && (gs[x2, y2].type == ChessType.Empety || gs[x2, y2].isBlack != gs[x1, y1].isBlack)) || (gs[x2, y2].isBlack = gs[x2, y2].isBlack && gs[x2, y2].type == ChessType.Rock && !gs[x2, y2].isMoved && !gs[x1, y1].isMoved))) { return true; }
                return false;
            }
            else
                return false;
        }

        bool isCheck(bool isBlack, Place[,] gs)
        {
            //Координаты короля
            int xk = 9, yk = 9;
            //Поиск короля цвета isBlack
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (gs[x, y].type == ChessType.King && gs[x, y].isBlack == isBlack)
                    {
                        xk = x;
                        yk = y;
                        break;

                    }
                }
            }
            //Проверка гипотетического состояния на наличие состояния шаха
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (gs[x, y].type != ChessType.Empety && gs[x, y].isBlack != isBlack && canMove(gs, x, y, xk, yk)) { return true; }
                }
            }
            return false;
        }


        void InGame(Place[,] Gamespace, bool isClient, bool gameEnd, int x1, int y1, int x2, int y2, Socket socket)
        {

            if (!gameEnd)
            {
                Place[,] newGS;
                int[] coordinates = new int[4];
                if (isCheck(isClient, Gamespace))
                {
                    gameEnd = true; // временная смена переменной для проверки на возможно выйти из проигрошного положения
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            if (Gamespace[x, y].isBlack == isClient)
                            {
                                for (int xt = 0; xt < 8; xt++)
                                {
                                    for (int yt = 0; yt < 8; yt++)
                                    {
                                        newGS = moveTo(Gamespace, x, y, xt, yt, isClient);
                                        if (newGS != Gamespace) { gameEnd = false; }
                                    }
                                }
                            }
                        }
                    }
                }




                coordinates[0] = x1;
                coordinates[1] = y1;
                coordinates[2] = x2;
                coordinates[3] = y2;
                newGS = moveTo(Gamespace, coordinates[0], coordinates[1], coordinates[2], coordinates[3], isClient);
                Gamespace = newGS;
                for (int i = 0; i < coordinates.Length; i++)
                {
                    byte[] buffer = BitConverter.GetBytes(coordinates[i]);
                    socket.Send(buffer);

                }

                for (int i = 0; i < coordinates.Length; i++)
                {
                    byte[] buffer = new byte[1024];
                    socket.Receive(buffer);
                    coordinates[i] = BitConverter.ToInt32(buffer, 0);
                    if (coordinates[0] == 50) { finalGame(socket, isClient); } //Костыль для проверки остановки игры если противник проиграл
                    Gamespace = moveTo(Gamespace, coordinates[0], coordinates[1], coordinates[2], coordinates[3], isClient);
                }

            }
            else { finalGame(isClient); }
        }



        Place[,] moveTo(Place[,] gs, int x1, int y1, int x2, int y2, bool isBlack) //Перемещение если ход возможен и если новое положение не вызывет шаха
        {
            Place[,] newGS = gs;
            if (gs[x2, y2].type == ChessType.Rock && gs[x2, y2].isBlack == gs[x1, y1].isBlack && gs[x1, y1].type == ChessType.King) //Рокировка
            {
                if (x2 == 0 && gs[1, y1].type == ChessType.Empety && gs[2, y1].type == ChessType.Empety && gs[3, y1].type == ChessType.Empety)
                {
                    newGS[2, y1].type = ChessType.King;
                    newGS[2, y1].isMoved = true;
                    newGS[3, y1].type = ChessType.Bishop;
                    newGS[3, y1].isMoved = true;
                    newGS[4, y1].type = ChessType.Empety;
                    newGS[0, y1].type = ChessType.Empety;
                }
                if (x2 == 0 && gs[5, y1].type == ChessType.Empety && gs[6, y1].type == ChessType.Empety)
                {
                    newGS[6, y1].type = ChessType.King;
                    newGS[6, y1].isMoved = true;
                    newGS[5, y1].type = ChessType.Bishop;
                    newGS[5, y1].isMoved = true;
                    newGS[7, y1].type = ChessType.Empety;
                    newGS[4, y1].type = ChessType.Empety;
                }

            }
            else
            {
                newGS[x2, y2] = gs[x1, x2];
                newGS[x2, y2].isMoved = true;
                newGS[x1, y1].type = ChessType.Empety;
            }
            if (canMove(gs, x1, y1, x2, y2) && !isCheck(isBlack, newGS))
            {
                return newGS;
            }
            return gs;
        }

        public void connectServer(String ip, int port) //Подключение через ip и соккет (для локалки использовать localhost
        {

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ip, port);
            StartGame(true, socket);
        }

        public void createServer(int port) //Создание сервера и перевод в его режим ожидания(максимум 2 клиента что бы избавиться от багов при плохом соединении)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen(2);
            socket.Accept();
            StartGame(false, socket);
        }

        public void finalGame(Socket socket, bool looser)
        {
            byte[] buffer = BitConverter.GetBytes(50); //Отправка значения 50(несуществуещей координаты) для сообщения о поражении(костыль)
            socket.Send(buffer);
            MessageBox.Show("         Player " + " Win", "Win", MessageBoxButtons.OK);
            socket.Close();
            //доделать финал здесь
        }
        public void finalGame(bool looser)
        {
            if (CheckWin)
                MessageBox.Show("         Player " + " Win", "Win", MessageBoxButtons.OK);
            CheckWin = true;
        }







        public void StartGame(bool isClient, Socket socket) //Начало игры
        {
            for (int x = 0; x < 8; x++) //Растановка фигур на поле
            {
                bool isBlack;
                for (int y = 0; y < 8; y++)
                {
                    if (y < 4) { isBlack = false; } else { isBlack = true; }
                    if (y == 1 || y == 6) { Gamespace[x, y] = new Place(ChessType.Pawn, isBlack); }
                    else if (y == 0 || y == 7)
                    {
                        if (x == 0 || x == 7) { Gamespace[x, y] = new Place(ChessType.Rock, isBlack); }
                        if (x == 1 || x == 6) { Gamespace[x, y] = new Place(ChessType.Knight, isBlack); }
                        if (x == 2 || x == 5) { Gamespace[x, y] = new Place(ChessType.Bishop, isBlack); }
                        if (x == 3) { Gamespace[x, y] = new Place(ChessType.Queen, isBlack); }
                        if (x == 4) { Gamespace[x, y] = new Place(ChessType.King, isBlack); }
                    }
                    else { Gamespace[x, y] = new Place(ChessType.Empety, isBlack); }
                }
            }

            bool looser = !isClient;
            int[] coordinates = new int[4];
            if (isClient)
            {
                for (int i = 0; i < coordinates.Length; i++)
                {
                    byte[] buffer = new byte[1024];
                    socket.Receive(buffer);
                    coordinates[i] = BitConverter.ToInt32(buffer, 0);
                    if (coordinates[0] == 50) { finalGame(socket, looser); } //Костыль для проверки остановки игры если противник проиграл
                    Gamespace = moveTo(Gamespace, coordinates[0], coordinates[1], coordinates[2], coordinates[3], isClient);
                }
            }
        }



        public void StartGame(bool isClient, int x1, int y1, int x2, int y2) //Начало игры
        {
            //init
            Restartion();

            bool myTurn = !isClient; //Текущих ход, начинает всегда сервер(белые)
            bool gameEnd = false;
            bool looser = !isClient;
            while (!gameEnd) //Основной игровой цикл
            {
                if (isCheck(isClient, Gamespace))//Проверка условия поржаения игрока
                {
                    gameEnd = true; // временная смена переменной для проверки на возможно выйти из проигрошного положения
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            if (Gamespace[x, y].isBlack == isClient)
                            {
                                for (int xt = 0; xt < 8; xt++)
                                {
                                    for (int yt = 0; yt < 8; yt++)
                                    {
                                        Place[,] newGS = moveTo(Gamespace, x, y, xt, yt, isClient);
                                        if (newGS != Gamespace) { gameEnd = false; }
                                    }
                                }
                            }
                        }
                        if (gameEnd)
                        {
                            CheckWin = true;
                            break;
                        }
                    }
                    int[] coordinates = new int[4];
                    if (!isClient)
                    {
                        Place[,] newGS;//Задавать координаты здесь в coordinates, цикл будет идти пока не появится ход который изменит поле
                        do
                        {
                            coordinates[0] = x1;//x1
                            coordinates[1] = x2;//y1
                            coordinates[2] = y1;//x2   Доделать здесь
                            coordinates[3] = y2;//y2
                            newGS = moveTo(Gamespace, coordinates[0], coordinates[1], coordinates[2], coordinates[3], isClient);
                        } while (newGS == Gamespace);
                        Gamespace = newGS;

                    }
                    else
                    {
                        for (int i = 0; i < coordinates.Length; i++)
                        {

                            Place[,] newGS;//Задавать координаты здесь в coordinates, цикл будет идти пока не появится ход который изменит поле
                            do
                            {
                                coordinates[0] = x1;//x1
                                coordinates[1] = y1;//y1
                                coordinates[2] = x2;//x2   Доделать здесь
                                coordinates[3] = y2;//y2
                                newGS = moveTo(Gamespace, coordinates[0], coordinates[1], coordinates[2], coordinates[3], isClient);
                            } while (newGS == Gamespace);
                            Gamespace = newGS;
                        }
                    }



                    myTurn = !myTurn;
                }
                if (CheckWin)
                    finalGame(looser);
            }
        }


        void Restartion()
        {
            for (int x = 0; x < 8; x++) //Растановка фигур на поле
            {
                bool isBlack;
                for (int y = 0; y < 8; y++)
                {
                    if (y < 4) { isBlack = false; } else { isBlack = true; }
                    if (y == 1 || y == 6) { Gamespace[x, y] = new Place(ChessType.Pawn, isBlack); }
                    else if (y == 0 || y == 7)
                    {
                        if (x == 0 || x == 7) { Gamespace[x, y] = new Place(ChessType.Rock, isBlack); }
                        if (x == 1 || x == 6) { Gamespace[x, y] = new Place(ChessType.Knight, isBlack); }
                        if (x == 2 || x == 5) { Gamespace[x, y] = new Place(ChessType.Bishop, isBlack); }
                        if (x == 3) { Gamespace[x, y] = new Place(ChessType.Queen, isBlack); }
                        if (x == 4) { Gamespace[x, y] = new Place(ChessType.King, isBlack); }
                    }
                    else { Gamespace[x, y] = new Place(ChessType.Empety, isBlack); }
                }
            }
        }

        enum ChessType
        {
            Empety,
            Pawn,
            King,
            Queen,
            Knight,
            Rock,
            Bishop

        }


        class Place
        {
            public ChessType type;
            public bool isBlack;
            public bool isMoved = false;

            public Place(ChessType chessType, bool isBlack)
            {
                this.type = chessType;
                this.isBlack = isBlack;
            }
        }












    }
}
