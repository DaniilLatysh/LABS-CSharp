using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork5
{

    struct Auto : IComparable<Auto> {
        private string Mark;
        private string Number;
        private string NameOfOwner;
        private int YearBuy;

        public Auto(string Mark, string Number, string NameOfOwner, int YearBuy) {
            this.Mark = Mark;
            this.Number = Number;
            this.NameOfOwner = NameOfOwner;
            this.YearBuy = YearBuy;
        }

        public int CompareTo(Auto obj)
        {
            Auto a = (Auto)obj;

            throw new NotImplementedException();
        }

        public override string ToString()
        {
            Console.WriteLine("\n Mark: " + Mark);
            Console.WriteLine(" Number: " + Number);
            Console.WriteLine(" NameOfOwner: " + NameOfOwner);
            Console.WriteLine(" YearBuy: " + YearBuy);
            return Mark + " " + Number + " " + NameOfOwner + " " + YearBuy.ToString();
        }

    }

    class Program
    {
        public static void AllCars(out Auto[] auto)
        {
            string path = "input.txt";           
            int size = File.ReadAllLines(path).Length;

            auto = new Auto[size];
            StreamReader f = new StreamReader(path, Encoding.GetEncoding(1251));

            for (int i = 0; i < size; i++) {
                string line = f.ReadLine();
                string[] var = line.Split(' ');
                auto[i] = new Auto(var[0], var[1], var[2], Convert.ToInt32(var[3]));
            }
           
            f.Close();

            foreach (Auto a in auto) {
                a.ToString();
            }
        }
        

        static void Main(string[] args)
        {
            try
            {
                Auto[] auto;
                AllCars(out auto);



                Console.Write("\n\t Insert year:");
                int year = Convert.ToInt32(Console.ReadLine());

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    
}
