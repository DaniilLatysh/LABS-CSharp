using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace LabWork1
{
    class Task0
    {
        static void Main(string[] args)
        {


            int[] arrTemp = Input();

            Console.WriteLine("\n Array: ");

            Output(arrTemp);

            Console.WriteLine("\n\n Inverting nums: ");

            Invert(arrTemp);

            Output(arrTemp);

            Console.WriteLine("\n");

           


        }



        static int[] Input()
        {

            int size = 0;

            Console.Write(" Input array size: ");

            try
            {
                size = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("\n You must input number! \n");
                Input();
            }

            int[] arr = new int[size];


            for (int i = 0; i < size; i++)
            {

                Console.Write(" Input item [{0}]: ", i);

                try
                {
                    arr[i] = int.Parse(Console.ReadLine());
                }
                catch
                {
                    arr[i] = 0;
                }

            }



            return arr;

        }


        static void Output(int[] arr)
        {

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(" {0}", arr[i]);
            }


        }

        static void Invert(int[] arr)
        {

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 0)
                {
                    arr[i] = -arr[i];
                }
            }

        }

    }

}
