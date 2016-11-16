using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    class Program
    {
        public static Random rand = new Random();
        /**
         * Create array;
         *
         */

        static int[,] createArr(int line, int column)
        {
            int[,] arr = new int[line, column];

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {

                    arr[i, j] = rand.Next(-1,10); ;

                }
            }

            return arr;
        }

        /*
         * Output methode;
         * 
         * **/
        static void Output(int[,] arr) {

            for (int i = 0; i < arr.GetLength(0); i++, Console.WriteLine())
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {

                    Console.Write("{0,5} ", arr[i, j]);

                }
            }

            Console.ReadKey();

        }

        static void Output(int[] arr) {

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(" Index: {0}", arr[i]);
            }

            Console.ReadKey();
        }



        /*
         * Find first minimal num index;
         */
        static int getIndex(int[,] arr) {

            var min = (from int x in arr select x).Min();
            Console.WriteLine(" Min: {0}", min);

            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    if (arr[i, j] == min)
                        return i;
            return -1;

        }

        /*
         * Adding line
         */

        static void addLine(int[,] arr) {

            int index = getIndex(arr) + 1;
            int[,] newArr = new int[arr.GetLength(0) + 1, arr.GetLength(1)];

            int i, j;

            for (i = 0; i < newArr.GetLength(0); i++)
            {

                for (j = 0; j < newArr.GetLength(1); j++)
                {
                    if (i == index)
                    {
                        newArr[i, j] = rand.Next(-9, 9);
                        continue;
                    }
                    if (i > index)
                    {
                        newArr[i, j] = arr[i - 1, j];
                        continue;
                    }
                    newArr[i, j] = arr[i, j];
                }
            }



            Output(newArr);



        }

        /*
         * Adding column
         */

        static void addColumn(int[,] arr) {


            int num = 0, count = 1, ind = 0;
            int line = arr.GetLength(0);
            int column = arr.GetLength(1);
            int[] index = new int[count];

        Flag:

            try //input number;
            {
                Console.Write(" Input number: ");
                num = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("\n You must input number! \n");
                goto Flag;
            }

            // find index;
            for (int i = 0; i < column; i++) {
                for (int j = 0; j < line; j++) {
                    if (arr[j, i] == num) {
                        index[ind] = i;
                        ind++;
                        count++;
                        Array.Resize(ref index, count);
                        break;

                    }
                }
            }

            Array.Resize(ref index, count - 1);

            /*
             * writing in new array
             * 
             * **/

            int alterI = 0;
            int newCol = column + index.Length;

            int[,] newArr = new int[line, newCol];


            for (int i = 0; i < column; i++)
            {

                for (int k = 0; k < index.Length; k++)
                {
                    if (i == index[k])   // if find index 
                    {
                        for (int j = 0; j < line; j++)
                        {
                            newArr[j, alterI] = arr[j, i]; // write this position
                        }

                        alterI++; //up one step 


                        for (int m = 0; m < line; m++)
                        {
                            newArr[m, alterI] = 1; // then write next position
                        }

                        break;
                    }
                    else {
                        for (int j = 0; j < line; j++) {

                            newArr[j, alterI] = arr[j, i];
                        }
                    }
                }
                alterI++;


            }

            Output(newArr);

        }


        /*
         * Fund line index;
         */

        static int[] getLineIndex(int[,] arr) {

            int countTemp = 1, countIndex = 1;
            int line = arr.GetLength(0);
            int column = arr.GetLength(1);
            int[] temp = new int[countTemp];
            int[] index = new int[countIndex];

            for (int i = 0; i < line; i++)
            {
                int calc = 0;
                for (int j = 0; j < column; j++)
                {
                    if (arr[i, j] % 2 == 0)
                    {
                        calc++;

                    }
                }
                temp[i] = calc;
                countTemp++;
                Array.Resize(ref temp, countTemp);
            }

            Array.Resize(ref temp, countTemp - 1);


            int ind = 0;
            int count = 1;


            for (int i = 0; i < line; i++)
            {
                if (temp[i] == 0) {
                    index[ind] = i;
                    ind++;
                    count++;
                    Array.Resize(ref index, count);


                }
            }

            Array.Resize(ref index, count - 1);

            if (index.Length == 0) {
                Console.WriteLine(" All the rows even!");

            }

            Output(index);
            return index;
        }


        static void delLine(int[,] arr) {

            int line = arr.GetLength(0);
            int column = arr.GetLength(1);

            int alterI = 0;

            int[] index = getLineIndex(arr);
            line = line - index.Length;

            int[,] temp = new int[line, column];



            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int k = 0; k < index.Length; k++)
                {
                    if (i == index[k])
                    {
                        try
                        {
                            i++;
                            for (int j = 0; j < column; j++)
                            {
                                temp[alterI, j] = arr[i, j];
                            }
                        }
                        catch {
                            break;
                        }

                    }
                    else {
                        for (int j = 0; j < column; j++)
                        {
                            temp[alterI, j] = arr[i, j];
                        }
                    }

                }

                alterI++;

            }

            Output(temp);

        }

        /*
         * Get Column Index
         * **/


        static int[] getColumnIndex(int[,] arr)
        {

            // calc negative numbers

            int countTemp = 1, countIndex = 1;
            int line = arr.GetLength(0);
            int column = arr.GetLength(1);
            int[] temp = new int[countTemp];
            int[] index = new int[countIndex];

            for (int i = 0; i < column; i++)
            {
                int calc = 0;
                for (int j = 0; j < line; j++)
                {
                    if (arr[j, i] < 0)
                    {
                        calc++;
                    }
                }
                temp[i] = calc;
                countTemp++;
                Array.Resize(ref temp, countTemp);
            }

            Array.Resize(ref temp, countTemp - 1);


            // get index

            int ind = 0;
            int count = 1;


            for (int i = 0; i < countTemp - 1; i++)
            {
                if (temp[i] == 0)
                {
                    index[ind] = i;
                    ind++;
                    count++;
                    Array.Resize(ref index, count);


                }
            }

            Array.Resize(ref index, count - 1);

            if (index.Length == 0)
            {
                Console.WriteLine(" All column negative!");

            }

            Output(index);
            return index;
        }




        /* 
         * Delete column
         * **/


        static void delColumn(int[,] arr) {


            int n = arr.GetLength(0)+1 ;
            int m = arr.GetLength(1)+1;

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
           
            Output(arr);
            

        }

        static void DelZeroStrCol(int[,] arr)
        {
            int n = arr.GetLength(0);
            int m = arr.GetLength(1);
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
            Output(arr);
        }



        static void Main(string[] args)
        {

            int line = 0, column = 0;

            Flag:

            try //input array size;
            {
                Console.Write(" Input line size: ");
                line = int.Parse(Console.ReadLine());

                Console.Write(" Input column size: ");
                column = int.Parse(Console.ReadLine());

            }
            catch
            {
                Console.WriteLine("\n You must input number! \n");
                goto Flag;
            }

            int[,] arr = new int[line, column];

            arr = createArr(line, column); //new arr;

            Output(arr); //output array;

            /* Console.WriteLine("\n 1.	Вставить новую строку после строки, в которой находится первый встреченный минимальный элемент. \n");

             addLine(arr); // adding line;

             Console.WriteLine("\n 2.	Вставить новый столбец перед всеми столбцами, в которых встречается заданное число. \n");

             addColumn(arr); // adding column;

             Console.WriteLine("\n 3.	Удалить все строки, в которых нет ни одного четного элемента. \n");

            delLine(arr);*/


            delColumn(arr);


        }


    }

}
