﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Game
{
    [Serializable]
    public class MapTile : InteractableObject
    {
        public MapTile(Point location, Int32 tileSize,  Bitmap image) : base()
        {
            Location = location;
            Image = image;
            TileSize = tileSize;
        }

        public MapTile() : base()
        {
            Location = new Point(0, 0);
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
                g.DrawImage(Image, new Rectangle(Location.X, Location.Y, TileSize, TileSize), 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, ia);
                GlobalLighting.Instance.Matrix.Matrix40 = startingBrightness;
                GlobalLighting.Instance.Matrix.Matrix41 = startingBrightness;
                GlobalLighting.Instance.Matrix.Matrix42 = startingBrightness;
            }
            //g.DrawImage(_image, (float)Math.Floor((double)Location.X), (float)Math.Floor((double)Location.Y), (float)Math.Floor((double)TileSize), (float)Math.Floor((double)TileSize));
        }

        public Boolean IsTileVisible()
        {
            var playerPosition = Scripting.ScriptDataBridge.Player.Location;
            if (Location.X + Screen.TileSize < playerPosition.X - Screen.ScreenWidth / 2 || Location.X - Screen.TileSize > playerPosition.X + Screen.ScreenWidth / 2)
            {
                return false;
            }
            if (Location.Y + Screen.TileSize < playerPosition.Y - Screen.ScreenHeight / 2 || Location.Y - Screen.TileSize > playerPosition.Y + Screen.ScreenHeight / 2)
            {
                return false;
            }
            return true;
        }

        public Int32 TileSize
        {
            get
            {
                return _tileSize;
            }
            set
            {
                _tileSize = value > 0 ? value : 32;
            }
        }

        public Int32 MapRow
        {
            get
            {
                return _mapRow;
            }
            set
            {
                _mapRow = value > 0 ? value : 0;
            }
        }

        public Int32 MapCol
        {
            get
            {
                return _mapCol;
            }
            set
            {
                _mapCol = value > 0 ? value : 0;
            }
        }


        private Int32 _mapRow = 0;
        private Int32 _mapCol = 0;

        private Int32 _tileSize = Screen.TileSize;
    }
}
