
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;//берем StreamReader and e.t.c.
using System.Text.RegularExpressions;
using System.Collections;

namespace Lab6
{
    class Stackk
    {
        //1.	Дан файл, в котором записан набор чисел. Переписать в другой файл все числа в обратном порядке. 
        //2.	Создать текстовый файл. Распечатать гласные буквы этого файла в обратном порядке. 
        //3.	Напечатать содержимое текстового файла t, выписывая литеры каждой его строки в обратном порядке. 
        //4.	Даны 2 строки s1 и s2. Из каждой можно читать по одному символу. Выяснить, является ли строка s2 обратной s1. 

        public void PervoeZadanie()
        {
            Console.Write("1.\nВседите путь тектового фала");
            StringBuilder temp = new StringBuilder();
            temp.Append(Console.ReadLine());
            //StringBuilder temp = new StringBuilder(@"D:\work\1.txt");
            Stack<char> myStack = new Stack<char>();
            using (StreamReader inp = new StreamReader(temp.ToString(), Encoding.Default))
                while (inp.Peek() >= 0)
                    myStack.Push((char)inp.Read());
            temp.Insert(temp.Length - 4, "_1");
            myStack.Reverse();
            using (StreamWriter outp = new StreamWriter(temp.ToString(), true, Encoding.Default))
                while (myStack.Count > 0)
                    outp.Write(myStack.Pop());
            return;
        }
        public void VtoroeZadanie()
        {
            Console.Write("2.\n");
            Stack<char> myStack = new Stack<char>();
            Stack<char> myStackRev = new Stack<char>();
            Random randD = new Random();
            int z = randD.Next(21, 150);
            for (int i = 0; i < z; i++)
                myStack.Push((char)randD.Next(48, 90));
            using (StreamWriter outp = new StreamWriter(@"D:\work\Zadanie2.txt", false, Encoding.Default))
                foreach (char pp in myStack)
                    outp.Write(pp);
            char[] glasnie = new char[] { 'a', 'e', 'i', 'o', 'y', 'u', 'A', 'E', 'I', 'O', 'Y', 'U' };
            Console.Write("исходная строка\n");
            foreach (char d in myStack)
                Console.Write(d);
            foreach (char pp in myStack)
                myStackRev.Push(pp);
            Console.Write("\nгласные в строке строке\n");
            foreach (char p in myStackRev)
                foreach (char pp in glasnie)
                    if (p == pp)
                        Console.Write(p);
            return;
        }
        public void TretieZadanie()
        {
            Console.Write("3.");
            Stack<Object> myStack = new Stack<Object>();
            using (StreamReader inp = new StreamReader(@"D:\work\Zadanie3.txt", Encoding.Default))
            {
                Console.Write("\n");
                while (inp.Peek() >= 0)
                {
                    myStack.Push((char)inp.Read());
                    if ((inp.Peek() == '\r') | (inp.EndOfStream))
                    {

                        myStack.Reverse();
                        while (myStack.Count > 0)
                        {
                            Console.Write("" + myStack.Pop());/**/
                        }
                    }
                }
            }
            return;
        }
        public void ChetvertoeZadanie()
        {
            string s1;
            string s2;
            Console.WriteLine("Ввести строки вручную?(Y/другая буква)");
            ConsoleKeyInfo h = Console.ReadKey();
            if (h.Key != ConsoleKey.Y)
            {
                s1 = "123";
                s2 = "321";
            }
            else
            {
                Console.WriteLine("Введите первую строку\n");
                s1 = Console.ReadLine();
                Console.WriteLine("Введите вторую строку\n");
                s2 = Console.ReadLine();
            }
            Console.WriteLine("");
            Stack<string> s1St = new Stack<string>();
            Stack<string> s2St = new Stack<string>();
            for (int i = 1; i <= s1.Length; i++)
            {
                s1St.Push(s1.Substring(i - 1, 1));
                s2St.Push(s2.Substring(s2.Length - i, 1));
            }
            if (s1.Length != s2.Length) throw new Exception("Строки на равны, сравнение невозможно");
            for (int i = 0; i < s1.Length; i++)
            {
                Console.WriteLine("{0}. s1St.Peek()={1} s2St.Peek()={2}", i, s1St.Peek(), s2St.Peek());
                if (s1St.Pop() != s2St.Pop())
                {
                    Console.WriteLine("Cтроки не обратные");
                    return;
                }/**/
            }
            Console.WriteLine("Строки являются обратными ");
            return;
        }

    }
    class Queuee
    {
        /* 1.	Дан файл, содержащий информацию о сотрудниках фирмы: фамилия, имя, отчество, пол, возраст, размер зарплаты. 
          За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о сотрудниках, зарплата 
          которых меньше 10000, потом данные об остальных сотрудниках, сохраняя исходный порядок в каждой группе сотрудников. 
         2.	Дан файл, содержащий информацию о сотрудниках фирмы: фамилия, имя, отчество, пол, возраст, размер зарплаты. 
          За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о сотрудниках младше 30 лет,
          потом данные об остальных сотрудниках, сохраняя исходный порядок в каждой группе сотрудников. 
         3.	Дан файл, содержащий информацию о студентах: фамилия, имя, отчество, номер группы, оценки по трем предметам текущей 
         * сессии. За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о студентах, успешно 
         * сдавших сессию, потом данные об остальных студентах, сохраняя исходный порядок в каждой группе сотрудников. 
        4.	Дан файл, содержащий информацию о студентах: фамилия, имя, отчество, номер группы, оценки по трем предметам текущей 
         * сессии. За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о студентах, успешно 
         * обучающихся на 4 и 5, потом данные об остальных студентах, сохраняя исходный порядок в каждой группе сотрудников. */

