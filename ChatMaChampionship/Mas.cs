using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMaChampionship
{
    public static class Mas
    {
        public static IEnumerable<double> DumbMovingAverage(IList<double> list, int period)
        {
            int start = 0;
            int end = list.Count - period + 1;
            while (start < end)
            {
                yield return list.Skip(start).Take(period).Average();
                start++;
            }
        }

        public static IEnumerable<double> MovingAverageIlist(IList<double> list, int period)
        {
            var doubles = new double[period];
            double fraction = 1.0 / period;
            double sum = 0;
            for (int i = 0; i < period; i++)
            {
                double @double = list[i] * fraction;
                doubles[i] = @double;
                sum += @double;
            }
            yield return sum;
            int j = period;
            int index = 0;
            while (j < list.Count)
            {
                double old = doubles[index];
                sum -= old;
                double @new = list[j] * fraction;
                doubles[index] = @new;
                sum += @new;
                yield return sum;
                j++;
                index++;
                if (index == period)
                    index = 0;
            }
        }

        public static IEnumerable<double> MovingAverageArray(double[] list, int period)
        {
            var doubles = new double[period];
            double fraction = 1.0 / period;
            double sum = 0;
            for (int i = 0; i < period; i++)
            {
                double @double = list[i] * fraction;
                doubles[i] = @double;
                sum += @double;
            }
            yield return sum;
            int j = period;
            int index = 0;
            int length = list.Length;
            while (j < length)
            {
                double old = doubles[index];
                sum -= old;
                double @new = list[j] * fraction;
                doubles[index] = @new;
                sum += @new;
                yield return sum;
                j++;
                index++;
                if (index == period)
                    index = 0;
            }
        }

        public static IEnumerable<double> MovingAverageTravis(HashSet<double> list, int period)
        {
            int index = 0;
            int total = period < list.Count ? period : list.Count;
            double sum = 0;
            double[] waste = new double[total];
            double fraction = 1.0 / total;
            foreach (double d in list)
            {
                sum += d * fraction;
                int offset = index % total;
                if (index >= total)
                {
                    sum -= waste[offset];
                }
                waste[offset] = d * fraction;
                if (index >= total - 1)
                {
                    yield return sum;
                }
                index++;
            }
        }

        public static IEnumerable<double> MovingAverageKendall(IEnumerable<double> list, int window)
        {
            Queue<double> buffer = new Queue<double>(window);
            int i = 0;
            foreach (double item in list)
            {
                if (i < window)
                {
                    i++;
                }
                else
                {
                    buffer.Dequeue();
                }
                buffer.Enqueue(item);
                if (i == window)
                {
                    yield return buffer.Average();
                }
            }
        }

        public static IEnumerable<double> MovingAverageHeap(double[] list, int period)
        {
            var result = new double[1 + list.Length];
            var doubles = new double[period];
            var length = list.Length;
            double fraction = 1.0 / period;
            double sum = 0;
            for (int i = 0; i < period; i++)
            {
                double @double = list[i] * fraction;
                doubles[i] = @double;
                sum += @double;
            }
            result[0] = sum;
            int j = period;
            int k = 0;
            int index = 0;
            while (j < length)
            {
                double old = doubles[index];
                sum -= old;
                double @new = list[j] * fraction;
                doubles[index] = @new;
                sum += @new;
                result[++k] = sum;
                j++;
                index++;
                if (index == period)
                    index = 0;
            }
            return result;
        }
        public static unsafe IEnumerable<double> MovingAverageStack(double[] list, int period)
        {
            var result = new double[1 + list.Length];
            var doubles = stackalloc double[period];
            var length = list.Length;
            fixed (double* listPtr = list, resultPtr = result)
            {
                double fraction = 1.0 / period;
                double sum = 0;
                for (int i = 0; i < period; i++)
                {
                    double @double = listPtr[i] * fraction;
                    doubles[i] = @double;
                    sum += @double;
                }
                resultPtr[0] = sum;
                int j = period;
                int k = 0;
                int index = 0;
                while (j < length)
                {
                    double old = doubles[index];
                    sum -= old;
                    double @new = listPtr[j] * fraction;
                    doubles[index] = @new;
                    sum += @new;
                    resultPtr[++k] = sum;
                    j++;
                    index++;
                    if (index == period)
                        index = 0;
                }
            }
            return result;
        }
    }
}
