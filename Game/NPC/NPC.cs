using Game.Scripting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace Game.NPC
{
    [Serializable]
    public class Npc : InteractableObject
    {
        public Npc()
        {
            NpcHelper.AddNPC(this);
        }

        public override void Draw(Graphics g)
        {
            Animate();
            using (var ia = new System.Drawing.Imaging.ImageAttributes())
            {
                var startingBrightness = GlobalLighting.Instance.Matrix.Matrix40;
                if (Brightness != 0)
                {
                    float addedBrightness = ((float)Brightness / 255.0f);
                    GlobalLighting.Instance.Matrix.Matrix40 += addedBrightness;
                    GlobalLighting.Instance.Matrix.Matrix41 += addedBrightness;
                    GlobalLighting.Instance.Matrix.Matrix42 += addedBrightness;
                }
                ia.SetColorMatrix(GlobalLighting.Instance.Matrix);
                ia.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                g.DrawImage(Image, new Rectangle(Location.X, Location.Y, Size, Size), 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, ia);
                GlobalLighting.Instance.Matrix.Matrix40 = startingBrightness;
                GlobalLighting.Instance.Matrix.Matrix41 = startingBrightness;
                GlobalLighting.Instance.Matrix.Matrix42 = startingBrightness;
            }
        }

        public void MoveTo(MapTile tile)
        {
            Thread t = new Thread(() =>
            {
                move(tile);
            });
            t.Start();
        }

        private void move(MapTile tile)
        {
            _isMoving = true;
            List<Int32> path = ScriptDataBridge.Map.GetPath(CurrentTile, tile);
            for (int i = 0; i < path.Count; i++)
            {
                if (!_isMoving)
                {
                    Animating = false;
                    OnMoveCompleted();
                    return;
                }
                if (CurrentTile.MapRow == tile.MapRow && CurrentTile.MapCol == tile.MapCol)
                {
                    Animating = false;
                    OnMoveCompleted();
                    return;
                }
                if (path[i] == 1)
                {
                    setAnimation(Enums.Direction.Up);
                    MoveOneTile(0, -1);
                    //CurrentTile = ScriptDataBridge.Map.Tiles[CurrentTile.MapRow - 1][CurrentTile.MapCol];
                    //player.Animation = _walkUpAnimation;
                    //player.Animating = true;
                }
                else if (path[i] == 2)
                {
                    setAnimation(Enums.Direction.Right);
                    MoveOneTile(1, 0);
                    //CurrentTile = ScriptDataBridge.Map.Tiles[CurrentTile.MapRow][CurrentTile.MapCol + 1];
                    //player.Animation = _walkRightAnimation;
                    //player.Animating = true;
                }
                else if (path[i] == 3)
                {
                    setAnimation(Enums.Direction.Down);
                    MoveOneTile(0, 1);
                    //CurrentTile = ScriptDataBridge.Map.Tiles[CurrentTile.MapRow + 1][CurrentTile.MapCol];
                    //player.Animation = _walkDownAnimation;
                    //player.Animating = true;
                }
                else if (path[i] == 4)
                {
                    setAnimation(Enums.Direction.Left);
                    MoveOneTile(-1, 0);
                    //CurrentTile = ScriptDataBridge.Map.Tiles[CurrentTile.MapRow][CurrentTile.MapCol - 1];
                    //player.Animation = _walkLeftAnimation;
                    //player.Animating = true;
                }
            }
            Animating = false;
            _isMoving = false;
            OnMoveCompleted();
        }

        private void setAnimation(Enums.Direction direction)
        {
            if (_animations.ContainsKey(Enums.Direction.Up))
            {
                Animation = _animations[direction];
                Animating = true;
            }
        }

        public void MoveOneTile(Int32 x, Int32 y)
        {
            lock (movingLockObject)
            {
                var targetTile = ScriptDataBridge.Map.Tiles[CurrentTile.MapRow + y][CurrentTile.MapCol + x];
                while (Location != targetTile.Location && !MasterRunningScriptList.Closing)
                {
                    if (Math.Abs(Location.X - targetTile.Location.X) <= Speed)
                    {
                        if (Math.Abs(Location.Y - targetTile.Location.Y) <= Speed)
                        {
                            CurrentTile = ScriptDataBridge.Map.Tiles[CurrentTile.MapRow + y][CurrentTile.MapCol + x];
                            return;
                        }
                    }
                    System.Threading.Thread.Sleep(15);
                    _xOffset += (x * Speed);
                    _yOffset += (y * Speed);
                    //Location = new Point(Location.X + (x * Speed), Location.Y + (y * Speed));
                }
            }
        }

        private object movingLockObject = new object();

        public void StopMoving()
        {
            _isMoving = false;
        }

        public MapTile CurrentTile
        {
            get
            {
                return _currentTile;
            }
            set
            {
                Brightness = value.Brightness;
                _currentTile = value;
                _yOffset = 0;
                _xOffset = 0;
                OnNpcMoved();
            }
        }

        public Boolean IsMoving
        {
            get
            {
                return _isMoving;
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

        public Int32 Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public Point Location
        {
            get
            {
                return new Point(_currentTile.Location.X + _xOffset, _currentTile.Location.Y + _yOffset);
            }
            set
            {

                _xOffset += _currentTile.Location.X - value.X;
                _yOffset += _currentTile.Location.Y - value.Y;
            }
        }

        protected virtual void OnNpcMoved()
        {
            NpcMoved?.Invoke(this, new EventArgs());
        }

        protected virtual void OnMoveCompleted()
        {
            MoveCompleted?.Invoke(this, new EventArgs());
        }

        public event EventHandler NpcMoved;
        public event EventHandler MoveCompleted;

        protected Dictionary<Enums.Direction, Animation> _animations = new Dictionary<Enums.Direction, Animation>();
        private MapTile _currentTile;
        private Int32 _speed = 3;
        private Int32 _size = Screen.TileSize;

        [NonSerialized]
        private Boolean _isMoving = false;

        private Int32 _xOffset = 0;
        private Int32 _yOffset = 0;
    }
}
