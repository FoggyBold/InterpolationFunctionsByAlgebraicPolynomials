using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._1laba
{
    internal class Program
    {
        static void Main()
        {
            const int n = 5;
            const double a = 1;
            const double b = 2;

            double[,] matrix = generate(n + 1, a, b);

            Console.WriteLine("Исходная табличная функция");
            print(matrix);

            Console.Read();
        }

        private static double[,] generate(int n, double a, double b)
        {
            double[,] result = new double[2, n];

            //uniformPartitioning(result, a, b, n);
            ChebyshevPartition(result, a, b, n);

            return result;
        }

        private static void ChebyshevPartition(double[,] matrix, double a, double b, int size)
        {
            for (int i = 0; i < size; ++i)
            {
                matrix[0, i] = (a + b) / 2 - (b - a) * Math.Cos((2 * i + 1) * Math.PI / (2 * size + 2)) / 2;
                matrix[1, i] = f(matrix[0, i]);
            }
        }

        private static void uniformPartitioning(double[,] matrix, double a, double b, int size)
        {
            for (int i = 0; i < size; ++i)
            {
                matrix[0, i] = a + (b - a) * i / size;
                matrix[1, i] = f(matrix[0, i]);
            }
        }

        private static double f(double x)
        {
            return 0.6 + 0.5 * Math.Log(x);
        }

        private static void print(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    Console.Write(Math.Round(matrix[i, j], 3) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
