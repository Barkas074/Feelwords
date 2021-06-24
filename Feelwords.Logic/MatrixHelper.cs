using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Feelwords.Logic
{
    public enum Directions
    {
        Left,
        Right,
        Top,
        Bottom
    }
    public class MatrixHelper
    {
        private Random rd=new Random();
        private int[,] _matrix;
        private int n, m;
        private Dictionary<int, List<IntPoint>>_wordsPlaceHolders;
        private List<int> DeadEnds;
        private int _minLength;
        
        public MatrixHelper(int[,] matrix)
        {
            DeadEnds=new List<int>();
            _wordsPlaceHolders=new Dictionary<int, List<IntPoint>>();
            _matrix = matrix;
            n = matrix.GetLength(0);
            m = matrix.GetLength(1);
            
        }

        public Dictionary<int, List<IntPoint>> getDictionary()
        {
            return _wordsPlaceHolders;
        }
        public int[,] Slise(int minLength)
        {
            _minLength = minLength;
            InitStart();
            //printMatrix();
            for (int i = 0; i < 10; i++)
            {
                DeadEnds.Clear();
                EvolutionStep();
            }
            //while (Isready(minLength))
            //{
            //    EvolutionStep();
            //    TryForExplosion();
            //    Compress();
            //}
            //Debug.WriteLine("___________");
            //printMatrix();
            //Debug.WriteLine("___________");
            var a =_wordsPlaceHolders;
            return _matrix;
        }

        private void EvolutionStep()
        {
            int[] keys = _wordsPlaceHolders.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                TryToGrow(keys[i]);
            }
        }

        private void TryToGrow(int index)//поиск соседних червяков для слития. нужно чтобы с началом или концом одного червя граничило начало или конец другого червя
        {
            if(!_wordsPlaceHolders.ContainsKey(index)) return;
            List<IntPoint> worm = _wordsPlaceHolders[index];
            IntPoint head = worm[0];
            IntPoint tail = worm[worm.Count - 1];

            List<Directions> possibleDirection = GetPossibleDirection(tail, index);
            if (possibleDirection.Count != 0)
            {
                ChooseDirectionAndGrow(index, possibleDirection, tail);
                return;
            }
            possibleDirection = GetPossibleDirection(head, index);
            if (possibleDirection.Count != 0)
            {
                _wordsPlaceHolders[index].Reverse(); // развернуть червя и свести задачу к предыдущему пункту
                ChooseDirectionAndGrow(index, possibleDirection, head);
                return;
            }
            // если рост червя невозможени он короткий, то пометить его на подрыв.
            if (_wordsPlaceHolders[index].Count<_minLength)DeadEnds.Add(index);

        }

        private void ChooseDirectionAndGrow(int index, List<Directions> possibleDirection, IntPoint tail)
        {
            Directions direct = possibleDirection[rd.Next(0, possibleDirection.Count)];
            IntPoint otherPoint;
            switch (direct)
            {
                case Directions.Left:
                    otherPoint = new IntPoint(tail.x, tail.y-1);
                    GrowFromTailToDirection(index, otherPoint);
                    break;
                case Directions.Right:
                    otherPoint = new IntPoint(tail.x, tail.y+1);
                    GrowFromTailToDirection(index, otherPoint);
                    break;
                case Directions.Top:
                    otherPoint = new IntPoint(tail.x-1, tail.y);
                    GrowFromTailToDirection(index, otherPoint);
                    break;
                case Directions.Bottom:
                    otherPoint = new IntPoint(tail.x+1, tail.y);
                    GrowFromTailToDirection(index, otherPoint);
                    break;
            }
        }

        private void GrowFromTailToDirection(int index, IntPoint otherPoint)
        {
            int key = _matrix[otherPoint.x, otherPoint.y];
            if (key == index)
            {
                printMatrix();
                throw new Exception("начало");
			}
			if (_wordsPlaceHolders[index].Count < 10 && _wordsPlaceHolders[key].Count < 10)
			{
				if (otherPoint != _wordsPlaceHolders[key][0]
				) // если хвост первого упирается в хвост второго разворачиваем второго червя
				{
					_wordsPlaceHolders[key].Reverse();
				}

				foreach (var point in _wordsPlaceHolders[key])
				{
					//Debug.WriteLine($"transform {_matrix[point.x, point.y]} to {index}");
					_matrix[point.x, point.y] = index;
				}

				for (int i = 0; i < n; i++)
				{
					for (int j = 0; j < m; j++)
					{
						if (!_wordsPlaceHolders.Keys.Contains(_matrix[i, j]))
						{
							int kkkk = _matrix[i, j];
							printMatrix();
							throw new Exception("тут ошибка");
						}


					}
				}
				_wordsPlaceHolders[index].AddRange(_wordsPlaceHolders[key]); // прибавляем к первому червю второго
				_wordsPlaceHolders.Remove(key); //удаляем второго

				//Debug.WriteLine($"удалено {key}");

				if (rd.Next(1, 3) < 2) _wordsPlaceHolders[index].Reverse();
			}
		}

        private void printMatrix()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Debug.Write("\t"+_matrix[i,j]+"\t");
                }
                Debug.WriteLine("");
            }
        }

        private List<Directions> GetPossibleDirection(IntPoint point, int i)
        {
            List<Directions> possibleDirection = new List<Directions>();
            if (point.y > 0 && _matrix[point.x,point.y-1] != i && IsHeadOrTail(new IntPoint(point.x, point.y-1)))
            {
                possibleDirection.Add(Directions.Left);
            }
            if (point.y<m-1&&_matrix[point.x, point.y+1] != i && IsHeadOrTail(new IntPoint(point.x, point.y+1)))
            {
                possibleDirection.Add(Directions.Right);
            }
            if (point.x>0&&_matrix[point.x-1, point.y] != i && IsHeadOrTail(new IntPoint(point.x-1, point.y)))
            {
                possibleDirection.Add(Directions.Top);
            }
            if (point.x<n-1&&_matrix[point.x+1, point.y] != i && IsHeadOrTail(new IntPoint(point.x+1, point.y)))
            {
                possibleDirection.Add(Directions.Bottom);
            }

            return possibleDirection;
        }

        private bool IsHeadOrTail(IntPoint point)
        {
            if (point.x < 0 || point.y < 0) return false;
            int key = _matrix[point.x, point.y];
            return (_wordsPlaceHolders[key][0] == point ||
                    _wordsPlaceHolders[key][_wordsPlaceHolders[key].Count - 1] == point);
        }

        private bool Isready(int minLength)
        {
            foreach (var k in _wordsPlaceHolders.Keys)
            {
                if (_wordsPlaceHolders[k].Count < minLength)
                {
                    return false;
                }
            }

            return true;
        }
        private void InitStart()
        {
            int index = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    _matrix[i, j] = index;
                    List < IntPoint > oneElemList= new List<IntPoint>();
                    oneElemList.Add(new IntPoint(i,j));
                    _wordsPlaceHolders.Add(index,oneElemList);
                    index++;
                }
            }

            

        }

        public class IntPoint
        {
            public IntPoint(int _x, int _y)
            {
                x = _x;
                y = _y;
            }
            public int x { get; set; }
            public int y { get; set; }

            public static bool operator ==(IntPoint a, IntPoint b)
            {
                return (a.x == b.x && a.y == b.y);
            }

            public static bool operator !=(IntPoint a, IntPoint b)
            {
                return !(a == b);
            }
			public override string ToString()
			{
                return x.ToString() + y.ToString();
			}
		}
    }
}
