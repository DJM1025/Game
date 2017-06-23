using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Game.Scripting;
using System.IO;

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
            ScriptDataBridge.GetInstance().Map = this.map;
            ScriptDataBridge.GetInstance().Player = this.player;
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
            //map = new Map("MyFile.bin");
            map = new Map(_mapSize, _tileSize);
        }

        private void loadPlayer()
        {
            Point start = new Point((_gamePanel.Width / 2) - 16, (_gamePanel.Height / 2) - 16);
            player = new Player(start, map.GetMiddleTile());
            map.CenterOnPlayer(1000, player);
        }

        private void _gamePanel_MouseClick(object sender, MouseEventArgs e)
        {
            if(isDevMode)
            {
                map.ShowCollisions = isDevMode;
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
                            OpenFileDialog dialog = new OpenFileDialog();
                            dialog.ShowDialog();
                            Animation a = new Animation();
                            if (File.Exists(dialog.FileName))
                            {
                                a.Load(dialog.FileName);
                            }
                            tile.Animation = a;
                            tile.Animating = !tile.Animating;
                            this.Tag = a;
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
            MapTile clickedTile = map.GetClickedTile(e.X, e.Y);
            ScriptDataBridge.GetInstance().ActionObject = clickedTile;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (!isDevMode)
                {
                    clickedTile.Interact(this.player, this._gamePanel, e.X, e.Y);
                }
            }
            else
            {
                clickedTile.DefaultInteract();
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
            Sound.SoundHandler.UnloadAllSounds();
            //Sound.SoundActionConsumer.IsRunning = false;
            if (isDevMode)
            {
                map.Save();
            }
        }

        private bool _isRunning = true;
        private Thread _renderThread = null;
        private Int32 _mapSize = 100;
        public static Int32 _tileSize = 64;
        private Int32 speed = 6;
        private Boolean isDevMode = false;

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
