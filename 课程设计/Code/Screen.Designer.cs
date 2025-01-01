using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Gomoku
{
    class Tile : System.Windows.Forms.Label
    {
        public Tile(int type) : base()
        {
            this.type = type;
        }

        public void SetType(int type)
        {
            this.type = type;
        }

        public bool GetAvailable()
        {
            return pawn == 0;
        }

        public void SetImage(int pawn)
        {
            string imageName, imageType = "Cross";
            this.pawn = pawn;

            switch (pawn)
            {
                case 1:
                    imageName = "black";
                    break;
                case -1:
                    imageName = "white";
                    break;
                default:
                    imageName = "empty";
                    break;
            }

            switch (type)
            {
                case 0:
                    imageType = "Cross";
                    break;
                case 1:
                    imageType = "Dot";
                    break;
                case 2:
                    imageType = "Top";
                    break;
                case 3:
                    imageType = "Right";
                    break;
                case 4:
                    imageType = "Bottom";
                    break;
                case 5:
                    imageType = "Left";
                    break;
                case 6:
                    imageType = "TopLeft";
                    break;
                case 7:
                    imageType = "TopRight";
                    break;
                case 8:
                    imageType = "BottomRight";
                    break;
                case 9:
                    imageType = "BottomLeft";
                    break;
            }

            this.Image = System.Drawing.Image.FromFile(string.Format(".\\assets\\textures\\{0:S}\\{1:S}.png", imageName, imageType));
        }

        private int type;
        private int pawn;
    }

    public partial class Screen
    {
        private System.ComponentModel.IContainer components = null;

        /// <param name="dpisposing">如果应释放托管资源，为 true；否则为 false。</param>
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 900);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        private void InitLayout_()
        {
            this.SuspendLayout();

            Text = "Gomoke!";

            Icon = new System.Drawing.Icon(".\\assets\\icon.ico");

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Tile tile = new Tile(0);
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            tile.SetType(6);
                        }
                        else if (j == 14)
                        {
                            tile.SetType(9);
                        }
                        else
                        {
                            tile.SetType(5);
                        }
                    }
                    else if (i == 14)
                    {
                        if (j == 0)
                        {
                            tile.SetType(7);
                        }
                        else if (j == 14)
                        {
                            tile.SetType(8);
                        }
                        else
                        {
                            tile.SetType(3);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            tile.SetType(2);
                        }
                        else if (j == 14)
                        {
                            tile.SetType(4);
                        }
                        else if ((new int[] {3, 11}.Contains(i) && new int[] { 3, 11 }.Contains(j)) ||
                                  i == 7 && j == 7)
                        {
                            tile.SetType(1);
                        }
                    }
                    tile.AutoSize = false;
                    tile.Location = new System.Drawing.Point(60*i, 60*j);
                    tile.Name = string.Format("boardx{0:D}y{1:D}", i, j);
                    tile.Size = new System.Drawing.Size(60, 60);
                    tile.TabIndex = 15*i+j;
                    tile.Text = "";
                    tile.SetImage(0);
                    tile.Click += ClickEventGenerator(i, j);
                    board.Add(tile);
                    Controls.Add(tile);
                }
            }

            this.PerformLayout();
        }

        private List<Tile> board = new List<Tile> { };
    }
}

