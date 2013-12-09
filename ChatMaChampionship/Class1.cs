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
        [TestCase(5000000, 50)]
        [TestCase(500000, 350)]
        [TestCase(50000, 35)]
        [TestCase(5000, 350)]
        public void Run(int n, int p)
        {
            double[] d1 = RandomDoubles(n).ToArray();
            double[] d2 = d1.ToArray();
            HashSet<double> set = new HashSet<double>(d1);
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 5; i++)
            {
                stopwatch.Restart();
                var ma1 = Mas.DumbMovingAverage(d1, p);
                ma1.Count();
                TimeSpan t1 = stopwatch.Elapsed;
                Console.WriteLine("Johan ({0}, {1}) took {2} ms", n, p, stopwatch.ElapsedMilliseconds);


                stopwatch.Restart();
                var ma2 = Mas.MovingAverageHeap(d2, p);
                ma2.Count();
                TimeSpan t2 = stopwatch.Elapsed;
                Console.WriteLine("Travis ({0}, {1}) took {2} ms", n, p, stopwatch.ElapsedMilliseconds);

                Console.WriteLine("Improvement: {0:n2}x", t1.TotalMilliseconds / t2.TotalMilliseconds);
                AreEqual(ma1.ToArray(), ma2.ToArray(), 0.001);
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

        public static void AreEqual(double[] first, double[] other, double treshold)
        {
            Assert.AreEqual(first.Count(), other.Count());
            for (int i = 0; i < first.Count(); i++)
            {
                Assert.IsTrue(Math.Abs(first[i] - other[i]) < treshold);
            }
        }
    }
}
