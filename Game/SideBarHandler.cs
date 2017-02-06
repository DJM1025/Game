using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Game
{
    class SideBarHandler : IDisposable
    {
        public SideBarHandler(PictureBox sideBar)
        {
            _images = typeof(Properties.Resources)
               .GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic |
                                                    System.Reflection.BindingFlags.Public)
               .Where(p => p.PropertyType == typeof(Bitmap))
               .Select(x => x.GetValue(null, null))
               .ToList().ConvertAll(new Converter<Object, Bitmap>((object item) => { return (Bitmap)item; }));
            _sideBar = sideBar;
        }

        public void setDevMode(Int32 TileSize)
        {
            tileSize = TileSize;
            _tiles = new List<MapTile>();
            Int32 x = _sideBar.Width / TileSize;
            Int32 j = 0;
            for (int i = 0; i < _images.Count; i++)
            {
                if (i % x == 0 && i != 0)
                {
                    j++;
                }
                _tiles.Add(new MapTile(new Point((i % x ) * TileSize, j*TileSize), TileSize, _images[i]));
            }
            _sideBar.Click += dev_click;
            _sideBar.Paint += paint_sidebar;
            _sideBar.Invalidate();
        }

        private void dev_click(object sender, EventArgs e)
        {
            Int32 x = ((MouseEventArgs)e).X;
            Int32 y = ((MouseEventArgs)e).Y;
            Int32 yIndex = ((y / tileSize)) * (_sideBar.Width / tileSize);
            Int32 xIndex = x / tileSize;
            Image = _images[yIndex + xIndex];
        }

        public void Dispose()
        {
            _sideBar.Paint -= paint_sidebar;
        }

        private void paint_sidebar(object sender, PaintEventArgs e)
        {
            foreach (var tile in _tiles)
            {
                tile.Draw(e.Graphics);
            }
        }

        public Bitmap Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        private List<Bitmap> _images = new List<Bitmap>();
        private PictureBox _sideBar;
        private List<MapTile> _tiles;
        private Bitmap _image = Properties.Resources.grass;
        private Int32 tileSize = 32;
    }
}
