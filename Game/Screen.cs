using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Game
{
    public partial class Screen : Form
    {
        public Screen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadMap();
            loadPlayer();
            loadSideBar();
            System.Threading.TimerCallback tc = new TimerCallback(setFps);
            t = new System.Threading.Timer(tc, null, 1000, 1000);
            _gamePanel.Paint += titleScreenPaint;
            _gamePanel.Invalidate();   
        }

        private void titleScreenPaint(object sender, PaintEventArgs e)
        {
            String titleString = "Press Enter to play";
            Int32 fontSize = 24;
            Font font = new Font(FontFamily.Families.First(family => family.Name.Contains("Console")), fontSize);
            e.Graphics.DrawString(titleString, font, new SolidBrush(Color.Yellow), this._gamePanel.Width/2 - (titleString.Length * fontSize)/2, this._gamePanel.Height/2);
            _gamePanel.Invalidate();
        }

        private void _gamePanel_Paint(object sender, PaintEventArgs e)
        {
            map.Draw(e.Graphics);
            player.Draw(e.Graphics);
            e.Graphics.DrawString(fps.ToString(), new Font(FontFamily.GenericSerif, 12), new SolidBrush(Color.Yellow), 20, 20);
            frames++;
        }

        private void startGame()
        {
            _gamePanel.Paint += _gamePanel_Paint;
            while (_isRunning)
            {
                try
                {
                    _gamePanel.Invalidate();
                }
                catch
                {
                }
            }
        }

        private void loadSideBar()
        {
            sideBarHandler = new SideBarHandler(_sideBar);
            if (isDevMode)
            {
                sideBarHandler.setDevMode(_tileSize);
                //map.ShowCollisions = true;
            }
        }

        private void loadMap()
        {
            map = new Map("MyFile.bin");
            //map = new Map(_mapSize, _tileSize);
        }

        private void loadPlayer()
        {
            Point start = new Point((_gamePanel.Width / 2) - 16, (_gamePanel.Height / 2) - 16);
            player = new Player(start, map.GetMiddleTile());
            centerMapOnPlayer(1000);
        }

        private void centerMapOnPlayer(Int32 speed)
        {
            map.Move(player.Location.X - player.CurrentTile.Location.X, player.Location.Y - player.CurrentTile.Location.Y, speed);
        }

        private void _gamePanel_MouseClick(object sender, MouseEventArgs e)
        {
            if(isDevMode)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    System.Windows.Forms.ListBox tempBox = new System.Windows.Forms.ListBox();
                    tempBox.Items.Add("Collision");
                    tempBox.Items.Add("Set Image");
                    tempBox.Items.Add("Animate");
                    tempBox.Items.Add("Last Animation");
                    tempBox.Height = tempBox.PreferredHeight;
                    tempBox.Click += (object s, EventArgs ea) =>
                    {
                        ListBox clicked = (ListBox)s;
                        Int32 x = clicked.Location.X;
                        Int32 y = clicked.Location.Y;
                        MapTile tile = map.GetClickedTile(x, y);
                        if (clicked.SelectedItem == "Collision")
                        {
                            tile.Collide = !tile.Collide;
                        }
                        if (clicked.SelectedItem == "Set Image")
                        {
                            tile.Image = sideBarHandler.Image;
                        }
                        if (clicked.SelectedItem == "Animate")
                        {
                            AnimationMaker maker = new AnimationMaker(_tileSize);
                            maker.ShowDialog();
                            tile.Animation = maker.Animation;
                            tile.Animating = !tile.Animating;
                            maker.Dispose();
                            this.Tag = maker.Animation;
                        }
                        if (clicked.SelectedItem == "Last Animation")
                        {
                            if (this.Tag != null && this.Tag.GetType() == typeof(Animation))
                            {
                                Animation temp = (Animation)this.Tag;
                                tile.Animation.Images = temp.Images;
                                //tile.Animation.Timer = temp.Timer;
                                tile.Animation.DefaultImage = temp.DefaultImage;
                                tile.Animating = temp.Animating;
                                tile.Animation.AnimationDelayMs = temp.AnimationDelayMs;
                            }
                            //tile.Animating = !tile.Animating;
                        }
                        tempBox.Dispose();
                    };
                    tempBox.MouseLeave += (object s, EventArgs ea) =>
                    {
                        tempBox.Dispose();
                    };
                    tempBox.Location = new Point(e.X - 5, e.Y - 5);
                    this._gamePanel.Controls.Add(tempBox);
                    for (int i = 0; i < tempBox.Items.Count; i++)
                    {
                        tempBox.SetSelected(i, true);
                    }
                    tempBox.ClearSelected();
                    tempBox.Visible = true;
                }
                if (ModifierKeys.HasFlag(Keys.Shift))
                {
                    MapTile tile = map.GetClickedTile(e.X, e.Y);
                    tile.Image = sideBarHandler.Image;
                    return;
                }
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    MapTile tile = map.GetClickedTile(e.X, e.Y);
                    tile.Collide = !tile.Collide;
                    return;
                }
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (player.Animating)
                {
                    player.Animating = false;
                }
                List<Int32> path = map.GetPath(player.CurrentTile, map.GetClickedTile(e.X, e.Y));
                moveThread = new Thread(() => doMovement(e.X, e.Y, path));
                moveThread.Start();
            }
        }

        private void doMovement(Int32 x, Int32 y, List<Int32> path)
        {
            MapTile clickedTile = map.GetClickedTile(x,y);
            for (int i = 0; i < path.Count; i++)
            {
                if (player.CurrentTile.MapRow == clickedTile.MapRow && player.CurrentTile.MapCol == clickedTile.MapCol)
                {
                    centerMapOnPlayer(speed);
                    return;
                }
                if (path[i] == 1)
                {
                    player.CurrentTile = map.Tiles[player.CurrentTile.MapRow - 1][player.CurrentTile.MapCol];
                    player.StartWalking(Player.WalkingDirection.Up);
                }
                if (path[i] == 2)
                {
                    player.CurrentTile = map.Tiles[player.CurrentTile.MapRow][player.CurrentTile.MapCol + 1];
                    player.StartWalking(Player.WalkingDirection.Right);
                }
                if (path[i] == 3)
                {
                    player.CurrentTile = map.Tiles[player.CurrentTile.MapRow + 1][player.CurrentTile.MapCol];
                    player.StartWalking(Player.WalkingDirection.Down);
                }
                if (path[i] == 4)
                {
                    player.CurrentTile = map.Tiles[player.CurrentTile.MapRow][player.CurrentTile.MapCol - 1];
                    player.StartWalking(Player.WalkingDirection.Left);
                }
                if (!player.Animating)
                {
                    return;
                }
                centerMapOnPlayer(speed);
                while (player.Location != player.CurrentTile.Location && player.Animating) 
                { 
                    if(Math.Abs(player.Location.X - player.CurrentTile.Location.X) <= speed)
                    {
                        if (Math.Abs(player.Location.Y - player.CurrentTile.Location.Y) <= speed)
                        {
                            break;
                        }
                    }
                };
                player.StopWalking();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (_renderThread == null)
                {
                    _gamePanel.Paint -= titleScreenPaint;
                    _renderThread = new Thread(startGame);
                    _renderThread.Start();
                }
                _gamePanel.Invalidate();
            }
            return base.ProcessDialogKey(keyData);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isRunning = false;
            player.StopWalking();
            _gamePanel.Paint -= _gamePanel_Paint;
            _gamePanel.Dispose();
            sideBarHandler.Dispose();
            _sideBar.Dispose();
            this.Dispose();
            if (isDevMode)
            {
                map.Save();
            }
        }

        private bool _isRunning = true;
        private Thread _renderThread = null;
        private Thread moveThread = null;
        private Int32 _mapSize = 100;
        private Int32 _tileSize = 64;
        private Int32 speed = 6;
        private Boolean isDevMode = true;

        private Map map;
        private Player player;
        private SideBarHandler sideBarHandler;

        private Int32 frames = 0;
        private Int32 fps = 0;
        System.Threading.Timer t;

        public void setFps(object target)
        {
            fps = frames;
            frames = 0;
        }
    }
}
