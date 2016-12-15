using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        
        /**
         * Input array
         */

        static int[] Input()
        {

            int num = 0;

            Console.Write("\n Input size array: ");

            try
            {
                num = int.Parse(Console.ReadLine());
            }
            catch {

                Console.WriteLine(" You must input number! ");

                Input();
            }

            int[] arr = new int[num];

            for (int i = 0; i < arr.Length; i++) {
                try
                {
                    Console.Write(" [{0}] ", i);
                    arr[i] = int.Parse(Console.ReadLine());
                }
                catch {
                    arr[i] = 0;
                }
            }

           

            return arr;
        }

        /**
         * Output array
         */

        static void Output(int[] arr) {

            Console.WriteLine();

            for (int i = 0; i < arr.Length; i++) {
                
                Console.Write(" {0} ",arr[i]);

            }
            
        }


        /**
        *   05 Подсчитать среднее арифметическое элементов.
        */

        static void Task05(int[] arr)
        {
            double summ = 0;

            for (int i = 0; i < arr.Length; i++)
            {

                summ += arr[i];

            }

            Console.WriteLine(" Arithmetic mean: {0}", summ/2);

        }


        static int[,] InputM()
        {
            Console.WriteLine();
            int line = 0, column = 0;
            Random rand = new Random();

            try
            {

                Console.Write(" Input line size: ");

                line = int.Parse(Console.ReadLine());

                Console.Write(" Input column size: ");

                column = int.Parse(Console.ReadLine());

            }
            catch
            {

                Console.Write(" You must input number! ");
                InputM();

            }

            int[,] arr = new int[line, column];

            for (int i = 0; i < line; ++i)
            {

                for (int j = 0; j < column; ++j)
                {
                    arr[i,j] = rand.Next(0, 10);
                }

            }



            return arr;
        }

        /*
         * Вывод данных
         */

        static void Output(int[,] arr)
        {

            Console.WriteLine();

            for (int i = 0; i < arr.GetLength(0); i++, Console.WriteLine())
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(" {0,5} ", arr[i,j]);
                }
            }

        }

        static void Task55(int[,] arr)
        {
            double summ = 0;

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    summ += arr[i, j];
                }
            }

            Console.WriteLine(" Arithmetic mean: {0}", summ / 2);

            Console.ReadKey();
        }


        static void Main(string[] args)
        {

            int[] arrTemp = Input();

            Output(arrTemp);

            Task05(arrTemp);

            int[,] arrMult = InputM();

            Output(arrMult);

            Task55(arrMult);

            Console.ReadKey();
        }

    }
}
