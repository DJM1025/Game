using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace Game
{
    [Serializable]
    class Player : DrawableObject
    {
        public Player(Point location, MapTile startTile)
        {
            this.Location = location;
            this.CurrentTile = startTile;
            loadPLayerImages();
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(_image, Location.X, Location.Y, 64, 64);
        }

        public void loadPLayerImages()
        {

            _front.Add(Properties.Resources.playerFront1);
            _front.Add(Properties.Resources.playerFront2);
            _front.Add(Properties.Resources.playerFront1);
            _front.Add(Properties.Resources.playerFront);
            _front.Add(Properties.Resources.playerFront3);
            _front.Add(Properties.Resources.playerFront4);
            _front.Add(Properties.Resources.playerFront3);
            _front.Add(Properties.Resources.playerFront);

            _back.Add(Properties.Resources.playerBack);
            _back.Add(Properties.Resources.playerBack1);
            _back.Add(Properties.Resources.playerBack2);
            _back.Add(Properties.Resources.playerBack1);
            _back.Add(Properties.Resources.playerBack);
            _back.Add(Properties.Resources.playerBack3);
            _back.Add(Properties.Resources.playerBack4);
            _back.Add(Properties.Resources.playerBack3);

            _left.Add(Properties.Resources.playerLeftA);
            _left.Add(Properties.Resources.playerLeftB);
            _left.Add(Properties.Resources.playerLeftC);
            _left.Add(Properties.Resources.playerLeftD);
            _left.Add(Properties.Resources.playerLeftE);
            _left.Add(Properties.Resources.playerLeftF);

            _right.Add(Properties.Resources.playerRightA);
            _right.Add(Properties.Resources.playerRightB);
            _right.Add(Properties.Resources.playerRightC);
            _right.Add(Properties.Resources.playerRightD);
            _right.Add(Properties.Resources.playerRightE);
            _right.Add(Properties.Resources.playerRightF);

            _defaultImage = Properties.Resources.playerFront;

            _image = _defaultImage;
        }

        public void StartWalking(WalkingDirection direction)
        {
            _isWalking = true;
            List<Bitmap> imageQueue = new List<Bitmap>();

            switch (direction)
            {
                case WalkingDirection.Up:
                    imageQueue = _back;
                    break;
                case WalkingDirection.Down:
                    imageQueue = _front;
                    break;
                case WalkingDirection.Left:
                    imageQueue = _left;
                    break;
                case WalkingDirection.Right:
                    imageQueue = _right;
                    break;
            }

            movementThread = new Thread(() =>
            {
                walk(imageQueue);
            });
            movementThread.Start();
        }

        public void StopWalking()
        {
            _isWalking = false;
        }

        private void walk(List<Bitmap> walkingImages)
        {
            Int32 size = walkingImages.Count();
            Int32 currentImageIndex = 0;
            while (_isWalking)
            {
                _image = walkingImages[currentImageIndex];
                currentImageIndex = ++currentImageIndex % size;
            }
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
        private List<Bitmap> _front = new List<Bitmap>();
        private List<Bitmap> _back = new List<Bitmap>();
        private List<Bitmap> _left = new List<Bitmap>();
        private List<Bitmap> _right = new List<Bitmap>();

        private Thread movementThread;
        public volatile Boolean _isWalking = false;
        private Int32 _size = 32;

        public enum WalkingDirection
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}
