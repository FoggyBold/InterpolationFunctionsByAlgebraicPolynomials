using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.mariuszgromada.math.mxparser;

namespace _1._1laba
{
    internal class Program
    {
        static void Main()
        {
            int n;
            double a, b;

            Console.Write("Введите n: ");
            n = int.Parse(Console.ReadLine());
            Console.Write("Введите a: ");
            a = double.Parse(Console.ReadLine());
            Console.Write("Введите b: ");
            b = double.Parse(Console.ReadLine());

            double[,] matrix = generate(n + 1, a, b);

            Console.WriteLine("Исходная табличная функция");
            print(matrix);

            double[] coefficients = calculationCoefficients(matrix, n + 1);

            Console.WriteLine(p(matrix, n + 1, coefficients, 1));

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

        private static double p(double[,] matrix, int size, double[] coefficients, double x)
        {
            double result = coefficients[0];
            for (int i = 1; i < size; ++i)
            {
                double temp = coefficients[i];
                for(int j = 0; j < i; ++j)
                {
                    temp *= x - matrix[0, j];
                }
                result += temp;
            }
            return result;
        }

        private static string p(double[,] matrix, int size, double[] coefficients)
        {
            string result = "p(x) = " + coefficients[0].ToString();
            for (int i = 1; i < size; ++i)
            {
                if (coefficients[i] >= 0)
                {
                    result += "+";
                }

                result += coefficients[i].ToString();
                for (int j = 0; j < i; ++j)
                {
                    result += "*(x";
                    result += matrix[0, j] < 0 ? "+" : "-";
                    result += Math.Abs(matrix[0, j]).ToString() + ")";
                }
            }
            return result;
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

        private static double[] calculationCoefficients(double[,] matrix, int size)
        {
            double[] coefficients = new double[size];

            double[,] tempMatrix = new double[size * 2 - 1, size];
            double[] tempX = new double[size * 2 - 1];
            for (int i = 0, j = 0; i < size * 2 - 1 && j < size; i += 2, ++j)
            {
                tempMatrix[i, 0] = matrix[1, j];
                tempX[i] = matrix[0, j];
            }

            int indent = 1;
            while (indent <= size - 1)
            {
                for (int i = indent; i < (size * 2 - 1) - indent; i += 2) 
                {
                    tempMatrix[i, indent] = (tempMatrix[i + 1, indent - 1] - tempMatrix[i - 1, indent - 1]) / (tempX[i + indent] - tempX[i - indent]);
                }
                indent++;
            }

            for (int i = 0; i < size; i++)
            {
                coefficients[i] = tempMatrix[i, i];
            }

            return coefficients;
        }
    }
}
