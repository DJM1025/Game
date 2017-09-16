using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace Game
{
    [Serializable]
    public class Player : AnimatableObject
    {
        public Player(Point location, MapTile startTile)
        {
            this.Location = location;
            this.CurrentTile = startTile;
            loadPlayerAnimations();
        }

        public override void Draw(Graphics g)
        {
            Animate();
            g.DrawImage(Image, Location.X, Location.Y, _size, _size);
        }

        public void loadPlayerAnimations()
        {
            Animation a = new Animation();
            a.Load(animPath + "playerFront.bin");
            _animations.Add(WalkingDirection.Down, a);
            a = new Animation();
            a.Load(animPath + "playerLeft.bin");
            _animations.Add(WalkingDirection.Left, a);
            a = new Animation();
            a.Load(animPath + "playerRight.bin");
            _animations.Add(WalkingDirection.Right, a);
            a = new Animation();
            a.Load(animPath + "playerBack.bin");
            _animations.Add(WalkingDirection.Up, a);
            Animation = _animations[WalkingDirection.Down];
        }

        public void StartWalking(WalkingDirection direction)
        {
            try
            {
                Animation = _animations[direction];
                Animation.Animating = true;
            }
            catch
            {
                Animation = _animations[_animations.Keys.First()];
                Animation.Animating = false;
            }
        }

        public void MoveToLocation(Int32 x, Int32 y, Map map)
        {
            Thread moveThread;
            if (Animating)
            {
                Animating = false;
            }
            List<Int32> path = map.GetPath(CurrentTile, map.GetClickedTile(x, y));
            moveThread = new Thread(() => moveToLocation(x, y, path, map));
            moveThread.Start();
        }

        private void moveToLocation(Int32 x, Int32 y, List<Int32> path, Map map)
        {
            Animating = true;
            MapTile clickedTile = map.GetClickedTile(x, y);
            for (int i = 0; i < path.Count; i++)
            {
                if (!Animating)
                {
                    return;
                }
                if (CurrentTile.MapRow == clickedTile.MapRow && CurrentTile.MapCol == clickedTile.MapCol)
                {
                    map.CenterOnPlayer(Speed, this);
                    return;
                }
                if (path[i] == 1)
                {
                    CurrentTile = map.Tiles[CurrentTile.MapRow - 1][CurrentTile.MapCol];
                    StartWalking(Player.WalkingDirection.Up);
                }
                if (path[i] == 2)
                {
                    CurrentTile = map.Tiles[CurrentTile.MapRow][CurrentTile.MapCol + 1];
                    StartWalking(Player.WalkingDirection.Right);
                }
                if (path[i] == 3)
                {
                    CurrentTile = map.Tiles[CurrentTile.MapRow + 1][CurrentTile.MapCol];
                    StartWalking(Player.WalkingDirection.Down);
                }
                if (path[i] == 4)
                {
                    CurrentTile = map.Tiles[CurrentTile.MapRow][CurrentTile.MapCol - 1];
                    StartWalking(Player.WalkingDirection.Left);
                }
                map.CenterOnPlayer(Speed, this);
                while (Location != CurrentTile.Location && Animating)
                {
                    if (Math.Abs(Location.X - CurrentTile.Location.X) <= Speed)
                    {
                        if (Math.Abs(Location.Y - CurrentTile.Location.Y) <= Speed)
                        {
                            break;
                        }
                    }
                };
            }
            StopWalking();
        }

        public void StopWalking()
        {
            Animation.Animating = false;
        }

        public MapTile CurrentTile
        {
            get
            {
                return _currentTile;
            }
            set
            {
                _currentTile = value;
                OnPlayerMoved();
            }
        }

        public Int32 Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        private MapTile _currentTile;
        private Dictionary<WalkingDirection, Animation> _animations = new Dictionary<WalkingDirection, Animation>();
        private Int32 _size = Screen.TileSize;
        private Int32 _speed = 6;

        protected readonly string animPath = Scripting.BaseScript.animPath;
        protected readonly string soundPath = Scripting.BaseScript.soundPath;

        protected virtual void OnPlayerMoved()
        {
            PlayerMoved?.Invoke(this, new EventArgs());
        }

        public event EventHandler PlayerMoved;

        public enum WalkingDirection
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}
