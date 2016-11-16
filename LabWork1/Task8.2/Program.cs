using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_2
{
    class Program
    {
        static int[,] Input(out int n, out int m)
        {
            Random rand = new Random();
            Console.WriteLine("\n Введите размерность массива (n, m):");
            n = int.Parse(Console.ReadLine());
            m = int.Parse(Console.ReadLine());
            int[,] arr = new int[(2 * n), (2 * m)];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    arr[i, j] = rand.Next(-9,9);
                }

            Console.Clear();
            return arr;
        }
        static void Print(int[,] arr, int n, int m)
        {
            Console.Clear();
            Console.WriteLine();
            for (int i = 0; i < n; i++, Console.WriteLine())
                for (int j = 0; j < m; j++)
                    Console.Write(" {0} ", arr[i, j]);
            Console.WriteLine();
            Console.ReadKey();
        }
        static void AddString(int[,] arr, ref int n, int m)
        {
            Console.Clear();
            int minEl = arr[0, 0];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (arr[i, j] < minEl) minEl = arr[i, j];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (arr[i, j] == minEl)
                    {
                        for (int k = n; k > i; k--)
                            for (int l = 0; l < m; l++)
                                arr[k, l] = arr[k - 1, l];
                        n++;

                        Console.WriteLine(" Введите элементы новой строки: ");
                        for (int k = 0; k < m; k++)
                        {
                            Console.Write("arr[{0}, {1}] = ", i + 1, k);
                            arr[i + 1, k] = int.Parse(Console.ReadLine());
                        }
                        Print(arr, n, m);
                        return;
                    }
        }
        static void AddColumn(int[,] arr, int n, ref int m)
        {
            Console.Clear();
            Console.Write(" Введите заданное число: ");
            int num = int.Parse(Console.ReadLine());
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if (arr[j, i] == num)
                    {
                        for (int k = m; k > i; k--)
                            for (int l = 0; l < n; l++)
                                arr[l, k] = arr[l, k - 1];
                        m++;

                        Console.WriteLine(" Введите эл-ты нового столбца: ");
                        for (int k = 0; k < n; k++)
                        {
                            Console.Write("arr[{0}, {1}] = ", k, i);
                            arr[k, i] = int.Parse(Console.ReadLine());
                        }
                        i++; break;
                    }
            Print(arr, n, m);
        }
        static void DelNoEvenStr(int[,] arr, ref int n, int m)
        {
            Console.Clear();
            int QEvenEl = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    if (arr[i, j] % 2 == 0) QEvenEl++;
                if (QEvenEl == 0)
                {
                    for (int k = i; k < n; k++)
                        for (int l = 0; l < m; l++)
                            arr[k, l] = arr[k + 1, l];
                    n--; i--;
                }
                QEvenEl = 0;
            }
            Print(arr, n, m);
        }
        static void DelPositiveCol(int[,] arr, int n, ref int m)
        {
            Console.Clear();
            int QPosEl = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                    if (arr[j, i] > 0) QPosEl++;
                if (QPosEl == n)
                {
                    for (int k = i; k < m; k++)
                        for (int l = 0; l < n; l++)
                            arr[l, k] = arr[l, k + 1];
                    m--; i--;
                }
                QPosEl = 0;
            }
            Print(arr, n, m);
        }
        static void DelStrCol(int[,] arr, ref int n, ref int m)
        {
            Console.Clear();
            int QMatch = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    if (arr[i, j] == arr[j, i]) QMatch++;
                if (QMatch == n)
                {
                    for (int k = i; k < n; k++)                                         //удаляем строку
                        for (int l = 0; l < m; l++)
                            arr[k, l] = arr[k + 1, l];
                    n--;

                    for (int k = i; k < m; k++)                                         //удаляем столбец
                        for (int l = 0; l < n; l++)
                            arr[l, k] = arr[l, k + 1];
                    m--; i--;
                }
                QMatch = 0;
            }
            Print(arr, n, m);
        }
        static void DelZeroStrCol(int[,] arr, ref int n, ref int m)
        {
            Console.Clear();
            int QZeroMatchStr = 0, QZeroMatchCol = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (arr[i, j] == 0) QZeroMatchStr++;
                    if (arr[j, i] == 0) QZeroMatchCol++;
                }
                if (QZeroMatchStr == n)
                {
                    for (int k = i; k < n; k++)
                        for (int l = 0; l < m; l++)
                            arr[k, l] = arr[k + 1, l];
                    n--;
                }
                if (QZeroMatchCol == m)
                {
                    for (int k = i; k < m; k++)
                        for (int l = 0; l < n; l++)
                            arr[l, k] = arr[l, k + 1];
                    m--;
                }

                if (n == (QZeroMatchStr - 1) || m == (QZeroMatchCol - 1)) i--;              //если удаление было, то i--
                QZeroMatchCol = 0; QZeroMatchStr = 0;
            }
            Print(arr, n, m);
        }
        static void Main(string[] args)
        {
            try
            {
                int n, m;
                int[,] myArray = Input(out n, out m);

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\n 1 - Вывести массив на экран");
                    Console.WriteLine(" 2 - Вставить строку после строки, в которой наход. 1-ый встреченный min эл-т");
                    Console.WriteLine(" 3 - Вставить столбец перед всеми столбцами, в которых встреч. зад-е число.");
                    Console.WriteLine(" 4 - Удалить все строки, в которых нет ни одного четного элемента.");
                    Console.WriteLine(" 5 - Удалить все столбцы, в которых все элементы положительны.");
                    Console.WriteLine(" 6 - Удалить k-тую строку и j-тый столбец, если их значения совпадают.");
                    Console.WriteLine(" 7 - Удалить из массива все нулевые строки и столбцы.");
                    Console.WriteLine(" 0 - Выход");
                    Console.Write(" ---> Введите пункт меню: ");
                    int menu = int.Parse(Console.ReadLine());
                    switch (menu)
                    {
                        case 1: Print(myArray, n, m); break;
                        case 2: AddString(myArray, ref n, m); break;
                        case 3: AddColumn(myArray, n, ref m); break;
                        case 4: DelNoEvenStr(myArray, ref n, m); break;
                        case 5: DelPositiveCol(myArray, n, ref m); break;
                        case 6: DelStrCol(myArray, ref n, ref m); break;
                        case 7: DelZeroStrCol(myArray, ref n, ref m); break;
                        case 0: Environment.Exit(0); break;
                        default: Console.WriteLine("Неверный пункт. Повторите ввод."); break;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine("Размер массива не может быть отрицательным.");
            }
            catch (NullReferenceException)
            {
                Console.Clear();
                Console.WriteLine("Размер массива не может быть нулевым.");
            }
            catch (OverflowException)
            {
                Console.Clear();
                Console.WriteLine("Размер массива слишком большой");
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
                Console.WriteLine("Произошла ошибка:\n{0}", err);
            }
        }
    }
}

