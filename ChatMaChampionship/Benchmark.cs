using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChatMaChampionship
{
    class Benchmark
    {
        private List<Competitor> _competitors;

        public Benchmark()
        {
            _competitors = new List<Competitor>
            {
                new Competitor("Rightfold", (x, p) => Mas.MovingAveragerightfold((double[]) x, p),x=>x.ToArray()),
                new Competitor("Travis", (x, p) => Mas.MovingAverageTravis((HashSet<double>) x, p),x=>new HashSet<double>(x)),
                new Competitor("Johan", (x, p) => Mas.MovingAverageIlist((IList<double>) x, p),x=>x.ToArray()),
            };
        }

        [TestCase(5000000, 50)]
        [TestCase(500000, 350)]
        [TestCase(50000, 35)]
        [TestCase(5000, 350)]
        public void Run(int n, int p)
        {
            double[] doubles = RandomDoubles(n).ToArray();
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 5; i++)
            {

                foreach (var competitor in _competitors)
                {
                    stopwatch.Restart();
                    var data = competitor.Transform.Invoke(doubles);
                    competitor.Code.Invoke(data, p).Count();
                    Console.WriteLine("{0} ({1}, {2}) took {3} ms",competitor.Name, n, p, stopwatch.ElapsedMilliseconds);
                }
            }
        }

        [Test]
        public void Check()
        {
            int n = 5000;
            int p = 35;
            var doubles = RandomDoubles(n).ToArray();
            var expected = Mas.DumbMovingAverage(doubles, p).ToArray();
            foreach (var competitor in _competitors)
            {
                var data = competitor.Transform.Invoke(doubles);
                var reslut = competitor.Code.Invoke(data, p).ToArray();
                if (!AreEqual(expected, reslut, 0.01))
                {
                    Console.WriteLine(competitor.Name +" failed");
                }
                else
                {
                    Console.WriteLine(competitor.Name + " passed");
                }
            }
        }

        private IEnumerable<double> RandomDoubles(int n)
        {
            var random = new Random();
            for (int i = 0; i < n; i++)
            {
                yield return random.NextDouble();
            }
        }

        public static bool AreEqual(double[] first, double[] other, double treshold)
        {
            if (first.Length != other.Length)
                return false;
            for (int i = 0; i < first.Count(); i++)
            {
                if (Math.Abs(first[i] - other[i]) > treshold)
                    return false;
            }
            return true;
        }
    }
}
