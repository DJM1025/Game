using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Game.Scripting;

namespace Game
{
    class PathingAlgorithm
    {
        public List<Int32> getBestPathAStar(MapTile fromTile, MapTile toTile)
        {
            if (toTile.Collide)
            {
                return new List<Int32>();
            }
            var startNode = new ASNode(fromTile, this);
            _endingNode = new ASNode(toTile, this);
            var endNode = startNode.GetBestPath();

            Stack<Int32> path = new Stack<int>();

            var currentNode = endNode;
            if (endNode != null)
            {
                while (currentNode.Parent != null)
                {
                    if (currentNode.Parent.Tile.MapCol > currentNode.Tile.MapCol)
                    {
                        path.Push(4);
                    }
                    else if (currentNode.Parent.Tile.MapCol < currentNode.Tile.MapCol)
                    {
                        path.Push(2);
                    }
                    else if (currentNode.Parent.Tile.MapRow > currentNode.Tile.MapRow)
                    {
                        path.Push(1);
                    }
                    else if (currentNode.Parent.Tile.MapRow < currentNode.Tile.MapRow)
                    {
                        path.Push(3);
                    }
                    currentNode = currentNode.Parent;
                }
            }
            return path.ToList();
        }

        public List<ASNode> OpenSet
        {
            get
            {
                return _openSet;
            }
        }

        public List<ASNode> ClosedSet
        {
            get
            {
                return _closedSet;
            }
        }

        public ASNode EndingNode
        {
            get
            {
                return _endingNode;
            }
        }

        private List<ASNode> _openSet = new List<ASNode>();
        private List<ASNode> _closedSet = new List<ASNode>();

        private ASNode _endingNode;

    }
    class ASNode
    {
        public ASNode(MapTile currentTile, PathingAlgorithm algorithm)
        {
            _currentTile = currentTile;
            _algorithm = algorithm;
        }

        public ASNode GetBestPath()
        {
            System.Timers.Timer t = new System.Timers.Timer(100);
            t.Elapsed += T_Elapsed;

            _algorithm.ClosedSet.Add(this);
            calculateAdjacentNodes();
            var lowestNode = getLowestFScoreNode();
            while (lowestNode != null && lowestNode.HCost > 0 && _isPathFinding)
            {
                //var img = lowestNode.Tile.Image;
                //lowestNode.Tile.Image = Properties.Resources.stone;
                //System.Threading.Thread.Sleep(100);
                _algorithm.OpenSet.Remove(lowestNode);
                _algorithm.ClosedSet.Add(lowestNode);
                lowestNode.calculateAdjacentNodes();
                lowestNode = getLowestFScoreNode();
            }
            return lowestNode;
        }

        private void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ((System.Timers.Timer)sender).Stop();
            _isPathFinding = false;
        }

        #region Properties

        public Int32 GCost
        {
            get
            {
                return _gCost;
            }
            set
            {
                _gCost = value;
            }
        }

        public Int32 HCost
        {
            get
            {
                if (_hCost < 0)
                {
                    Int32 dX = Math.Abs(_currentTile.MapCol - _algorithm.EndingNode.Tile.MapCol);
                    Int32 dY = Math.Abs(_currentTile.MapRow - _algorithm.EndingNode.Tile.MapRow);
                    _hCost = dX + dY;
                }
                return _hCost * 2;
            }
        }

        public Int32 FCost
        {
            get
            {
                return GCost + HCost;
            }
        }

        public MapTile Tile
        {
            get
            {
                return _currentTile;
            }
        }

        public ASNode Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        #endregion

        #region Private Methods

        private ASNode getLowestFScoreNode()
        {
            if (_algorithm.OpenSet.Count == 0)
            {
                return null;
            }
            Int32 zeroHIndex = _algorithm.OpenSet.FindIndex(node => node.HCost == 0);
            if (zeroHIndex != -1)
            {
                return _algorithm.OpenSet[zeroHIndex];
            }
            var min = _algorithm.OpenSet.Min(node => node.FCost);
            var minNodes = _algorithm.OpenSet.Where(node => node.FCost == min).ToList();
            var minG = minNodes[0].GCost;
            var minGIndex = 0;
            for (int i = 1; i < minNodes.Count; i++)
            {
                if (minNodes[i].GCost < minG)
                {
                    minG = minNodes[i].GCost;
                    minGIndex = i;
                }
            }
            return minNodes[minGIndex];
        }

        public void calculateAdjacentNodes()
        {
            Int32 x = _currentTile.MapCol;
            Int32 y = _currentTile.MapRow;
            updateTileAtLocation(x + 1, y);
            updateTileAtLocation(x - 1, y);
            updateTileAtLocation(x, y + 1);
            updateTileAtLocation(x, y - 1);
        }

        private void updateTileAtLocation(Int32 x, Int32 y)
        {
            if (x < ScriptDataBridge.Map.MapSize && x >= 0)
            {
                if (y < ScriptDataBridge.Map.MapSize && y >= 0)
                {
                    var tempTile = ScriptDataBridge.Map.Tiles[y][x];
                    if (!tempTile.Collide)
                    {
                        addTileToLists(tempTile);
                    }
                }
            }
        }

        private void addTileToLists(MapTile tile)
        {
            if (!_algorithm.ClosedSet.Any(node => node.Tile == tile))
            {
                var currentNode = _algorithm.OpenSet.FirstOrDefault(node => node.Tile == tile);
                if (currentNode == null)
                {
                    currentNode = new ASNode(tile, _algorithm);
                    currentNode.GCost = GCost + 1;
                    currentNode.Parent = this;
                    _adjacentNodes.Add(currentNode);
                    _algorithm.OpenSet.Add(currentNode);
                }
                else if (currentNode.GCost > GCost + 1)
                {
                    if (!_adjacentNodes.Contains(currentNode))
                    {
                        _adjacentNodes.Add(currentNode);
                    }
                    currentNode.GCost = GCost + 1;
                    currentNode.Parent = this;
                }
            }
        }

        #endregion Private Methods

        private Boolean _isPathFinding = true;
        private Int32 _hCost = int.MinValue;
        private Int32 _gCost = 0;
        private ASNode _parent;
        private MapTile _currentTile;

        private List<ASNode> _adjacentNodes = new List<ASNode>();

        private PathingAlgorithm _algorithm;
    }
}