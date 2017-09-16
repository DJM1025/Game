using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Game
{
    public class GlobalLighting
    {
        private GlobalLighting()
        {
            _matrix = new ColorMatrix();
            _matrix.Matrix00 = 1;
            _matrix.Matrix11 = 1;
            _matrix.Matrix22 = 1;
            _matrix.Matrix33 = 1;

            _matrix.Matrix43 = 0;
            _matrix.Matrix44 = 1;
        }

        public static GlobalLighting Instance
        {
            get
            {
                _instance = _instance != null ? _instance : new GlobalLighting();
                return _instance;
            }
        }

        private void ApplyLighting()
        {
            float FinalValue = (float)Brightness / 255.0f;

            _matrix.Matrix40 = FinalValue;
            _matrix.Matrix41 = FinalValue;
            _matrix.Matrix42 = FinalValue;
        }

        public ColorMatrix Matrix
        {
            get
            {
                return _matrix;
            }
        }

        public Int32 Brightness
        {
            get
            {
                return _brightness;
            }
            set
            {
                if (value < -255)
                {
                    _brightness = -255;
                }
                else if (value > 255)
                {
                    _brightness = 255;
                }
                else
                {
                    _brightness = value;
                }
                ApplyLighting();
            }
        }

        [NonSerialized]
        private static ColorMatrix _matrix = new ColorMatrix();
        private static Int32 _brightness = 0;
        private static GlobalLighting _instance;
    }
}
