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
        private int Mileage;

        public Auto(string Mark, string Number, string NameOfOwner, int YearBuy, int Mileage) {
            this.Mark = Mark;
            this.Number = Number;
            this.NameOfOwner = NameOfOwner;
            this.YearBuy = YearBuy;
            this.Mileage = Mileage;
        }

      
        public int GetYearBuy {
            get {
                return this.YearBuy;
            }
        }
        public int CompareTo(Auto obj)
        {
            Auto a = (Auto)obj;
            if (a.Mileage > this.Mileage)
            {
                return 1;
            }
            if (a.Mileage < this.Mileage)
            {
                return -1;
            }
            return 0;
        }

        public override string ToString()
        {
            Console.WriteLine("\n Mark: " + Mark);
            Console.WriteLine(" Number: " + Number);
            Console.WriteLine(" NameOfOwner: " + NameOfOwner);
            Console.WriteLine(" YearBuy: " + YearBuy);
            Console.WriteLine(" Mileage: " + Mileage);
            return Mark + " " + Number + " " + NameOfOwner + " " + YearBuy.ToString() + " " + Mileage;
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
                auto[i] = new Auto(var[0], var[1], var[2], Convert.ToInt32(var[3]), Convert.ToInt32(var[4]));
            }
           
            f.Close();

            foreach (Auto a in auto) {
                a.ToString();
            }

            
        }

        public static void Output(Auto[] auto) {

            string path = "output.txt";
            StreamWriter write = new StreamWriter(path);

            Array.Sort(auto);

            Console.Write("\n\t Insert year:");
            int year = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < auto.Length; i++) {
                if (auto[i].GetYearBuy < year) {
                    write.WriteLine(auto[i]);
                }
            }
            write.Close();
        }
        

        static void Main(string[] args)
        {
            try
            {
                Auto[] auto;
                AllCars(out auto);
                Output(auto);

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.Write("\n |!| " + e.Message + " |!| \n");
                Console.ReadKey();
            }
        }
    }

    
}
