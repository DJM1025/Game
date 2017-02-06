using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace Game
{
    [Serializable]
    class Player : AnimatableObject
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
            a.Load("playerFront.bin");
            _animations.Add(WalkingDirection.Down, a);
            a = new Animation();
            a.Load("playerLeft.bin");
            _animations.Add(WalkingDirection.Left, a);
            a = new Animation();
            a.Load("playerRight.bin");
            _animations.Add(WalkingDirection.Right, a);
            a = new Animation();
            a.Load("playerBack.bin");
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
            }
        }

        private Bitmap _defaultImage;
        private MapTile _currentTile;
        private Dictionary<WalkingDirection, Animation> _animations = new Dictionary<WalkingDirection, Animation>();
        private Int32 _size = 64;

        public enum WalkingDirection
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}
