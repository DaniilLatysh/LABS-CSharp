using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {


        /*
         * Ввод данных
         */

        static int[][] Input(out int line, out int column)
        {

            Random rand = new Random();

            try
            {

                Console.Write("\n Input line size: ");

                line = int.Parse(Console.ReadLine());

                Console.Write(" Input column size: ");

                column = int.Parse(Console.ReadLine());

            }
            catch
            {

                Console.Write(" You must input number! ");
                Input(out line, out column);

            }

            int[][] arr = new int[line][];

            for (int i = 0; i < line; ++i)
            {

                arr[i] = new int[column];

                for (int j = 0; j < column; ++j)
                {
                    arr[i][j] = rand.Next(-10, 10);
                }

            }



            return arr;
        }

        /*
         * Вывод данных
         */

        static void Output(int[][] arr, int line, int column)
        {

            Console.WriteLine();

            for (int i = 0; i < line; i++, Console.WriteLine())
            {
                for (int j = 0; j < column; j++)
                {
                    Console.Write("{0,5} ", arr[i][j]);
                }
            }
            Console.WriteLine();

        }

        /*
         * 5.	Для каждой строки подсчитать  количество положительных элементов и записать данные в новый массив.
         */

        static void Summ(int[][] arr, int line, int column)
        {

            int[,] arrSumm = new int[line,1];
            int summ = 0;

            for (int i = 0; i < line; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {

                    if (arr[i][j] >= 0)
                    {
                        summ++;
                    }

                }

                arrSumm[i,0] = summ;
                
                Console.WriteLine(" Line {0} | Result: {1,6} ", i, arrSumm[i,0]);
                summ = 0;
            }

           


        }

        static void Main(string[] args)
        {

            int line, column;
            int[][] arrTemp = Input(out line, out column);

            Output(arrTemp, line, column);

            Console.ReadKey();

            Summ(arrTemp, line, column);

            Console.ReadKey();
        }

    }
}
