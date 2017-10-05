using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace ForForeachTest
{
    class Program
    {
        const int Size = 10000;
        const int Iterations = 100000;
        const int WarmUpIterations = 10000;



        static void Main()
        {
            Stopwatch sw;
            double[] arr = new double[Size];
            double sum = 0, sum1 = 0;
            Random rng = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rng.NextDouble();
            }
            List<double> list = new List<double>(arr);

            Action<string, Action> runTest = (str, func) =>
            {
                sum = 0; sum1 = 0;
                // warm up
                for (var i = 0; i < WarmUpIterations; i++)
                {
                    func();
                }

                sum = 0; sum1 = 0;
                sw = Stopwatch.StartNew();
                for (int i = 0; i < Iterations; i++)
                {
                    func();
                }
                sw.Stop();
                Console.WriteLine($"Results: {sum} {sum1}");
                Console.WriteLine(str, sw.ElapsedMilliseconds);
               // GC.Collect();
            };

            runTest("warm up {0}", () =>
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    sum += arr[j];
                    sum1 += arr[j] * arr[j];
                }
            });

            runTest("ARR For loop: {0}", () =>
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    sum += arr[j];
                    sum1 += arr[j] * arr[j];
                }
            });

            runTest("ARR Foreach loop: {0}", () =>
            {
                foreach (double d in arr)
                {
                    sum += d;
                    sum1 += d * d;
                }
            });

            runTest("List For loop: {0}", () =>
            {
                for (int j = 0; j < list.Count; j++)
                {
                    sum += list[j];
                    sum1 += list[j] * list[j];
                }
            });

            runTest("List Foreach loop: {0}", () =>
            {
                foreach (double d in list)
                {
                    sum += d;
                    sum1 += d * d;
                }
            });
        }
    }
}
