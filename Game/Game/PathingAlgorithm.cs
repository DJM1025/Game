using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Game
{
    class PathingAlgorithm
    {
        public PathingAlgorithm(Map map)
        {
            _map = map;
        }

        public List<Int32> GetMovementDirections(MapTile fromTile, MapTile toTile)
        {
            startTile = fromTile;
            endTile = toTile;
            if (toTile.Collide)
            {
                return new List<Int32> { 0 };
            }
            List<Int32> directions = getBestPath();
            return directions;
        }

        private List<Int32> getBestPath()
        {
            Queue<Node<Point>> nodeQueue = new Queue<Node<Point>>();
            Node<Point> paths = new Node<Point>(new Point(startTile.MapCol, startTile.MapRow), null);
            nodeQueue.Enqueue(paths);
            Node<Point> bestPath = null;
            try
            {
                bestPath = getBestPath(nodeQueue);

            }
            catch (Exception e)
            {
                return new List<Int32> { 0 };
            }
            Node<Point> current = bestPath;
            Stack<Int32> path = new Stack<Int32>();
            if (current == null)
            {
                path.Push(0);
                return path.ToList();
            }
            while (current.Parent != null)
            {
                Point parentPoint = current.Parent.Value;
                if (parentPoint.X > current.Value.X)
                {
                    path.Push(4);
                }
                if (parentPoint.X < current.Value.X)
                {
                    path.Push(2);
                }
                if (parentPoint.Y > current.Value.Y)
                {
                    path.Push(1);
                }
                if (parentPoint.Y < current.Value.Y)
                {
                    path.Push(3);
                }
                current = current.Parent;
            }
            String s = "";
            foreach (var i in path)
            {
                s += i.ToString();
            }
            System.Console.WriteLine(s);
            return path.ToList();
        }

        private void stopTime(object sender, System.Timers.ElapsedEventArgs e)
        {
            ((System.Timers.Timer)sender).Stop();
            isPathFinding = false;
        }

        private volatile Boolean isPathFinding = true;

        private Node<Point> getBestPath(Queue<Node<Point>> nodeQueue)
        {
            System.Timers.Timer t = new System.Timers.Timer(100);
            t.Elapsed += stopTime;
            t.Start();

            while (nodeQueue.Count > 0 && isPathFinding)
            {
                Node<Point> current = nodeQueue.Dequeue();
                addTile(new Point(current.Value.X + 1, current.Value.Y), current);
                addTile(new Point(current.Value.X - 1, current.Value.Y), current);
                addTile(new Point(current.Value.X, current.Value.Y + 1), current);
                addTile(new Point(current.Value.X, current.Value.Y - 1), current);
                var currentChildren = current.Children;
                foreach (var child in currentChildren)
                {
                    nodeQueue.Enqueue(child);
                    if (child.Value == new Point(endTile.MapCol, endTile.MapRow))
                    {
                        return child;
                    }
                }
            }
            return null;
        }

        private void addTile(Point p, Node<Point> parent)
        {
            try
            {
                if (!_map.Tiles[p.Y][p.X].Collide && !nodeParentHasValue(parent, p))
                {
                    parent.Children.Add(new Node<Point>(p, parent));
                }
            }
            catch
            {
                return;
            }
        }

        private Boolean nodeParentHasValue(Node<Point> node, Point p)
        {
            if (node.Parent != null)
            {
                if ((new Point(node.Parent.Value.X, node.Parent.Value.Y)).Equals(p))
                {
                    return true;
                }
                else
                {
                    return nodeParentHasValue(node.Parent, p);
                }
            }
            return false;
        }

        /*
        private List<Int32> getBestPath()
        {
            List<Int32> best = new List<Int32>();
            Int32 maxFitness = Int32.MaxValue;
            var population = getChromosomes();
            for (int generation = 0; generation < numGenerations; generation++)
            {
                List<Double> fitnessRatios = new List<Double>();
                List<Int32> fitness = new List<Int32>();
                for(int i = 0; i < populationSize; i++)
                {
                    fitness.Add(getFitness(population[i]));
                }

                for (int i = 0; i < populationSize; i++)
                {
                    if (fitness[i] < maxFitness)
                    {
                        maxFitness = fitness[i];
                        best = population[i];
                    }
                }
                Int32 minFitness = fitness.Max();
                Double avg = fitness.Average();
                for (int i = 0; i < populationSize; i++)
                {
                    fitnessRatios.Add((double)avg / (double)fitness[i]);
                }
                population.Add(best);
                for (int i = 0; i < populationSize * (1 - mutationRate); i++ )
                {
                    population.Add(population[pickChromosomeIndex(fitnessRatios)]);
                }
                while(population.Count < populationSize * 2)
                {
                    Int32 index1 = pickChromosomeIndex(fitnessRatios);
                    Int32 index2 = pickChromosomeIndex(fitnessRatios);
                    population.Add(crossover(population[index1], population[index2]));
                }
                Random r = new Random();
                for (int i = 0; i < populationSize * mutationRate; i++)
                {
                    if (mutationRate > r.NextDouble())
                    {
                        population[i][r.Next(0, chromosomeLength)] = r.Next(0, 5);
                    }
                }
                population.RemoveRange(0, populationSize);

                //Do crossover and mutationy thingss
            }

            return best;
        }

        private Int32 pickChromosomeIndex(List<Double> fitness)
        {
            Random r = new Random();
            Double worstFitness = fitness.Max();
            Int32 index = r.Next(0, fitness.Count);
            while (fitness[index] > r.NextDouble())
            {
                index = r.Next(0, fitness.Count);
            }
            return index;
        }

        private List<Int32> crossover(List<Int32> c1, List<Int32> c2)
        {
            Random r = new Random();
            Int32 index = r.Next(1, chromosomeLength);
            List<Int32> result = new List<Int32>();

            result.AddRange(c1.GetRange(0, index));
            result.AddRange(c2.GetRange(index, chromosomeLength - index));
            return result;
        }
        */
        private void setRowColumnIndex()
        {
            var tiles = _map.Tiles;
            Int32 currentIndex = 0;
            for (int i = 0; i < _map.MapSize; i++)
            {
                currentIndex = tiles[i].IndexOf(startTile);
                if (currentIndex >= 0)
                {
                    startTileRowIndex = i;
                    startTileColIndex = currentIndex;
                }
                currentIndex = tiles[i].IndexOf(startTile);
                if (currentIndex >= 0)
                {
                    endTileRowIndex = i;
                    endTileColIndex = currentIndex;
                }
            }
        }
        /*
        private List<List<Int32>> getChromosomes()
        {
            List<List<Int32>> chromosomes = new List<List<Int32>>();
            Random r = new Random();
            for (int i = 0; i < populationSize; i++)
            {
                chromosomes.Add(new List<Int32>());
                for (int j = 0; j < chromosomeLength; j++)
                {
                    chromosomes[i].Add(r.Next(1, 5));
                }
            }
            return chromosomes;
        }

        private Int32 getFitness(List<Int32> chromosome)
        {
            Int32 fitness = 0;
            Int32 currentColIndex = startTileColIndex;
            Int32 currentRowIndex = startTileRowIndex;
            var tiles = _map.Tiles;
            for (int i = 0; i < chromosomeLength; i++)
            {
                //EDGE THING THAT I KNOW
                if (chromosome[i] == 1 && currentRowIndex > 0)
                {
                    currentRowIndex--;
                }
                if (chromosome[i] == 2 && currentColIndex < _map.MapSize - 1)
                {
                    currentColIndex++;
                }
                if (chromosome[i] == 3 && currentRowIndex < _map.MapSize - 1)
                {
                    currentRowIndex++;
                }
                if (chromosome[i] == 4 && currentColIndex > 0)
                {
                    currentColIndex--;
                }
                if (tiles[currentRowIndex][currentColIndex] == endTile)
                {
                    return fitness++;
                }
                fitness++;
                if (tiles[currentRowIndex][currentColIndex].Collide)
                {
                    fitness += 10;
                }
            }
            fitness += Math.Abs((currentRowIndex - endTileRowIndex) * 3);
            fitness += Math.Abs((currentColIndex - endTileColIndex) * 3);
            return fitness;
        }
        */
        private MapTile startTile;
        private MapTile endTile;
        /*
        private Int32 numGenerations = 100;
        private Int32 populationSize = 100;
        private Int32 chromosomeLength = 20;
        private Double crossoverRate = .7;
        private Double mutationRate = .2;
        */
        private Int32 startTileRowIndex = 0;
        private Int32 startTileColIndex = 0;

        private Int32 endTileRowIndex = 0;
        private Int32 endTileColIndex = 0;

        Map _map;
    }

    class Node<T>
    {

        public Node(T value, Node<T> parent)
        {
            Value = value;
            Parent = parent;
        }

        public Boolean ParentHasValue(T value)
        {
            if (Parent != null)
            {
                if (Parent.Value.Equals(value))
                {
                    return true;
                }
                else
                {
                    return Parent.ParentHasValue(value);
                }
            }
            return false;
        }

        public List<Node<T>> Children
        {
            get
            {
                return _children;
            }
            set
            {
                _children = value;
            }
        }

        public Node<T> Parent
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

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        private Node<T> _parent;

        private T _value;

        private List<Node<T>> _children = new List<Node<T>>();
    }
}
