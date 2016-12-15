using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    class Program
    {

        static readonly int MaxDeep = 10000;
        static int[] Decomposition = new int[MaxDeep];
        static int[] sums = new int[100];


        static void Main(string[] args)
        {
            try
            {
                Console.Write("Введите число для разложения на слагаемые: ");
                int number = int.Parse(Console.ReadLine());
                DecompSum(number, 1, 0);
                Console.WriteLine();
                Console.ReadKey();

                Console.Write("Введите число для разложения на множители: ");
                int numberDec = int.Parse(Console.ReadLine());
                Decompose(numberDec, 2, 0);
                Console.WriteLine();
            }
            catch (IndexOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine("Число не может быть отрицательным.");
            }
            catch (NullReferenceException)
            {
                Console.Clear();
                Console.WriteLine("Число не может быть == 0.");
            }
            catch (OverflowException)
            {
                Console.Clear();
                Console.WriteLine("Число слишком большое");
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Ошибка ввода! Нужно ввести число.");
                Environment.Exit(0);
            }
            catch (Exception err)
            {
                Console.Clear();
                Console.WriteLine("Произошла ошибка:\n{0}", err.Message);
            }

            
            

            Console.ReadKey();
            
        }


        static void DecompSum(int number, int sum, int level)
        {
            while (number >= sum)
            {
                sums[level] = sum;

                if (number - sum > 0)
                    DecompSum((number - sum), sum, (level + 1));

                if (number - sum == 0)
                {
                    if (level == 0) break;

                    for (int i = 0; i <= level; i++)
                        Console.Write(sums[i] + " ");
                    Console.WriteLine();
                }
                sum++;
            }

        }


            static void Decompose(int number, int mul, int level)
        {
            while (number >= mul)
            {
                if (number % mul == 0)
                {
                    sums[level] = mul;

                    if (number / mul == 1)
                    {
                        if (level == 0) break;                                      

                        for (int i = 0; i <= level; i++)                            
                            Console.Write(sums[i] + " ");
                        Console.WriteLine();

                        mul++; continue;
                    }
                    Decompose((number / mul), mul, (level + 1));
                }
                mul++;
            }
        }

    }
}
