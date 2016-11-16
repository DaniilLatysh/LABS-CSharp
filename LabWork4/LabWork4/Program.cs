using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace LabWork4
{
    class Program
    {

        static void Search(string mask, DateTime dateMin, DateTime dateMax) {

            string[] files = Directory.GetFiles(@"D:\TestFolder", "*"+mask+"*", SearchOption.AllDirectories);
            StreamWriter sw = new StreamWriter("Log.txt", true, Encoding.Default);

            foreach (var file in files)
            {
                DateTime lastChande = File.GetCreationTime(file);


                if (lastChande >= dateMin && lastChande <= dateMax)
                {
                    Console.WriteLine(" {0}, {1}", file, lastChande);
                    sw.WriteLine(file + " Date: " + lastChande);
                }

            }
            sw.Close();
                
        }



        static void Main(string[] args)
        {

            
            
            string mask = "*.*";
            int year = 1991, month = 01 , day = 1;

            DateTime dateMin, dateMax;

            try
            {

                Console.Write("\n Input mask for search: ");
                mask = Console.ReadLine();

                Console.WriteLine("\n Input date FROM ");
                Console.Write(" Year: ");
                year = int.Parse(Console.ReadLine());
                Console.Write(" Month: ");
                month = int.Parse(Console.ReadLine());
                Console.Write(" Day: ");
                day = int.Parse(Console.ReadLine());

                dateMin = new DateTime(year, month, day);

                Console.WriteLine(" ------------------------------------ ");

                Console.WriteLine(" Input date TO ");
                Console.Write(" Year: ");
                year = int.Parse(Console.ReadLine());
                Console.Write(" Month: ");
                month = int.Parse(Console.ReadLine());
                Console.Write(" Day: ");
                day = int.Parse(Console.ReadLine());

                dateMax = new DateTime(year, month, day);

                Search(mask, dateMin, dateMax);

            } catch(Exception e) {

                Console.WriteLine(" --------------------------------------------------------------------- ");
                Console.WriteLine("\n Exception: {0} \n", e.Message);
                Console.WriteLine(" --------------------------------------------------------------------- ");
                Console.ReadKey();
            }

            Console.ReadKey();

        }
    }
}
