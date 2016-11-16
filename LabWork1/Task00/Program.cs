using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1
{
    class Task00
    {

  
        static int[,] Input()
        {

            int line = 0, column = 0;



            try
            {
                Console.Write(" Input line size: ");
                line = int.Parse(Console.ReadLine());

                Console.Write(" Input column size: ");
                column = int.Parse(Console.ReadLine());

            }
            catch
            {
                Console.WriteLine("\n You must input number! \n");
                Input();
            }

            int[,] arr = new int[line, column];


            for (int i = 0; i < line; i++)
            {

                for (int j = 0; j < column; j++)
                {

                    try
                    {
                        Console.Write(" [{0}][{1}]", line, column);
                        arr[i, j] = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        arr[i, j] = 0;
                    }
                }

            }

            return arr;

        }


        static void Output(int[,] arr)
        {

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write("{0,5} ", arr[i, j]);

                }
                Console.WriteLine();
            }


        }

        static void Invert(int[,] arr)
        {

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i,j] > 0)
                    {
                        arr[i,j] = -arr[i,j];
                    }
                }
            }

        }


        static void Main(string[] args)
        {


            int[,] arrTemp = Input();

            Console.WriteLine("\n Array: ");

            Output(arrTemp);

            Console.WriteLine("\n\n Inverting nums: ");

            Invert(arrTemp);

            Output(arrTemp);

            Console.WriteLine("\n");

            Console.ReadKey();

        }
    }
}
