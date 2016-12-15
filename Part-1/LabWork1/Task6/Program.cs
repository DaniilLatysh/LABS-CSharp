using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    class Program
    {

        /*
         *      5.	Удалить из сообщения все слова, содержащие данный символ (без учета регистра).
         * */


        static void Output(StringBuilder str)
        {
            Console.WriteLine("\n Your text:\n {0}", str);
        }



        static void cheackRepead(StringBuilder strB, char ch)
        {

            

            string str = strB.ToString();

            char[] div = {' '};

            int count = 0;

            string[] stack = str.Split(div);

            Console.WriteLine("\n Result:");

            for (int i = 0; i < stack.Length; i++) {

                char[] temp = new char[stack[i].Length];

                temp = stack[i].ToCharArray();

                for (int j = 0; j < temp.Length; j++) {
                    if (temp[j].Equals(ch))
                    {
                        stack[i] = "";
                    }
                }

                Console.Write(" {0}", stack[i]);
            }


        }





        static void Main(string[] args)
        {
            char ch;
            Flag:
            try
            {
                Console.Write("\n Input your char: ");
                ch = char.Parse(Console.ReadLine());
            }
            catch {
                Console.WriteLine("\n You must input 1 symbhol! ");
                goto Flag;

            }
            Console.Write("\n Input your text: ");
            StringBuilder strB = new StringBuilder(Console.ReadLine());

            Output(strB);

            Console.ReadKey();

            cheackRepead(strB, ch);

            Console.ReadKey();

        }
    }
}
