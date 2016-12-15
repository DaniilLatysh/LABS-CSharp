using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Permissions;

namespace LabWork4_3
{
    class Program
    {


        static void Search(string path, string mask) {

            int counter = 0;
            string[] dir = Directory.GetDirectories(@"" + path + "", "*" + mask + "*", SearchOption.AllDirectories);
            string[] files = Directory.GetFiles(@"" + path + "", "*" + mask + "*", SearchOption.AllDirectories);




            Console.WriteLine("\n ============== Directory List ==============\n ");                     // Список директорий
            foreach (var item in dir) {

                Console.WriteLine(" {0} : {1}", counter, item);
                counter++;

            }
            counter = 0;




            Console.WriteLine("\n ============== File List ==============\n ");                     // Список файлов
            foreach (var item in files)
            {
                Console.WriteLine(" {0} : {1}", counter, item);
                counter++;
            }
            counter = 0;




            string[] arr = dir.Concat(files).ToArray();

            Console.WriteLine("\n ============== All List ==============\n ");                     // Список всех 
            foreach (var item in arr)
            {
                Console.WriteLine(" {0} : {1}", counter, item);
                counter++;
            }



            Console.WriteLine(" 1. Delete ALL item's");
            Console.WriteLine(" 2. Delete ONE item's");
            Console.WriteLine(" 3. Delete RANGE item's");
            Console.WriteLine(" 4. EXIT");

            int choise = Int32.Parse(Console.ReadLine());
            

            switch (choise) {
                case 1:
                    DelAll(dir, files);
                    break;

                case 2:
                    DelOne(arr);
                    break;
               
                case 3:
                    DelRange(arr);
                    break;

                case 4: Environment.Exit(0);
                    break;

                default: break;
            }

        }





        static void DelAll(string[] dir, string[] files) {               // Удалить все файлы
            try
            {
                foreach (var f in files)
                {
                        File.SetAttributes(f, FileAttributes.Normal);
                        File.Delete(f);
                }
                foreach (var d in dir)
                {
                    try
                    {
                        Directory.Delete(d);
                    }
                    catch {
                        DirectoryInfo di = new DirectoryInfo(@"" +d+ "");
                        di.Delete(true);
                        Directory.Delete(d);
                    }
                }
                Console.WriteLine("\n Files and Dir's was deleted! \n ");
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        


        static void DelOne(string[] arr) {           // Удаление по индексу

            Console.Write("\n Input index: ");
            int numItem = Int32.Parse(Console.ReadLine());

            try
            {
                File.SetAttributes(arr[numItem], FileAttributes.Normal);
                File.Delete(arr[numItem]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                DirectoryInfo di = new DirectoryInfo(@"" + arr[numItem] + "");
                di.Delete(true);
                Directory.Delete(arr[numItem]);
            }
            Console.WriteLine("\n File was deleted! \n ");

        }


        static void DelRange(string[] arr) {                 // Удалить по диапазону


            Console.Write(" FROM: ");
            int from = Int32.Parse(Console.ReadLine());
            Console.Write(" TO: ");
            int to = Int32.Parse(Console.ReadLine());

            for (int i = from; i <= to; i++) {
                try
                {
                    File.SetAttributes(arr[i], FileAttributes.Normal);
                    File.Delete(arr[i]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    DirectoryInfo di = new DirectoryInfo(@"" + arr[i] + "");
                    di.Delete(true);
                    Directory.Delete(arr[i]);
                }
                Console.WriteLine("\n Files was deleted! \n ");
            }

        }


        static void Main(string[] args)
        {

            try
            {
                Console.Write("\n Input mask for search: ");
                string mask = Console.ReadLine();

                Console.Write(" Path: ");
                string path = Console.ReadLine();

                Search(path, mask);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
