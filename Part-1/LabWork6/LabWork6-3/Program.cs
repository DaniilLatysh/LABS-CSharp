using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork6_3
{
    /** Hashtable
     * Реализовать простейший каталог музыкальных компакт-дисков, который позволяет: 
     * 1.	Добавлять и удалять диски. 
     * 2.	Добавлять и удалять песни. 
     * 3.	Просматривать содержимое целого каталога и каждого диска в отдельности. 
     * 4.	Осуществлять поиск всех записей заданного исполнителя по всему каталогу.

     */
    class Program
    {

        public static Hashtable CreateList()
        {

            Hashtable table = new Hashtable();

            table.Add("Rock", new Hashtable());
            {
                ((Hashtable)table["Rock"]).Add("ACDC-Song1", "ACDC Song1");
                ((Hashtable)table["Rock"]).Add("ACDC-Song2", "ACDC Song2");
                ((Hashtable)table["Rock"]).Add("Metallica-Song1", "Metallica Song1");
                ((Hashtable)table["Rock"]).Add("Metallica-Song2", "Metallica Song2");
                ((Hashtable)table["Rock"]).Add("Metallica-Song3", "Metallica Song3");
                ((Hashtable)table["Rock"]).Add("Shokran-Song1", "Shokran Song1");
                ((Hashtable)table["Rock"]).Add("ArchEnemy-Song5", "Song5");
            }

            table.Add("Classic", new Hashtable());
            {
                ((Hashtable)table["Classic"]).Add("Bah-Song1", "bah Song1");
                ((Hashtable)table["Classic"]).Add("Bah-Song2", "Bah Song2");
                ((Hashtable)table["Classic"]).Add("LudovicoEinaudi-Song1", "Ludovico Einaudi Song1");
                ((Hashtable)table["Classic"]).Add("LudovicoEinaudi-Song2", "Ludovico Einaudi Song2");
                ((Hashtable)table["Classic"]).Add("Vivaldi-Song1", "Vivaldi Song1");
            }

            table.Add("Electronic", new Hashtable());
            {
                ((Hashtable)table["Electronic"]).Add("Scooter-Song1", "Scooter Song1");
                ((Hashtable)table["Electronic"]).Add("TheProdigy-Song1", "The Prodigy Song1");
                ((Hashtable)table["Electronic"]).Add("DepecheMode-Song1", "Depeche Mode Song1");
                ((Hashtable)table["Electronic"]).Add("Scooter-Song2", "Scooter Song2");
                ((Hashtable)table["Electronic"]).Add("Scooter-Song3", "Scooter Song3");
            }

            return table;

        }

        public static Hashtable AddDisk(Hashtable catalog) {

            Hashtable song = new Hashtable();
            
            StringBuilder diskName = new StringBuilder(100);
            StringBuilder signerName = new StringBuilder(30);
            StringBuilder songName = new StringBuilder(30);

            int count;
            Random rand = new Random();

            Console.Write(" Input disk name: ");
            diskName.Append(Console.ReadLine());

            Flag:
            try
            {
                Console.Write(" Input count of song: ");
                count = Int32.Parse(Console.ReadLine());
            }
            catch {
                Console.WriteLine(" Wrong number. Try again!");
                Console.ReadKey();
                goto Flag;
            }

            for (int i = 0; i < count; i++) {
                Console.Write(" Input Singer name: ");
                signerName.Append(Console.ReadLine());

                Console.Write(" Input Song: ");
                songName.Append(Console.ReadLine());

                song.Add(signerName + "-" + songName + "-" + rand.Next(-999, 999), signerName + " " + songName);

            }

            catalog.Add(diskName, song);
            return catalog;

        }

        public static void AddSong(Hashtable catalog) {
            int count = 1;
            Console.WriteLine("\n");
            foreach (DictionaryEntry de in catalog)
                Console.WriteLine(" " + count++ + ". " + de.Key);

            Console.Write("\n Input disk name: ");
            string name = Console.ReadLine();

            if (catalog.ContainsKey(name))
            {
                AddSongOnDisk((Hashtable)catalog[name]); // input in disk

            }
            else
            {
                Console.WriteLine(" Wrong input!");
            };

        }

        public static void AddSongOnDisk(Hashtable catalog) {
           
            StringBuilder signerName = new StringBuilder(30);
            StringBuilder songName = new StringBuilder(30);

            Random rand = new Random();
            
            Console.Write(" Input Singer name: ");
            signerName.Append(Console.ReadLine());

            Console.Write(" Input Song: ");
            songName.Append(Console.ReadLine());

            catalog.Add(signerName + "-" + songName + "-" + rand.Next(-999, 999), signerName + " " + songName);



        }



        public static void ShowList(Hashtable catalog)   //  show method
        {
            int countD = 1, countS = 1;
            Console.WriteLine("\n List of Disks:\n");
            foreach (DictionaryEntry disk in catalog)  // disks
            {
                Console.WriteLine(" " + countD++ + " " + disk.Key);
                foreach (DictionaryEntry song in (Hashtable)disk.Value) {  // songs
                    Console.WriteLine("    " + countS++ + "  " + song.Value);
                }
                countS = 1;
                Console.WriteLine("\n");
            }

            Console.ReadKey();
           
        }




        public static void DeleteSongFromDisk(Hashtable catalog)
        {
            int count = 1;
            Console.WriteLine("\n");
            foreach (DictionaryEntry de in catalog)
                Console.WriteLine(" " + count++ + ". " + de.Key);

            Console.Write("\n Input song: ");
            string songName = Console.ReadLine();

            if (catalog.ContainsKey(songName))
            {
                catalog.Remove(songName);             
                Console.WriteLine(" Success!");
            }
            else
            {
                Console.WriteLine(" Wrong input!");                
            }
            Console.ReadKey();
            
        }

        public static Hashtable Delete(Hashtable catalog) {

            Console.Clear();

        Flag:
            try
            {
                int count = 1;
                Console.WriteLine(" \n What delete? 1. Disk || 2. Song");
                byte choise = byte.Parse(Console.ReadLine());               
                
                switch (choise)
                {

                    case 1:// delete Disk
                        count = 1;
                        Console.WriteLine("\n");
                        foreach (DictionaryEntry de in catalog)
                            Console.WriteLine(" " + count++ + ". " + de.Key);

                        Console.Write("\n Input disk name: ");      
                        string mask = Console.ReadLine();
                        catalog.Remove(mask);  
                        break;

                    case 2:  //Delete song
                        count = 1;
                        Console.WriteLine("\n");
                        foreach (DictionaryEntry de in catalog)
                            Console.WriteLine(" " + count++ + ". " + de.Key);

                        Console.Write("\n Input disk name: ");
                        string name = Console.ReadLine();

                        if (catalog.ContainsKey(name))
                        {
                            DeleteSongFromDisk((Hashtable)catalog[name]); // input in disk

                        }
                        else
                        {
                            Console.WriteLine(" Wrong input!");
                        };
                        break;
                    
                     
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n Wrong input. Try again!");
                Console.ReadKey();
                goto Flag;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
                Console.ReadKey();
            }

            return catalog;
        }


        static void Search(Hashtable music)
        {
            int counter = 1;
            Console.Write("\n Input singer: ");
            string name = Console.ReadLine();
            
            ICollection keys = music.Keys;
            Console.WriteLine("\n All singer song: ");
            foreach (string key in keys)
            {
                foreach (DictionaryEntry songs in (Hashtable)music[key])
                {
                    if (songs.Value.ToString().Contains(name))
                    {
                        Console.WriteLine(" "+ counter++ + ". " +songs.Value);
                    }
                }
            }
            Console.ReadLine();
        }


        static void Main(string[] args)
        {

            Hashtable catalog = CreateList();

        Flag:
            try
            {

                while (true)
                {
                    Console.Clear();
                   

                    Console.WriteLine("\n 1. Add Disk and Song");
                    Console.WriteLine(" 2. Add Song");
                    Console.WriteLine(" 3. Delete Disk or Song");
                    Console.WriteLine(" 4. Show all");
                    Console.WriteLine(" 5. Find Song");
                    Console.WriteLine(" 6. Exit");
                    Console.Write(" Your choise: ");

                    int choise = int.Parse(Console.ReadLine());


                    switch (choise)
                    {
                        case 1:
                            catalog = AddDisk(catalog);
                            break;
                        case 2: AddSong(catalog);
                            break;
                        case 3:
                            catalog = Delete(catalog);
                            break;
                        case 4:
                            ShowList(catalog);
                            break;
                        case 5:
                            Search(catalog);
                            break;
                        case 6: Environment.Exit(0);
                            break;
                        default:
                            break;
                    }

                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n Wrong input. Try again!");
                Console.ReadKey();
                goto Flag;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
                Console.ReadKey();
            }

        }
    }
}
