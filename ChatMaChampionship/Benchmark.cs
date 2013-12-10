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
                new Competitor("rightfoldEvil", (x, p) => Mas.MovingAveragerightfoldEvil((double[]) x, p),x=>x.ToArray()),
                new Competitor("rightfoldMoreEvil", (x, p) => Mas.MovingAveragerightfoldMoreEvil((double[]) x, p),x=>x.ToArray()),
                new Competitor("rightfoldMostEvil", (x, p) => Mas.MovingAveragerightfoldMostEvil((double[]) x, p),x=>x.ToArray()),
                new Competitor("rightfoldTerriblyEvil", (x, p) => Mas.MovingAveragerightfoldTerriblyEvil((double[]) x, p),x=>x.ToArray()),
                new Competitor("Travis", (x, p) => Mas.MovingAverageTravis((double[]) x, p),x=>x.ToArray()),
                new Competitor("Johan", (x, p) => Mas.MovingAverageIlist((IList<double>) x, p),x=>x.ToArray()),
                new Competitor("Reed", (x, p) => Mas.MovingAverageReed((double[]) x, p),x=>x.ToArray()),
                //new Competitor("Kendall", (x, p) => Mas.MovingAverageKendall( x, p),x=>x.ToArray()),
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
            foreach (var competitor in _competitors)
            {
                competitor.Results.Clear();
            }
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Pass " + i);
                foreach (var competitor in _competitors)
                {
                    var data = competitor.Transform.Invoke(doubles);
                    stopwatch.Restart();
                    competitor.Code.Invoke(data, p).Count();
                    competitor.Results.Add(stopwatch.ElapsedMilliseconds);

                }
            }
            foreach (var competitor in _competitors)
            {
                Console.WriteLine("{0} ({1}, {2}) took min: {3} max: {4} average {5} ms", competitor.Name, n, p, competitor.Results.Min(),competitor.Results.Max(),competitor.Results.Average());
            }
        }

        [Test]
        public void Check()
        {
            int n = 5000;
            int p = 35;
            var doubles = RandomDoubles(n).ToArray();
            var expected = Mas.DumbMovingAverage(doubles, p).ToArray();
            Console.WriteLine("Checking");
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
