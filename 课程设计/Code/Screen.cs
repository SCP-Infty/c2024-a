using System;
using System.Windows.Forms;
using Core;

namespace Gomoku
{
    public partial class Screen : Form
    {
        public Screen()
        {
            AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            InitLayout_();

            KeyDown += new KeyEventHandler(ResetGame);

            game = new GomokuGame();

            int[] pos = game.AIPlacePawn();

            board[15 * pos[0] + pos[1]].SetImage((!game.GetPlayerColor()) ? 1 : -1);
        }

        public EventHandler ClickEventGenerator(int x, int y)
        {
            void Click(object sender, EventArgs e)
            {
                if (!game.GetPlayerTurn())
                {
                    return;
                }
                if (!board[15*x+y].GetAvailable())
                {
                    return;
                }
                int v = game.Judge();
                switch (v)
                {
                    case 0:
                        break;
                    case 1:
                        Text = "Black Wins";
                        MessageBox.Show("Black Wins!", "Game Over", MessageBoxButtons.OK);
                        break;
                    case -1:
                        Text = "White Wins";
                        MessageBox.Show("White Wins!", "Game Over", MessageBoxButtons.OK);
                        break;
                }
                if (v!=0)
                {
                    return;
                }
                board[15 * x + y].SetImage(game.GetPlayerColor() ? 1 : -1);
                //GC.Collect();
                game.PlacePawn(x, y);

                v = game.Judge();

                switch (v)
                {
                    case 0:
                        int[] pos = game.AIPlacePawn();
                        board[15 * pos[0] + pos[1]].SetImage((!game.GetPlayerColor()) ? 1 : -1);
                        int v2 = game.Judge();
                        switch (v2)
                        {
                            case 0:
                                break;
                            case 1:
                                Text = "Black Wins";
                                MessageBox.Show("Black Wins!", "Game Over", MessageBoxButtons.OK);
                                return;
                            case -1:
                                Text = "White Wins";
                                MessageBox.Show("White Wins!", "Game Over", MessageBoxButtons.OK);
                                return;
                        }
                        break;
                    case 1:
                        Text = "Black Wins";
                        MessageBox.Show("Black Wins!", "Game Over", MessageBoxButtons.OK);
                        break;
                    case -1:
                        Text = "White Wins";
                        MessageBox.Show("White Wins!", "Game Over", MessageBoxButtons.OK);
                        break;
                }
            }
            return new EventHandler(Click);
        }

        public void ResetGame(object sender, KeyEventArgs e)
        {
            Keys keys = e.KeyCode;
            if (keys == Keys.Escape)
            {
                game.ClearMap();
            }
        }

        GomokuGame game;
    }
}
