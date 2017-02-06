using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game
{
    [Serializable]
    public partial class AnimationMaker : Form
    {
        public AnimationMaker(Int32 TileSize)
        {
            InitializeComponent();
            _tileSize = TileSize;
            _animationBox.Paint += paintAnimationPreview;
        }

        private void _loadButton_Click(object sender, EventArgs e)
        {
            var _images = typeof(Properties.Resources)
               .GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic |
                                                    System.Reflection.BindingFlags.Public)
               .Where(p => p.PropertyType == typeof(Bitmap))
               .Select(x => x.GetValue(null, null))
               .ToList().ConvertAll(new Converter<Object, Bitmap>((object item) => { return (Bitmap)item; }));
            Int32 maxTiles = _selectionBox.Width / _tileSize;
            Int32 j = 0;
            for (int i = 0; i < _images.Count; i++)
            {
                if (i % maxTiles == 0 && i != 0)
                {
                    j++;
                }
                _selectionTiles.Add(new MapTile(new Point((i % maxTiles) * _tileSize, j * _tileSize), _tileSize, _images[i]));
            }
            _selectionBox.Paint += drawImages;
            _selectionBox.Invalidate();
        }

        private void drawImages(object sender, PaintEventArgs e)
        {
            foreach (var tile in _selectionTiles)
            {
                tile.Draw(e.Graphics);
            }
        }

        private void paintAnimationPreview(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < _animation.Images.Count; i++)
            {
                e.Graphics.DrawImage(_animation.Images[i], i * _tileSize, 0, _tileSize, _tileSize);
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.Stream stream = new System.IO.FileStream("Animation.bin",
                                     System.IO.FileMode.Create,
                                     System.IO.FileAccess.Write,
                                     System.IO.FileShare.None);
            formatter.Serialize(stream, this._animation);
            stream.Close();
        }

        private void _selectionBox_Click(object sender, EventArgs e)
        {
            Int32 x = ((MouseEventArgs)e).X;
            Int32 y = ((MouseEventArgs)e).Y;
            Int32 yIndex = ((y / _tileSize)) * (_selectionBox.Width / _tileSize);
            Int32 xIndex = x / _tileSize;
            _selected = _selectionTiles[yIndex + xIndex];
        }

        private void _animationBox_Click(object sender, EventArgs e)
        {
            Int32 x = ((MouseEventArgs)e).X;
            Int32 position = x % _tileSize;
            if(position > _animation.Images.Count)
            {
                _animation.Images.Add(_selected.Image);
            }
            else
            {
                _animation.Images[position] = _selected.Image;
            }
            _animationBox.Invalidate();
        }

        private void _speed_ValueChanged(object sender, EventArgs e)
        {

        }

        public Animation Animation
        {
            get
            {
                return _animation;
            }
            set
            {
                _animation = value;
            }
        }

        private List<MapTile> _selectionTiles = new List<MapTile>();
        private Animation _animation = new Animation();
        private Int32 _tileSize = 64;
        private MapTile _selected = new MapTile();
    }
}
