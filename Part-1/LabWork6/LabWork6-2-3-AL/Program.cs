using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace LabWork6_2_3_AL
{

    /*  Array List
     * 3.	Дан файл, содержащий информацию о студентах: 
     * фамилия, имя, отчество, номер группы, оценки по трем предметам текущей сессии. 
     * За один просмотр файла напечатать элементы файла в следующем порядке:  
     * сначала все данные о студентах, успешно сдавших сессию, потом данные об остальных студентах, 
     * сохраняя исходный порядок в каждой группе сотрудников. 

     * 4.	Дан файл, содержащий информацию о студентах: 
     * фамилия, имя, отчество, номер группы, оценки по трем предметам текущей сессии. 
     * За один просмотр файла напечатать элементы файла в следующем порядке: 
     * сначала все данные о студентах, успешно обучающихся на 4 и 5, 
     * потом данные об остальных студентах, сохраняя исходный порядок в каждой группе сотрудников. 
     */
    class Program
    {

        public struct Student
        {
            public string secondName { get; set; }
            public string firstName { get; set; }
            public string midleName { get; set; }
            public int groupNum { get; set; }
            public int exOne { get; set; }
            public int exTwo { get; set; }
            public int exThree { get; set; }

            public override string ToString()
            {
                Console.WriteLine("\n " + secondName + " " + firstName + " " + midleName + " " + groupNum + " " + exOne + " " + exTwo + " " + exThree);
                return string.Format("\n {0} {1} {2} {3} {4} {5}", secondName, firstName, midleName, groupNum, exOne, exTwo, exThree);
            }
        }


        public static void sortStud(ArrayList all)
        {     // sort by exam
            foreach (Student s in all)
            {
                if (s.exOne > 3 && s.exTwo > 3 && s.exThree > 3)
                {
                    s.ToString();
                }
            }
            foreach (Student s in all)
            {
                if (s.exOne < 4 || s.exTwo < 4 || s.exThree < 4)
                {
                    s.ToString();
                }
            }
        }

        public static void sortByGoodStud(ArrayList all)
        {                                                                   // sort by good stud
            foreach (Student s in all)
            {
                if (s.exOne > 8 && s.exTwo > 8 && s.exThree > 8)
                {
                    s.ToString();
                }
            }
            foreach (Student s in all)
            {
                if (s.exOne < 9 || s.exTwo < 9 || s.exThree < 9)
                {
                    s.ToString();
                }
            }
        }

        static void Main(string[] args)
        {

            try
            {
                ArrayList all = new ArrayList ();
                StreamReader read = new StreamReader(@"input.txt", Encoding.Default);

                while (!read.EndOfStream)
                {
                    Student s = new Student();
                    string line = read.ReadLine();
                    string[] spl = line.Split(' ');
                    s.secondName = spl[0];
                    s.firstName = spl[1];
                    s.midleName = spl[2];
                    s.groupNum = Convert.ToInt32(spl[3]);
                    s.exOne = Convert.ToInt32(spl[4]);
                    s.exTwo = Convert.ToInt32(spl[5]);
                    s.exThree = Convert.ToInt32(spl[6]);

                    all.Add(s);  // add stud to queue

                }

                Console.WriteLine("\n Sort by EXAM: ");
                sortStud(all);
                Console.ReadKey();

                Console.WriteLine("\n Sort by GOOD students: ");
                sortByGoodStud(all);
                Console.ReadKey();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
