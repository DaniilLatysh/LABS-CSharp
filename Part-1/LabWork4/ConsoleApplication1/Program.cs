using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace LabWork4_2
{
    class Program
    {

        static void Search(string path, string strIn, string strCh, string mask)
        {
            int counter = 0;
            string[] files = Directory.GetFiles(@"" + path + "", "*" + mask + "*", SearchOption.AllDirectories);

            StreamWriter sw = new StreamWriter("Log.txt", true, Encoding.Default);
            


            foreach (var el in files)
            {
                StreamReader read = new StreamReader(el, Encoding.Default); // open file

                string line = read.ReadToEnd();          // read all from file
                read.Close(); //close file

                StreamWriter write = new StreamWriter(el);  //ready to write
                Regex regText = new Regex(strIn);   //what we need replace?
                line = regText.Replace(line, strCh);  //replace
                write.Write(line);  //write

                counter++;

                write.Close();

            }

            Console.WriteLine("Complite! File's was update {0}", counter);

        }

        static void Main(string[] args)
        {

            try
            {
                Console.Write(" Path: ");
                string path = Console.ReadLine();

                if (path[path.Length - 1] != '\\')
                    path += '\\';

                Console.Write(" Mask: ");
                string mask = Console.ReadLine();

                Console.Write(" Find text: ");
                string strIn = Console.ReadLine();

                Console.Write(" Change text: ");
                string strCh = Console.ReadLine();

                Search(path, strIn, strCh, mask);

            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }

}