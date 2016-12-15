using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork2
{
    class Money
    {

        private int nominal, count, total;

        public Money(int nominalValue, int countValue)
        {
            this.nominal = nominalValue;
            this.count = countValue;
            this.total = nominal * count;
        }

        public void View()
        {
            this.total = nominal * count;
            Console.WriteLine("\n Nominal $ | Count | Total");
            Console.WriteLine("\n       {0}   |   {1}  |  {2} ", nominal, count, total);
            Console.ReadKey();
        }


        public bool CanBuy(int cost) {
            bool answer = false;

            if (total > cost)
            {
                answer = true;
                Console.WriteLine("\n You can buy it!  ");
            }
            else
            {
                Console.WriteLine("\n Sorry dude, you can't buy it! ='(  ");      
            }
            Console.ReadKey();
            return answer;
        }

        public void HowMuch(int cost) {
            int answer = 0;

            if (cost > 0) {
                answer = total / cost;
                Console.WriteLine(" You can by: {0}", answer);
            }
            else{
                Console.WriteLine(" Wrong product cost!");
            }
            Console.ReadKey();
        }

        public int Denomination {
            get {
                return this.nominal;
            }
            set {
                this.nominal = value;
            }
        }

        public int Count {
            get {
                return this.count;
            }
            set {
                this.count = value;
            }
        }

        public int Total {
            get {
                return this.total;
            }
        }


        public int this[int index] {
            get {
                if (index == 0) return nominal;
                if (index == 1) return count;
                else {
                    Console.WriteLine("/n Entered incorrectly index! ");
                    return 0;
                }
            }

            set {
                if (index == 0) nominal = value;
                if (index == 1) count = value;
            }
        }

        public static Money operator ++(Money obj) {
            obj[0]++;
            obj[1]++;
            return obj;
        }

        public static Money operator --(Money obj)
        {
            if (obj[0] <= 0 || obj[1] <= 0) {
                Console.WriteLine("/n It's impossible to reduce! ");
                return obj;
            }
            else {
                obj[0]--;
                obj[1]--;
            }
            return obj;
        }

        public static bool operator !(Money obj) {
            if (obj[1] != 0) {
                return true; 
            }return false;
        }

        public static Money operator +(Money obj, int n) {
            obj[1] = obj[1] + n;
            return obj;
        }

        public static implicit operator string(Money ob)
        {
            return ob.nominal.ToString() + " " + ob.count.ToString();
        }


        public static implicit operator Money(string obj) {

            string[] m = obj.Split(' ');
            Money newM = new Money(int.Parse(m[0]), int.Parse(m[1]));

            return newM;
        }
    }
    





    class Program
    {
        static void Main(string[] args)
        {
            try {

                int val = 0, count = 0, menu, cost = 0;
                Money m = new Money(val, count);

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\n 1 - New money object");
                    Console.WriteLine(" 2 - Show money");
                    Console.WriteLine(" 3 - Buy goods");
                    Console.WriteLine(" 4 - How much can I buy?");
                    Console.WriteLine(" 5 - ++ ");
                    Console.WriteLine(" 6 - -- ");
                    Console.WriteLine(" 7 - ! ");
                    Console.WriteLine(" 8 - + ");
                    Console.WriteLine(" 9 - Money to String ");
                    Console.WriteLine(" 0 - Exit ");
                    Console.Write(" ---> Your choise: ");
                    menu = int.Parse(Console.ReadLine());
                    switch (menu)
                    {
                        case 1:
                        Flag:
                            try
                            {
                                Console.Clear();

                                Console.Write("\n Insert denomination $: ");
                                val = int.Parse(Console.ReadLine());

                                Console.Write(" Insert count: ");
                                count = int.Parse(Console.ReadLine());
                            }
                            catch
                            {
                                Console.WriteLine("\n Nope. Try again!\n");
                                goto Flag;

                            }
                            m = new Money(val, count);
                            break;

                        case 2:
                            m.View();
                            break;

                        case 3:
                        FlagE:
                            try
                            {
                                Console.Clear();
                                Console.Write("\n Insert goode cost $: ");
                                cost = int.Parse(Console.ReadLine());
                            }
                            catch
                            {
                                Console.WriteLine("\n Nope. Try again!\n");
                                goto FlagE;

                            }
                            m.CanBuy(cost);
                            break;

                        case 4:
                        FlagM:
                            try
                            {
                                Console.Clear();
                                Console.Write("\n Insert goode cost $: ");
                                cost = int.Parse(Console.ReadLine());
                            }
                            catch
                            {
                                Console.WriteLine("\n Nope. Try again!\n");
                                goto FlagM;

                            }
                            m.HowMuch(cost);
                            break;

                        case 5:
                            m = ++m;
                            break;

                        case 6:
                            m = --m;
                            break;

                        case 7:
                            Console.WriteLine("\n {0} ", !m);
                            Console.ReadKey();
                            break;

                        case 8:
                            m = m + 3;
                            break;

                        case 9:

                            string mon = m;
                           
                            Console.WriteLine("\n That's string: {0} ", mon);
                            Console.ReadKey();
                            Money str = mon;
                            Console.WriteLine("\n That's object: {0} {1} ", str[0], str[1]);
                            Console.ReadKey();
                            break;

                        case 0:
                            Environment.Exit(0);
                            break;

                        default: Console.WriteLine("\n Nope. Try again!\n");
                            break;
                    }
                }
            }
            catch(Exception e)
            {
                Console.Clear();
                Console.WriteLine("\n Error: {0} ", e.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
  
    }
}
