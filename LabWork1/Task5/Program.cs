using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class Program
    {

        /*
         * 5.	определяет, имеются ли в строке два соседствующих одинаковых символа;
         * */


        static void Output(StringBuilder str) {

            Console.WriteLine("\n Your text:\n {0}", str);
        }



        static void cheackRepead(StringBuilder strB) {


            int summ = 0, len;

            string str = strB.ToString();

            len = str.Length - 1;

            char[] arr = new char[len];

            arr = str.ToCharArray();



            for (int i = 0; i < len; i++)
            {
                if (arr[i].Equals(arr[i + 1]))
                {
                    summ++;
                }
            }

            Console.WriteLine("\n Repead: {0} ", summ);

        }





        static void Main(string[] args)
        {

            try
            {
                Console.Write("\n Input your text: ");

                StringBuilder strB = new StringBuilder(Console.ReadLine());

                Output(strB);

                Console.ReadKey();

                cheackRepead(strB);

                Console.ReadKey();
            }
            catch (Exception e) {
                Console.WriteLine("Exeption {0}", e);
            }

        }
    }
}
