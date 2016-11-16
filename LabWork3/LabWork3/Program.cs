using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork3
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
            Console.WriteLine(" \n CAR    {0}     {1}     {2}     {3}    x", mark, number, speed, lifting);
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
            Console.WriteLine(" \n MOTO    {0}     {1}     {2}     {3}    {4}", mark, number, speed, lifting, sidecar);
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
            Console.WriteLine(" \n TRUCK    {0}     {1}     {2}     {3}    {4}", mark, number, speed, lifting, trailer);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int choise = 0;
                string mark = "NoName";
                int number = 0;
                int speed = 0;
                double lifting = 0;
                bool sidecar = false, trailer = false;

                var list = new List<object>(); // List of objects 


                while (true)
                {
                    
                    Console.Clear();
                    Console.WriteLine("\n 1 - Add Car;");
                    Console.WriteLine(" 2 - Add Moto;");
                    Console.WriteLine(" 3 - Add track;");
                    Console.WriteLine(" 4 - Show list;");
                    Console.WriteLine(" 0 - Exit;");

                    choise = int.Parse(Console.ReadLine());

                    switch (choise)
                    {
                        case 1:                                                                    /*            CAR       */
                        Flag:
                            try
                            {
                                Console.Clear();

                                Console.Write("\n Input Mark of the Car: ");
                                StringBuilder str = new StringBuilder(Console.ReadLine());
                                mark = str.ToString();

                                Console.Write("\n Input Number: ");
                                number = int.Parse(Console.ReadLine());

                                Console.Write("\n Input Speed: ");
                                speed = int.Parse(Console.ReadLine());

                                Console.Write("\n Input Lifting: ");
                                lifting = double.Parse(Console.ReadLine());

                            }
                            catch
                            {
                                Console.WriteLine("\n Wrong input! Try again! ");
                                Console.ReadKey();
                                goto Flag;
                            }

                            list.Add(new Car(mark, number, speed, lifting));
                            break;

                        case 2:
                        Flag2:                                                                    /*            MOTO       */
                            try
                            {
                                Console.Clear();

                                Console.Write("\n Input Mark of the Moto: ");
                                StringBuilder str = new StringBuilder(Console.ReadLine());
                                mark = str.ToString();

                                Console.Write("\n Input Number: ");
                                number = int.Parse(Console.ReadLine());

                                Console.Write("\n Input Speed: ");
                                speed = int.Parse(Console.ReadLine());

                                Console.Write("\n Input Lifting: ");
                                lifting = double.Parse(Console.ReadLine());

                                Console.Write("\n Has sidecar?: 1 - yes / 0 - no ");
                                byte answ = byte.Parse(Console.ReadLine());

                                if (answ == 1) sidecar = true;
                                    else sidecar = false;


                            }
                            catch
                            {
                                Console.WriteLine("\n Wrong input! Try again! ");
                                Console.ReadKey();
                                goto Flag2;
                            }

                            list.Add(new Moto(mark, number, speed, lifting, sidecar));
                            break;

                        case 3:                                                                    /*            TRUCK       */
                        Flag3:
                            try
                            {
                                Console.Clear();

                                Console.Write("\n Input Mark of the Truck: ");
                                StringBuilder str = new StringBuilder(Console.ReadLine());
                                mark = str.ToString();

                                Console.Write("\n Input Number: ");
                                number = int.Parse(Console.ReadLine());

                                Console.Write("\n Input Speed: ");
                                speed = int.Parse(Console.ReadLine());

                                Console.Write("\n Input Lifting: ");
                                lifting = double.Parse(Console.ReadLine());

                                Console.Write("\n Has trailer?: 1 - yes / 0 - no ");
                                byte answ = byte.Parse(Console.ReadLine());

                                if (answ == 1) trailer = true;
                                else trailer = false;


                            }
                            catch
                            {
                                Console.WriteLine("\n Wrong input! Try again! ");
                                Console.ReadKey();
                                goto Flag3;
                            }

                            list.Add(new Truck(mark, number, speed, lifting, trailer));
                            break;


                        case 4:                                                                    /*            OUTPUT       */
                            Console.WriteLine(" \n TYPE |    MARK    | NUMBER | SPEED | LIFTING |  BAG  ");
                            foreach (Auto lists in list)
                            {
                                lists.Show();
                            }
                            break;

                        case 0:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("\n Nope. Try again!\n");
                            break;
                    }
                    Console.ReadKey();
                }
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("\n Nothing data!\n");
                Console.ReadKey();
                Environment.Exit(0);
            }
        } 
    }
}
