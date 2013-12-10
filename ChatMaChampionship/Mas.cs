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
            var window = new double[period];
            double fraction = 1.0 / period;
            double sum = 0;
            for (int i = 0; i < period; i++)
            {
                double d = list[i] * fraction;
                window[i] = d;
                sum += d;
            }
            yield return sum;
            int j = period;
            int index = 0;
            while (j < list.Count)
            {
                double old = window[index];
                sum -= old;
                double newGuy = list[j] * fraction;
                window[index] = newGuy;
                sum += newGuy;
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

        public static unsafe IEnumerable<double> MovingAveragerightfoldEvil(double[] listArray, int period) {
            var length = listArray.Length;
            var total = period < length ? period : length;
            var resultArray = new double[length];
            var resultIndex = 0;
            fixed (double* list = listArray, result = resultArray) {
                var index = 0;
                var sum = 0.0;
                var waste = new double[total];
                var fraction = 1.0 / total;
                for (var i = 0; i < length; ++i) {
                    var d = list[i];
                    sum += d * fraction;
                    var offset = index % total;
                    if (index >= total) {
                        sum -= waste[offset];
                    }
                    waste[offset] = d * fraction;
                    if (index >= total - 1) {
                        result[resultIndex++] = sum;
                    }
                    index++;
                }
            }
            return new ArraySegment<double>(resultArray, 0, resultIndex);
        }

        public static unsafe IEnumerable<double> MovingAveragerightfoldMoreEvil(double[] listArray, int period) {
            var length = listArray.Length;
            var total = period < length ? period : length;
            var resultArray = new double[length];
            var resultIndex = 0;
            fixed (double* list = listArray, result = resultArray) {
                var index = 0;
                var sum = 0.0;
                var waste = stackalloc double[total];
                var fraction = 1.0 / total;
                for (var i = 0; i < length; ++i) {
                    var d = list[i];
                    sum += d * fraction;
                    var offset = index % total;
                    if (index >= total) {
                        sum -= waste[offset];
                    }
                    waste[offset] = d * fraction;
                    if (index >= total - 1) {
                        result[resultIndex++] = sum;
                    }
                    index++;
                }
            }
            return new ArraySegment<double>(resultArray, 0, resultIndex);
        }

        public static unsafe IEnumerable<double> MovingAveragerightfoldMostEvil(double[] listArray, int period) {
            var length = listArray.Length;
            var total = period < length ? period : length;
            var resultArray = new double[length + total];
            var resultIndex = 0;
            fixed (double* list = listArray, result = resultArray) {
                var index = 0;
                var sum = 0.0;
                var waste = result + length;
                var fraction = 1.0 / total;
                for (var i = 0; i < length; ++i) {
                    var d = list[i];
                    sum += d * fraction;
                    var offset = index % total;
                    if (index >= total) {
                        sum -= waste[offset];
                    }
                    waste[offset] = d * fraction;
                    if (index >= total - 1) {
                        result[resultIndex++] = sum;
                    }
                    index++;
                }
            }
            return new ArraySegment<double>(resultArray, 0, resultIndex);
        }

        public static unsafe IEnumerable<double> MovingAveragerightfoldMoreEvilThanMoreEvilButLessEvilThanMostEvil(double[] listArray, int period) {
            var length = listArray.Length;
            var total = period < length ? period : length;
            var resultArray = new double[length + total];
            var resultIndex = 0;
            fixed (double* list = listArray, result = resultArray) {
                var index = 0;
                var sum = 0.0;
                var waste = result + length;
                var fraction = 1.0 / total;
                for (var i = 0; i < length; ++i) {
                    var d = list[i];
                    sum += d * fraction;
                    var offset = index % total;
                    if (index >= total) {
                        sum -= waste[offset];
                    }
                    waste[offset] = d * fraction;
                    if (index >= total - 1) {
                        result[resultIndex++] = sum;
                    }
                    index++;
                }
            }
            var realResult = new double[resultIndex];
            Array.Copy(resultArray, 0, realResult, 0, resultIndex);
            return realResult;
        }
    }
}
