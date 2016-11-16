using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        

        /*
         * Ввод данных
         */

        static int[][] Input(out int line, out int column) {

            Random rand = new Random();

            try{

                Console.Write(" Input line size: ");

                line = int.Parse(Console.ReadLine());

                Console.Write(" Input column size: ");

                column = int.Parse(Console.ReadLine());

            }catch{

                Console.Write(" You must input number! ");
                Input(out line, out column);

            }

            int[][] arr = new int[line][];

            for (int i = 0; i < line; ++i) {

                arr[i] = new int[column];

                for (int j = 0; j < column; ++j) {
                    arr[i][j] = rand.Next(0,10);
                }

            }
            

            
            return arr;
        }

        /*
         * Вывод данных
         */

        static void Output(int[][] arr, int line, int column) {

            Console.WriteLine();

            for (int i = 0; i < line; i++, Console.WriteLine()) {
                for (int j = 0; j < column; j++) {
                    Console.Write("{0,5} ", arr[i][j]);
                }
            }

        }

        /*
         * 5.	Поменять местами столбцы по правилу: первый с последним, второй с предпоследним и т.д.
         */

        static void Flip(int[][] arr, int line, int column) {
            

            for (int i = 0; i < line; i++)
            {
                    Array.Reverse(arr[i]);
            }

            Output(arr, line, column);

        }

        static void Main(string[] args)
        {

            int line, column;
            int[][] arrTemp = Input(out line, out column);

            Output(arrTemp, line, column);

            Console.ReadKey();

            Flip(arrTemp, line, column);

            Console.ReadKey();
        }

    }
}
