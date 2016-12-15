using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Task7
{
    class Program
    {
        static int[] delChet(int[] arr) {

             int calc = 0;

             for (int i = 0; i < arr.Length; i++) {
                 if (arr[i]/2 == 0) {
                     calc++;
                 }
             }


            int[] newArr = new int[calc];

            var newArrVar = arr.Where(n => n % 2 == 0);

            newArr = newArrVar.ToArray();


            Console.WriteLine();

            return newArr;

        }

        static void Output(int[] arr) {

            Console.WriteLine();

            for (int i = 0; i < arr.Length; i++) {
                Console.Write(" {0} ", arr[i]);
            }

        }

        static int[] addElementAfter(int[] arr)
        {

            int val = 0, num = 0;

        Flag:
            try
            {
                Console.Write("\n Input value: ");
                val = int.Parse(Console.ReadLine());


                Console.Write("\n Input number: ");
                num = int.Parse(Console.ReadLine()); 

            }
            catch
            {
                Console.WriteLine(" You must input number! ");
                goto Flag;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == val)
                    continue;
                if (arr[i] % 10 == num)
                {
                    Array.Resize(ref arr, arr.Length + 1);
                    for (int j = arr.Length - 1; j >= i + 1; j--)
                    {
                        arr[j] = arr[j - 1];
                    }
                    arr[i + 1] = val;
                }
            }
            return arr;
        }

        static int[] dellDupl(int[] arr)
        {
            int[] massWork = new int[0];
            for (int i = 0; i < arr.Length; i++)
            {
                if (!massWork.Contains(arr[i]))
                {
                    Array.Resize(ref massWork, massWork.Length + 1);
                    massWork[massWork.Length - 1] = arr[i];
                }
            }
            arr = massWork;

            return arr;
        }

        static int[] dellZero(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 0)
                {
                    delElem(1, i, arr);
                }
            }
            return arr;
        }

        static void delElem(int kol_el, int pos, int[] arr)
        {
            for (int i = pos; i < arr.Length - kol_el; i++)
                arr[i] = arr[i + kol_el];
            Array.Resize(ref arr, arr.Length - kol_el);
        }




        static void Main(string[] args)
        {

            int num = 0;
            Flag:
            try
            {
                Console.Write("\n Input array size: ");
                num = int.Parse(Console.ReadLine());

            }
            catch
            {
                Console.WriteLine(" You must input number! ");
                goto Flag;
            }

            
            Random rand = new Random();
            int[] arr = new int[num];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(0, 10);
            }

            Output(arr);

            Console.ReadKey();

            arr = delChet(arr);

            Output(arr);

            Console.ReadKey();

            arr = addElementAfter(arr);

            Output(arr);

            Console.ReadKey();

            arr = dellDupl(arr);

            Output(arr);

            Console.ReadKey();

            arr = dellZero(arr);

            Output(arr);

            Console.ReadKey();


        }
    }



}
