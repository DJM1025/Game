using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Game
{
    [Serializable]
    class Map : DrawableObject
    {
        public Map(Int32 mapSize, Int32 TileSize)
        {
            this.TileSize = TileSize;
            Random r = new Random();
            MapSize = mapSize;
            for (int i = 0; i < MapSize; i++)
            {
                Tiles.Add(new List<MapTile>());
                for (int j = 0; j < MapSize; j++)
                {
                    MapTile t = new MapTile();
                    /*
                    if (r.Next(3) == 1 && (i != 50 && j != 50))
                    {
                        t.Image = Properties.Resources.tree;
                        t.Collide = true;
                    }
                    else if (r.Next(15) == 1 && (i != 50 && j != 50))
                    {
                        t.Image = Properties.Resources.water;
                        t.Collide = true;
                    }
                    else if (r.Next(5) == 1 && (i != 50 && j != 50))
                    {
                        t.Image = Properties.Resources.stone;
                    }
                    else
                    {
                        t.Image = Properties.Resources.grass;
                    }
                    */
                    //t.Image = Properties.Resources.grass;
                    t.Location = new Point(j * TileSize, i * TileSize);
                    t.TileSize = TileSize;
                    t.MapCol = j;
                    t.MapRow = i;
                    Tiles[i].Add(t);
                }
            }

            drawMutex = new System.Threading.Mutex(false);
        }

        public Map(String filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.Stream stream = new System.IO.FileStream("MyFile.bin",
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.Read);
                Map obj = (Map)formatter.Deserialize(stream);
                stream.Close();
                this.Tiles = obj.Tiles;
                this.TileSize = obj.TileSize;
                this.MapSize = obj.MapSize;
                this.TileSize = 64;
                drawMutex = new System.Threading.Mutex(false);
            }
            else
            {
                throw new Exception("Map file not found");
            }
        }

        public void Save()
        {
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.Stream stream = new System.IO.FileStream("MyFile.bin",
                                     System.IO.FileMode.Create,
                                     System.IO.FileAccess.Write, 
                                     System.IO.FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public override void Draw(Graphics g)
        {
            drawMutex.WaitOne();
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    Tiles[i][j].Draw(g);
                    if (ShowCollisions)
                    {
                        if (Tiles[i][j].Collide)
                        {
                            g.DrawRectangle(Pens.Purple, new Rectangle(Tiles[i][j].Location, new Size(TileSize, TileSize)));
                        }
                    }
                }
            }
            shiftTiles();
            drawMutex.ReleaseMutex();
        }

        public MapTile GetClickedTile(Int32 x, Int32 y)
        {
            return Tiles[findClickedRowIndex(y)][findClickedColIndex(x)];
        }

        public MapTile GetMiddleTile()
        {
            return Tiles[MapSize / 2][MapSize / 2];
        }

        public void Move(Int32 dx, Int32 dy, Int32 speed)
        {
            drawMutex.WaitOne();
            xMoveLeft = dx;
            yMoveLeft = dy;
            this.speed = speed;
            shiftTiles();
            drawMutex.ReleaseMutex();
        }

        public List<Int32> GetPath(MapTile fromTile, MapTile toTile)
        {
            PathingAlgorithm algorithm = new PathingAlgorithm(this);
            return algorithm.GetMovementDirections(fromTile, toTile);
        }

        #region PRIVATE

        private Int32 findClickedRowIndex(Int32 y)
        {
            for(int i = 0; i < MapSize; i++)
            {
                if (Tiles[i][0].Location.Y > y)
                {
                    return --i;
                }
            }

            return MapSize - 1;
        }

        private Int32 findClickedColIndex(Int32 x)
        {
            for (int i = 0; i < MapSize; i++)
            {
                if (Tiles[0][i].Location.X > x)
                {
                    return --i;
                }
            }

            return MapSize - 1;
        }

        private Int32 getXSpeed()
        {
            if (xMoveLeft < 0)
            {
                if (xMoveLeft < -speed)
                {
                    return -speed;
                }
            }
            else if(xMoveLeft > speed)
            {
                return speed;
            }
            return xMoveLeft;
        }

        private Int32 getYSpeed()
        {
            if (yMoveLeft < 0)
            {
                if (yMoveLeft < -speed)
                {
                    return -speed;
                }
            }
            else if (yMoveLeft > speed)
            {
                return speed;
            }
            return yMoveLeft;
        }

        private void shiftTiles()
        {
            Int32 speedx = getXSpeed();
            Int32 speedy = getYSpeed();
            if (speedx != 0 || speedy != 0)
            {
                for (int i = 0; i < MapSize; i++)
                {
                    for (int j = 0; j < MapSize; j++)
                    {
                        Point p = Tiles[i][j].Location;
                        p.Offset(speedx, speedy);
                        Tiles[i][j].Location = p;
                    }
                }
                xMoveLeft -= speedx;
                yMoveLeft -= speedy;
            }
        }

        #endregion PRIVATE

        #region PROPS

        public Int32 MapSize
        {
            get
            {
                return _mapSize;
            }
            set
            {
                _mapSize = value > 0 ? value : 100;
            }
        }

        public List<List<MapTile>> Tiles
        {
            get
            {
                return _tiles;
            }
            set
            {
                _tiles = value;
            }
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

        public Boolean ShowCollisions
        {
            get
            {
                return _showCollisions;
            }
            set
            {
                _showCollisions = value;
            }
        }

        public volatile Boolean isDrawing = false;

        #endregion PROPS

        [NonSerialized]
        System.Threading.Mutex drawMutex;
        List<List<MapTile>> _tiles = new List<List<MapTile>>();
        Int32 _mapSize = 128;
        Int32 _tileSize = 32;

        Int32 xMoveLeft = 0;
        Int32 yMoveLeft = 0;
        Int32 speed = 0;

        Boolean _showCollisions = false;

    }
}