        public Queue<string> ReadChelov(StringBuilder path)
        {
            Queue<string> spisChelov = new Queue<string>();
            using (StreamReader spisFile = new StreamReader(path.ToString(), Encoding.Default))
                for (int i = 0; ; i++)
                {
                    string line = spisFile.ReadLine();
                    if (line == null) break;
                    spisChelov.Enqueue(line);
                }
            return spisChelov;
        }
        public void Nayti(int i, int dopusk)
        {
            StringBuilder path = new StringBuilder(100);
            Console.WriteLine("1.\nВведите путь к текстовому файлу");
            path.Append(Console.ReadLine());
            //path.Append(@"D:\work\l6.txt");
            Queue<string> rab = ReadChelov(path);
            Queue<string> doZp = new Queue<string>();
            Queue<string> posleZp = new Queue<string>();
            while (rab.Count > 0)
            {
                string[] tempSplit = rab.Dequeue().Split(' ');


                if (tempSplit.Length <= 6)
                {
                    if (Convert.ToDouble(tempSplit[i]) < dopusk)
                        doZp.Enqueue(String.Join(" ", tempSplit));
                    else
                        posleZp.Enqueue(String.Join(" ", tempSplit));
                }
                else
                {
                    if ((Convert.ToDouble(tempSplit[4]) > dopusk) & (Convert.ToDouble(tempSplit[5]) > dopusk) & (Convert.ToDouble(tempSplit[6]) > dopusk))
                        doZp.Enqueue(String.Join(" ", tempSplit));
                    else
                        posleZp.Enqueue(String.Join(" ", tempSplit));
                }
            }
            Console.WriteLine("Люди до " + dopusk);
            while (doZp.Count > 0)
                Console.WriteLine(" " + doZp.Dequeue());
            Console.WriteLine("Люди после " + dopusk);
            while (posleZp.Count > 0)
                Console.WriteLine(" " + posleZp.Dequeue());
        }
        public void PervoeZadanie()
        {
            Console.Write("1.\n");
            int i = 5;
            int dopusk = 10000;
            Nayti(i, dopusk);
        }
        public void VtoroeZadanie()
        {
            Console.Write("2.\n");
            int i = 4;
            int dopusk = 30;
            Nayti(i, dopusk);
        }
        public void TretieZadanie()
        {
            Console.Write("3.\n");
            int i = -1;
            int dopusk = 2;
            Nayti(i, dopusk);
        }
        public void ChetvertoeZadanie()
        {
            Console.Write("4.\n");
            int i = -1;
            int dopusk = 3;
            Nayti(i, dopusk);
        }
    }
    class ArrayListt
    {
        /* //1.	Дан файл, в котором записан набор чисел. Переписать в другой файл все числа в обратном порядке. 
         //2.	Создать текстовый файл. Распечатать гласные буквы этого файла в обратном порядке. 
         //3.	Напечатать содержимое текстового файла t, выписывая литеры каждой его строки в обратном порядке. 
         //4.	Даны 2 строки s1 и s2. Из каждой можно читать по одному символу. Выяснить, является ли строка s2 обратной s1. 
         public void PervoeZadanie()
         {
             Console.Write("1.\nВседите путь тектового фала");
             StringBuilder temp = new StringBuilder();
             temp.Append(Console.ReadLine());
             //temp.Append(@"D:\work\1.txt"); 
             ArrayList myArrList = new ArrayList();
             using (StreamReader inp = new StreamReader(temp.ToString(), Encoding.Default))
                 while (inp.Peek() >= 0)
                     myArrList.Add((char)inp.Read());
             temp.Insert(temp.Length - 4, "_1");
             myArrList.Reverse();
             using (StreamWriter outp = new StreamWriter(temp.ToString(), true, Encoding.Default))
                 foreach (char pp in myArrList)
                     outp.Write(pp);
             return;
         }
         public void VtoroeZadanie()
           {
             Console.Write("2.\n");
             ArrayList myArrList = new ArrayList();
             Random randD = new Random();
             int z = randD.Next(30, 150);
             for (int i = 0; i < z; i++)
                 myArrList.Add((char)randD.Next(48, 90));
             using (StreamWriter outp = new StreamWriter(@"D:\work\Zadanie2.txt", false, Encoding.Default))
                 foreach(char pp in myArrList)
                     outp.Write(pp);
             char[] glasnie = new char[] { 'a', 'e', 'i', 'o', 'y', 'u', 'A', 'E', 'I', 'O', 'Y', 'U' };
             Console.Write("исходная строка\n");
             foreach (char d in myArrList)
                 Console.Write(d);
             myArrList.Reverse();
             Console.Write("\nгласные в строке строке\n");
             foreach (char p in myArrList)
                 foreach (char pp in glasnie)
                     if (p == pp)
                         Console.Write(p);
             return;
         }
         public void TretieZadanie()
         {
             Console.Write("3.");
             //Stack<Object> myStack = new Stack<Object>();
             ArrayList myArrList = new ArrayList();
             using (StreamReader inp = new StreamReader(@"D:\work\Zadanie3.txt", Encoding.Default))
             {
                 Console.Write("\n");
                 while (inp.Peek() >= 0)
                 {
                     myArrList.Add((char)inp.Read());
                     if ((inp.Peek() == '\r') | (inp.EndOfStream))
                     {
                         myArrList.Reverse(); foreach (char pp in myArrList)
                         {
                             Console.Write("" + pp);
                         }
                         myArrList.Clear();
                     }
                 }
             }
             return;
         }
         public void ChetvertoeZadanie()
         {
             string s1;
             string s2;
             int i = 0;
             Console.WriteLine("Ввести строки вручную?(Y/другая буква)");
             ConsoleKeyInfo h = Console.ReadKey();
             if (h.Key != ConsoleKey.Y)
             {
                 s1 = "abc";
                 s2 = "cba";
             }
             else
             {
                 Console.WriteLine("Введите первую строку\n");
                 s1 = Console.ReadLine();
                 Console.WriteLine("Введите вторую строку\n");
                 s2 = Console.ReadLine();
             }
             Console.WriteLine("");
             ArrayList strAr1 = new ArrayList();
             ArrayList strAr2 = new ArrayList();
             if (s1.Length != s2.Length) throw new Exception("Строки на равны, сравнение невозможно");
             for (i = 1; i <= s1.Length; i++)
             {
                 strAr1.Add(s1.Substring(i - 1, 1));
                 strAr2.Add(s2.Substring(s2.Length - i, 1));
             }
             for (i = 0; i < s1.Length; i++)
             {
                 Console.WriteLine("{0}. A={1} B={2}", i, strAr1[i], strAr2[i]);
                 if (strAr1[i].ToString() != strAr2[i].ToString())
                 {
                     Console.WriteLine("Cтроки не обратные");
                     return;
                 }
             } Console.WriteLine("Строки являются обратными ");
             return;
         }*/
        /* 1.	Дан файл, содержащий информацию о сотрудниках фирмы: фамилия, имя, отчество, пол, возраст, размер зарплаты. 
         За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о сотрудниках, зарплата 
         которых меньше 10000, потом данные об остальных сотрудниках, сохраняя исходный порядок в каждой группе сотрудников. 
        2.	Дан файл, содержащий информацию о сотрудниках фирмы: фамилия, имя, отчество, пол, возраст, размер зарплаты. 
         За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о сотрудниках младше 30 лет,
         потом данные об остальных сотрудниках, сохраняя исходный порядок в каждой группе сотрудников. 
        3.	Дан файл, содержащий информацию о студентах: фамилия, имя, отчество, номер группы, оценки по трем предметам текущей 
        * сессии. За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о студентах, успешно 
        * сдавших сессию, потом данные об остальных студентах, сохраняя исходный порядок в каждой группе сотрудников. 
       4.	Дан файл, содержащий информацию о студентах: фамилия, имя, отчество, номер группы, оценки по трем предметам текущей 
        * сессии. За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о студентах, успешно 
        * обучающихся на 4 и 5, потом данные об остальных студентах, сохраняя исходный порядок в каждой группе сотрудников. */

