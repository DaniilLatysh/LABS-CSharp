using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LabWork6_2
{

    /* 1.	Дан файл, содержащий информацию о сотрудниках фирмы: 
     * фамилия, имя, отчество, пол, возраст, размер зарплаты. За один просмотр файла напечатать элементы файла в следующем порядке: 
     * сначала все данные о сотрудниках, зарплата которых меньше 10000, потом данные об остальных сотрудниках, сохраняя исходный 
     * порядок в каждой группе сотрудников. 
     * 2.	Дан файл, содержащий информацию о сотрудниках фирмы: фамилия, имя, отчество, пол, возраст, размер зарплаты. 
     * За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о сотрудниках младше 30 лет, 
     * потом данные об остальных сотрудниках, сохраняя исходный порядок в каждой группе сотрудников. 
     */
    class Program
    {
        public struct Staff
        {
            public string secondName { get; set; }
            public string firstName { get; set; }
            public string midleName { get; set; }
            public string sex { get; set; }
            public int age { get; set; }
            public int salary { get; set; }

            public override string ToString()
            {
                Console.WriteLine("\n " + secondName + " " + firstName + " " + midleName + " " + sex + " " + age + " " + salary);
                return string.Format("\n {0} {1} {2} {3} {4} {5}", secondName, firstName, midleName, sex, age, salary);
            }
        }

        public static void Salary(Queue<Staff> all) {          // Sort by salaru
            foreach (Staff s in all) {
                if (s.salary > 10000) {
                    s.ToString();
                }
            }
            foreach (Staff s in all)
            {
                if (s.salary <= 10000)
                {
                    s.ToString();
                }
            }
        }

        public static void Age(Queue<Staff> all)       //sort by age
        {
            foreach (Staff s in all)
            {
                if (s.age < 30)
                {
                    s.ToString();
                }
            }
            foreach (Staff s in all)
            {
                if (s.salary >= 30)
                {
                    s.ToString();
                }
            }
        }




        static void Main(string[] args)
        {
            try
            {
                Queue<Staff> all = new Queue<Staff>();
                StreamReader read = new StreamReader(@"input.txt", Encoding.Default);

                while (!read.EndOfStream)
                {
                    Staff s = new Staff();
                    string line = read.ReadLine();
                    string[] spl = line.Split(' ');
                    s.secondName = spl[0];
                    s.firstName = spl[1];
                    s.midleName = spl[2];
                    s.sex = spl[3];
                    s.age = Convert.ToInt32(spl[4]);
                    s.salary = Convert.ToInt32(spl[5]);

                    all.Enqueue(s);  // add member to queue

                }

                Console.WriteLine(" Sort by SALARY: ");
                Salary(all); 
                Console.ReadKey();

                Console.WriteLine(" Sort by AGE: ");
                Age(all);
                Console.ReadKey();

            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

        }
    }
}
