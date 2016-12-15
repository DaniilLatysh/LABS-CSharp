using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LabWork3_2
{

    abstract class Auto {
        protected string mark;
        protected int number;
        protected int speed;
        protected double lifting;

        public Auto(string mark, int number, int speed, double lifting) {
            this.mark = mark;
            this.number = number;
            this.speed = speed;
            this.lifting = lifting;
        }

        abstract public void Show();
    }

    class Car : Auto {

        public Car(string mark, int number, int speed, double lifting) : base(mark, number, speed, lifting) {

        }
        public override void Show() {
            Console.WriteLine(" \n CAR     {0}       {1}     {2}     {3}    x", mark, number, speed, lifting);
        }
    }

    class Moto : Auto {

        bool sidecar;

        public Moto(string mark, int number, int speed, double lifting, bool sidecar) : base(mark, number, speed, lifting) {
            this.sidecar = sidecar;
            if (!sidecar) {
                this.lifting = 0;
            }
        }
        public override void Show()
        {
            Console.WriteLine(" \n MOTO    {0}       {1}     {2}     {3}    {4}", mark, number, speed, lifting, sidecar);
        }

    }

    class Truck : Auto {

        bool trailer;
        public Truck(string mark, int number, int speed, double lifting, bool trailer) : base(mark, number, speed, lifting) {
            this.trailer = trailer;
            if (trailer) {
                this.lifting = lifting * 2;
            }
        }

        public override void Show()
        {
            Console.WriteLine(" \n TRUCK    {0}       {1}     {2}     {3}    {4}", mark, number, speed, lifting, trailer);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool sidecar = false, trailer = false;

                var list = new List<object>(); // List of objects 


                StreamReader f = new StreamReader("data.txt", Encoding.GetEncoding(1251));
               
                string line;

                while ((line = f.ReadLine()) != null) {
                    string[] var = line.Split(' ');
                    if (String.Equals(var[0], "Car")) {
                        if (var.Length > 5) {
                            throw new Exception(" Incorrect line!");
                        }
                        list.Add(new Car(var[1], Convert.ToInt32(var[2]), Convert.ToInt32(var[3]), Convert.ToDouble(var[4])));
                    }
                    if (String.Equals(var[0], "Moto"))
                    {
                        if (var.Length > 6)
                        {
                            throw new Exception(" Incorrect line!");
                        }
                        if (Convert.ToInt32(var[5]) == 1) {
                            sidecar = true;
                        } else sidecar = false;

                        list.Add(new Moto(var[1], Convert.ToInt32(var[2]), Convert.ToInt32(var[3]), Convert.ToDouble(var[4]), sidecar));
                            
                    }
                    if (String.Equals(var[0], "Truck"))
                    {
                        if (var.Length > 6)
                        {
                            throw new Exception(" Incorrect line!");
                        }
                        if (Convert.ToInt32(var[5]) == 1)
                        {
                            trailer = true;
                        }
                        else trailer = false;

                        list.Add(new Truck(var[1], Convert.ToInt32(var[2]), Convert.ToInt32(var[3]), Convert.ToDouble(var[4]), trailer));

                    }
                }

                Console.WriteLine(" \n TYPE |    MARK    | NUMBER | SPEED | LIFTING |  BAG  ");
                foreach (Auto lists in list)
                {
                    lists.Show();
                }

            }
            catch (OutOfMemoryException e)
            { Console.WriteLine(e.Message); }
            catch (FileNotFoundException e)
            { Console.WriteLine(e.Message); }
            catch (IOException e)
            { Console.WriteLine(e.Message); }
            catch (ArgumentException e)
            { Console.WriteLine(e.Message); }
            catch (Exception error)
            { Console.WriteLine(error.Message); }
            Console.ReadKey();
        }
    } 
    
}