        public ArrayList ReadChelov(StringBuilder path)
        {
            ArrayList spisChelov = new ArrayList();
            using (StreamReader spisFile = new StreamReader(path.ToString(), Encoding.Default))
                for (int i = 0; ; i++)
                {
                    string line = spisFile.ReadLine();
                    if (line == null) break;
                    spisChelov.Add(line);
                }
            return spisChelov;
        }
        public void Nayti(int i, int dopusk)
        {
            StringBuilder path = new StringBuilder(100);
            Console.WriteLine("1.\nВведите путь к текстовому файлу");
            //path.Append(Console.ReadLine());
            path.Append(@"D:\work\l6.txt");
            ArrayList rab = ReadChelov(path);
            ArrayList doZp = new ArrayList();
            ArrayList posleZp = new ArrayList();
            foreach (string pp in rab)
            {
                string[] tempSplit = pp.Split(' ');


                if (tempSplit.Length <= 6)
                {
                    if (Convert.ToDouble(tempSplit[i]) < dopusk)
                        doZp.Add(String.Join(" ", tempSplit));
                    else
                        posleZp.Add(String.Join(" ", tempSplit));
                }
                else
                {
                    if ((Convert.ToDouble(tempSplit[4]) > dopusk) & (Convert.ToDouble(tempSplit[5]) > dopusk) & (Convert.ToDouble(tempSplit[6]) > dopusk))
                        doZp.Add(String.Join(" ", tempSplit));
                    else
                        posleZp.Add(String.Join(" ", tempSplit));
                }
            }
            Console.WriteLine("Люди до " + dopusk);
            foreach (string pp in doZp)
                Console.WriteLine(" " + pp);
            Console.WriteLine("Люди после " + dopusk);
            foreach (string pp in posleZp)
                Console.WriteLine(" " + pp);
        }
        public void PervoeZadanie()
        {
            Console.Write("1.\n");
            int i = 5;
            int dopusk = 10000;
            Nayti(i, dopusk);
        }
        public void VtoroeZadanie()
        {
            Console.Write("2.\n");
            int i = 4;
            int dopusk = 30;
            Nayti(i, dopusk);
        }
        public void TretieZadanie()
        {
            Console.Write("3.\n");
            int i = -1;
            int dopusk = 2;
            Nayti(i, dopusk);
        }
        public void ChetvertoeZadanie()
        {
            Console.Write("4.\n");
            int i = -1;
            int dopusk = 3;
            Nayti(i, dopusk);
        }

    }
    class HashTablee
    //        : реализовать простейший каталог музыкальных компакт-дисков, который позволяет: 
    //1.	Добавлять и удалять диски. 
    //2.	Добавлять и удалять песни. 
    //3.	Просматривать содержимое целого каталога и каждого диска в отдельности. 
    //4.	Осуществлять поиск всех записей заданного исполнителя по всему каталогу.
    {
        public Hashtable CreateList()
        {
            Hashtable tempH = new Hashtable(100);
            tempH.Add("Odin", new Hashtable());
            {
                ((Hashtable)tempH["Odin"]).Add("Odin***Psja odin", "Psja odin");
                ((Hashtable)tempH["Odin"]).Add("Odin***Psja dva", "Psja dva");
                ((Hashtable)tempH["Odin"]).Add("Odin***Psja tri", "Psja tri");
                ((Hashtable)tempH["Odin"]).Add("Odin***Psja chetire", "Psja chetire");
                ((Hashtable)tempH["Odin"]).Add("Odin***Psja pjat'", "Psja pjat'");
                ((Hashtable)tempH["Odin"]).Add("Odin***Psja shest'", "Psja shest'");
                ((Hashtable)tempH["Odin"]).Add("Odin***Psja sem", "Psja sem");
                ((Hashtable)tempH["Odin"]).Add("Odin***Psja vosem", "Psja vosem");
                ((Hashtable)tempH["Odin"]).Add("Odin***Psja devjat'", "Psja devjat'");
                ((Hashtable)tempH["Odin"]).Add("Odin***Psja desjat'", "Psja desjat");
            }
            tempH.Add("Dva", new Hashtable());
            {
                ((Hashtable)tempH["Dva"]).Add("Dva***Psja odin", "Psja odin");
                ((Hashtable)tempH["Dva"]).Add("Dva***Psja dva", "Psja dva");
                ((Hashtable)tempH["Dva"]).Add("Dva***Psja tri", "Psja tri");
                ((Hashtable)tempH["Dva"]).Add("Dva***Psja chetire", "Psja chetire");
                ((Hashtable)tempH["Dva"]).Add("Dva***Psja pjat'", "Psja pjat'");
                ((Hashtable)tempH["Dva"]).Add("Dva***Psja shest'", "Psja shest'");
                ((Hashtable)tempH["Dva"]).Add("Dva***Psja sem", "Psja sem");
                ((Hashtable)tempH["Dva"]).Add("Dva***Psja vosem", "Psja vosem");
                ((Hashtable)tempH["Dva"]).Add("Dva***Psja devjat'", "Psja devjat'");
                ((Hashtable)tempH["Dva"]).Add("Dva***Psja desjat'", "Psja desjat");
            }
            tempH.Add("Tri", new Hashtable());
            {
                ((Hashtable)tempH["Tri"]).Add("Tri***Psja odin", "Psja odin");
                ((Hashtable)tempH["Tri"]).Add("Tri***Psja dva", "Psja dva");
                ((Hashtable)tempH["Tri"]).Add("Tri***Psja tri", "Psja tri");
                ((Hashtable)tempH["Tri"]).Add("Tri***Psja chetire", "Psja chetire");
                ((Hashtable)tempH["Tri"]).Add("Tri***Psja pjat'", "Psja pjat'");
                ((Hashtable)tempH["Tri"]).Add("Tri***Psja shest'", "Psja shest'");
                ((Hashtable)tempH["Tri"]).Add("Tri***Psja sem", "Psja sem");
                ((Hashtable)tempH["Tri"]).Add("Tri***Psja vosem", "Psja vosem");
                ((Hashtable)tempH["Tri"]).Add("Tri***Psja devjat'", "Psja devjat'");
                ((Hashtable)tempH["Tri"]).Add("Tri***Psja desjat'", "Psja desjat");
            }
            tempH.Add("Chetire", new Hashtable());
            {
                ((Hashtable)tempH["Chetire"]).Add("Chetire***Psja odin", "Psja odin");
                ((Hashtable)tempH["Chetire"]).Add("Chetire***Psja dva", "Psja dva");
                ((Hashtable)tempH["Chetire"]).Add("Chetire***Psja tri", "Psja tri");
                ((Hashtable)tempH["Chetire"]).Add("Chetire***Psja chetire", "Psja chetire");
                ((Hashtable)tempH["Chetire"]).Add("Chetire***Psja pjat'", "Psja pjat'");
                ((Hashtable)tempH["Chetire"]).Add("Chetire***Psja shest'", "Psja shest'");
                ((Hashtable)tempH["Chetire"]).Add("Chetire***Psja sem", "Psja sem");
                ((Hashtable)tempH["Chetire"]).Add("Chetire***Psja vosem", "Psja vosem");
                ((Hashtable)tempH["Chetire"]).Add("Chetire***Psja devjat'", "Psja devjat'");
                ((Hashtable)tempH["Chetire"]).Add("Chetire***Psja desjat'", "Psja desjat");
            }
            tempH.Add("Pjat'", new Hashtable());
            {
                ((Hashtable)tempH["Pjat'"]).Add("Pjat'***Psja odin", "Psja odin");
                ((Hashtable)tempH["Pjat'"]).Add("Pjat'***Psja dva", "Psja dva");
                ((Hashtable)tempH["Pjat'"]).Add("Pjat'***Psja tri", "Psja tri");
                ((Hashtable)tempH["Pjat'"]).Add("Pjat'***Psja chetire", "Psja chetire");
                ((Hashtable)tempH["Pjat'"]).Add("Pjat'***Psja pjat'", "Psja pjat'");
                ((Hashtable)tempH["Pjat'"]).Add("Pjat'***Psja shest'", "Psja shest'");
                ((Hashtable)tempH["Pjat'"]).Add("Pjat'***Psja sem", "Psja sem");
                ((Hashtable)tempH["Pjat'"]).Add("Pjat'***Psja vosem", "Psja vosem");
                ((Hashtable)tempH["Pjat'"]).Add("Pjat'***Psja devjat'", "Psja devjat'");
                ((Hashtable)tempH["Pjat'"]).Add("Pjat'***Psja desjat'", "Psja desjat");
            }
            tempH.Add("Shest'", new Hashtable());
            {
                ((Hashtable)tempH["Shest'"]).Add("Shest'***Psja odin", "Psja odin");
                ((Hashtable)tempH["Shest'"]).Add("Shest'***Psja dva", "Psja dva");
                ((Hashtable)tempH["Shest'"]).Add("Shest'***Psja tri", "Psja tri");
                ((Hashtable)tempH["Shest'"]).Add("Shest'***Psja chetire", "Psja chetire");
                ((Hashtable)tempH["Shest'"]).Add("Shest'***Psja pjat'", "Psja pjat'");
                ((Hashtable)tempH["Shest'"]).Add("Shest'***Psja shest'", "Psja shest'");
                ((Hashtable)tempH["Shest'"]).Add("Shest'***Psja sem", "Psja sem");
                ((Hashtable)tempH["Shest'"]).Add("Shest'***Psja vosem", "Psja vosem");
                ((Hashtable)tempH["Shest'"]).Add("Shest'***Psja devjat'", "Psja devjat'");
                ((Hashtable)tempH["Shest'"]).Add("Shest'***Psja desjat'", "Psja desjat");
            }
            tempH.Add("Sem", new Hashtable());
            {
                ((Hashtable)tempH["Sem"]).Add("Sem***Psja odin", "Psja odin");
                ((Hashtable)tempH["Sem"]).Add("Sem***Psja dva", "Psja dva");
                ((Hashtable)tempH["Sem"]).Add("Sem***Psja tri", "Psja tri");
                ((Hashtable)tempH["Sem"]).Add("Sem***Psja chetire", "Psja chetire");
                ((Hashtable)tempH["Sem"]).Add("Sem***Psja pjat'", "Psja pjat'");
                ((Hashtable)tempH["Sem"]).Add("Sem***Psja shest'", "Psja shest'");
                ((Hashtable)tempH["Sem"]).Add("Sem***Psja sem", "Psja sem");
                ((Hashtable)tempH["Sem"]).Add("Sem***Psja vosem", "Psja vosem");
                ((Hashtable)tempH["Sem"]).Add("Sem***Psja devjat'", "Psja devjat'");
                ((Hashtable)tempH["Sem"]).Add("Sem***Psja desjat'", "Psja desjat");
            }
            tempH.Add("Vosem", new Hashtable());
            {
                ((Hashtable)tempH["Vosem"]).Add("Vosem***Psja odin", "Psja odin");
                ((Hashtable)tempH["Vosem"]).Add("Vosem***Psja dva", "Psja dva");
                ((Hashtable)tempH["Vosem"]).Add("Vosem***Psja tri", "Psja tri");
                ((Hashtable)tempH["Vosem"]).Add("Vosem***Psja chetire", "Psja chetire");
                ((Hashtable)tempH["Vosem"]).Add("Vosem***Psja pjat'", "Psja pjat'");
                ((Hashtable)tempH["Vosem"]).Add("Vosem***Psja shest'", "Psja shest'");
                ((Hashtable)tempH["Vosem"]).Add("Vosem***Psja sem", "Psja sem");
                ((Hashtable)tempH["Vosem"]).Add("Vosem***Psja vosem", "Psja vosem");
                ((Hashtable)tempH["Vosem"]).Add("Vosem***Psja devjat'", "Psja devjat'");
                ((Hashtable)tempH["Vosem"]).Add("Vosem***Psja desjat'", "Psja desjat");
            }
            tempH.Add("Devjat'", new Hashtable());
            {
                ((Hashtable)tempH["Devjat'"]).Add("Devjat'***Psja odin", "Psja odin");
                ((Hashtable)tempH["Devjat'"]).Add("Devjat'***Psja dva", "Psja dva");
                ((Hashtable)tempH["Devjat'"]).Add("Devjat'***Psja tri", "Psja tri");
                ((Hashtable)tempH["Devjat'"]).Add("Devjat'***Psja chetire", "Psja chetire");
                ((Hashtable)tempH["Devjat'"]).Add("Devjat'***Psja pjat'", "Psja pjat'");
                ((Hashtable)tempH["Devjat'"]).Add("Devjat'***Psja shest'", "Psja shest'");
                ((Hashtable)tempH["Devjat'"]).Add("Devjat'***Psja sem", "Psja sem");
                ((Hashtable)tempH["Devjat'"]).Add("Devjat'***Psja vosem", "Psja vosem");
                ((Hashtable)tempH["Devjat'"]).Add("Devjat'***Psja devjat'", "Psja devjat'");
                ((Hashtable)tempH["Devjat'"]).Add("Devjat'***Psja desjat'", "Psja desjat");
            }
            tempH.Add("Desjat'", new Hashtable());
            {
                ((Hashtable)tempH["Desjat'"]).Add("Desjat'***Psja odin", "Psja odin");
                ((Hashtable)tempH["Desjat'"]).Add("Desjat'***Psja dva", "Psja dva");
                ((Hashtable)tempH["Desjat'"]).Add("Desjat'***Psja tri", "Psja tri");
                ((Hashtable)tempH["Desjat'"]).Add("Desjat'***Psja chetire", "Psja chetire");
                ((Hashtable)tempH["Desjat'"]).Add("Desjat'***Psja pjat'", "Psja pjat'");
                ((Hashtable)tempH["Desjat'"]).Add("Desjat'***Psja shest'", "Psja shest'");
                ((Hashtable)tempH["Desjat'"]).Add("Desjat'***Psja sem", "Psja sem");
                ((Hashtable)tempH["Desjat'"]).Add("Desjat'***Psja vosem", "Psja vosem");
                ((Hashtable)tempH["Desjat'"]).Add("Desjat'***Psja devjat'", "Psja devjat'");
                ((Hashtable)tempH["Desjat'"]).Add("Desjat'***Psja desjat'", "Psja desjat");
            }
            return tempH;
        }
        public Hashtable AddSong(Hashtable cd)
        {
            int cdKol, songKol;
            //Hashtable cd = new Hashtable();
            //Hashtable[] cds;
            Console.WriteLine("Введите количество дисков");
            cdKol = int.Parse(Console.ReadLine());
            for (int i = 0; i < cdKol; i++)
            {
                StringBuilder cdName = new StringBuilder(50);
                Hashtable song = new Hashtable();
                Console.WriteLine("Введите название нового диска");
                cdName.Append(Console.ReadLine());
                Console.WriteLine("Введите количество песен в этом диске");
                songKol = int.Parse(Console.ReadLine());
                for (int j = 0; j < songKol; j++)
                {
                    StringBuilder songName = new StringBuilder(50);
                    Console.WriteLine("Введите название песни");
                    songName.Append(Console.ReadLine());
                    song.Add((cdName + "***" + songName), songName);
                }
                cd.Add(cdName, song);
            }
            return cd;
        }
        public string Choise(Hashtable tempH, int choise)
        {

            #region  костыль для обращения к ключу из Hashtable
            int iTemp = 0;
            StringBuilder sTemp = new StringBuilder();
            foreach (DictionaryEntry b in tempH)
            {
                iTemp++;
                sTemp.Clear().Append(b.Key);
                if (iTemp >= choise) break;
            }
            #endregion
            return sTemp.ToString();
        }
        public void ShowList(Hashtable tempH)
        {
            int count = 0;
            Console.WriteLine("Список дисков\n");
            foreach (DictionaryEntry a in tempH)
            {
                Console.WriteLine(count++ + ".   " + a.Key + "    " + a.Value);
            }
            Console.WriteLine("Нажмите любую клавишу что бы родолжить");
            Console.ReadKey();
            Console.WriteLine("----------------------------------------------------");
        }
        public Hashtable FindSong(Hashtable tempH, string mask)
        {
            Hashtable result = new Hashtable();
            int u = 0;
            foreach (DictionaryEntry pp in tempH)
            {
                foreach (DictionaryEntry ppp in (Hashtable)pp.Value)
                {

                    //Console.WriteLine("+++++++" + (ppp.Key.ToString()).Substring(0,mask.Length));
                    if (mask.Contains((ppp.Key.ToString()).Substring(0, mask.Length)))
                    {
                        result.Add(u++, ppp.Value);
                    }
                }
            }


            return result;
        }

        public void Vibor()

        {
            Hashtable tempH = CreateList();
            do
            {
                ShowList(tempH);
                Console.WriteLine("Выберите ноомер диска");
                int choise = int.Parse(Console.ReadLine());
                Console.WriteLine("Выберите действие:\n1-открыть\n2-удалить\n3-добавить\n4-поиск\n0-выход\n...другая клавиша");
                int vibor = int.Parse(Console.ReadLine());
                switch (vibor)
                {
                    case 1:
                        {
                            Hashtable inTemp = (Hashtable)tempH[Choise(tempH, choise)];
                            ShowList(inTemp);
                            break;
                        }
                    case 2:
                        {
                            tempH.Remove(Choise(tempH, choise).ToString());
                            break;
                        }
                    case 3:
                        {
                            tempH = AddSong(tempH);
                            break;
                        }
                    case 4:
                        {
                            StringBuilder mask = new StringBuilder();
                            Console.WriteLine("Введите имя исполнителя");
                            mask.Append(Console.ReadLine());
                            ShowList(FindSong(tempH, mask.ToString()));
                            break;
                        }
                    case 0:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default: break;
                }
            } while (true);


            // return;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {

            Stackk one = new Stackk();
            Queuee two = new Queuee();
            ArrayListt tree = new ArrayListt();
            HashTablee four = new HashTablee();
            try
            {
                //one.PervoeZadanie();
                //one.VtoroeZadanie();
                //one.TretieZadanie();
                //one.ChetvertoeZadanie();
                //two.PervoeZadanie();
                //two.VtoroeZadanie();
                //two.TretieZadanie();
                //two.ChetvertoeZadanie();
                //tree.PervoeZadanie();
                //tree.VtoroeZadanie();
                //tree.TretieZadanie();
                //tree.ChetvertoeZadanie();
                four.Vibor();
            }
            catch (Exception e)
            {
                Console.Write("!" + e);
            }
            Console.WriteLine("\nEnd");
            Console.ReadKey();

        }
    }
}
