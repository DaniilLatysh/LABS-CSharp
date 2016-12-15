using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Task02

{
    class Program
    {
        
       /**
        *   5.	Вывести на экран номера всех элементов, не совпадающих с максимальным.
        */


        static int[] Input() {

            int change = 0;
            
            try
            {
                Console.Write("\n Input array size: ");
                change = int.Parse(Console.ReadLine());
                
            }
            catch {
                Console.WriteLine(" You must input number! ");
                Input();
            }


            int[] arr = new int[change];

            Console.WriteLine();

            for (int i = 0; i < arr.Length; i++)
            {
                try
                {

                    Console.Write(" [{0}] ", i);
                    arr[i] = int.Parse(Console.ReadLine());

                }catch{
                    arr[i] = 0;
                }
            }

            return arr;

        }


        static void Output(int[] arr) {

            Console.WriteLine();

            for (int i = 0; i < arr.Length; i++) {
                Console.Write(" {0} ", arr[i]);
            }

        }

        static void Task(int[] arr) {

            Console.WriteLine("\n Sort: ");

            int max = arr.Max();

            for (int i = 0; i < arr.Length; i++) {
                if (arr[i] != max) {
                    Console.Write(" {0} ", arr[i]);
                }
            }
        }

        static void Main(string[] args)
        {
            int[] arrTemp = Input();

            Output(arrTemp);

            Task(arrTemp);

            Console.ReadKey();

        }


    }
}
