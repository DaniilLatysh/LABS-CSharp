

using System;//подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using System.IO; // библиотека с набором классов для работы с файлами
using System.Linq;//Пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают запросы, использующие LINQ (Language-Integrated Query).
using System.Text; //содержит классы, представляющие кодировки ASCII и Юникод.



/*
 * Латыш Дании Николаевич 50326-1 Вариант 4 
 * 
 * 
 * Аэропорт. Касса аэропорта имеет список тарифов на различные направления. При покупке билета регистрируются паспортные данные. 
 * Всем пассажирам предоставлена скидка. Одним пассажирам скидка задана в процентах, другие имеют фиксированную скидку.

 * Система должна:
 * позволять вводить данные о тарифах;
 * позволять вводить паспортные данные пассажира и регистрировать покупку билета;
 * рассчитывать стоимость купленных пассажиром билетов;
 * рассчитывать стоимость всех проданных билетов с учетом скидок.
 * 
 * Добавить обработку исключительных ситуаций:
 * длина поля наименования направления меньше 3 символов.
 * величина стоимости с учетом скидки имеет отрицательное значение
 * Добавить перегруженный бинарный  оператор для увеличения стоимости перелета.
 * 
 * **/

namespace KSR // пространство имён
{
    class Client { //объявление класса Клиент
        private string name; //имя клиента
        private string numPassport; //номер паспорта
        private static Tariff tariffS; // объект тарифа
        private static int countTicketsS; //количество билетов
        private static double costWithSale; //Стоимость билета во скидкой

        public Client(string name, string numPassport, Tariff tariff, int countTickets, double costWSale) //конструктор класса Клиент
        {
            this.name = name; //присвоение входящей переменной
            this.numPassport = numPassport; // присвоенеие 
            costWithSale = costWSale; // присвоенеие 
            tariffS = tariff; // присвоенеие 
            countTicketsS = countTickets; // присвоенеие 
        }

        private static double TotalCost() //метод подсчёта полной стоимости
        {
            double rez = costWithSale * countTicketsS; //расчёт стоимость со скидкой * количество
            return rez; // возврат результата
        }


        public override string ToString() // метод строчного представления класса
        {
            return name + " " + numPassport + " " + countTicketsS + " " + TotalCost().ToString() + " " + tariffS.ToString(); //возврат результата
        } 
    }

    class Tariff //объявление класса тариф
    {
        private string nameTarrif; //наименование
        private string departure; //точка отправления
        private string arrival; //точка прибытия
        private DateTime dateOfDeparture; //дата отправления
        private DateTime dateOfArrival; //дата приытия
        private double cost; //стоимость билета без скидки

        public Tariff(string nameTarrif, string departure, string arrival, DateTime dateOfDeparture, DateTime dateOfArrival, double cost) //конструктор класса тариф
        {
            this.nameTarrif = nameTarrif; // присвоенеие 
            this.departure = departure; // присвоенеие 
            this.arrival = arrival; // присвоенеие 
            this.dateOfDeparture = dateOfDeparture; // присвоенеие 
            this.dateOfArrival = dateOfArrival; // присвоенеие 
            this.cost = cost; // присвоенеие 
        }

        public double Cost //получение стоимости
        {
            get { return cost; } //возврат результата
            set { cost = value; } // прегрузка
        }

        public static Tariff operator +(Tariff obj1, double obj2) // бинарный  оператор для увеличения стоимости перелета
        {
            obj1.Cost += obj2; // присвоение параметру объекта числа
            return obj1; // возвращение результата
        }

        public override string ToString() // метод строчного представления класса
        {
            Console.WriteLine("\n###########################################################################\n"); // рамка
            Console.WriteLine(" TARIFF:    " + nameTarrif); //вывод на экран наименования тарифа
            Console.WriteLine(" DEPARTURE: {0} || {1}", departure, dateOfDeparture.ToString("yyyy-MM-dd")); //вывод на экран точки и даты отправления
            Console.WriteLine(" ARRIVAL:   {0} || {1}", arrival, dateOfArrival.ToString("yyyy-MM-dd")); //вывод на экран точки и даты прибытия
            Console.WriteLine(" COST:      {0}", cost); //вывод на экран 
            Console.WriteLine("\n###########################################################################\n"); // рамка

            return nameTarrif + " " //возврат результата
                + departure + " " //возврат результата
                + arrival + " " //возврат результата
                + dateOfDeparture.ToString("yyyy-MM-dd") + " " //возврат результата
                + dateOfArrival.ToString("yyyy-MM-dd") + " " //возврат результата
                + cost.ToString(); //возврат результата
        }
    }



    class Program // класс программы
    {

        /*
         *   ADD TARIFF
         * 
         * **/

        public static void AddTariff() //метод добавления тарифа
        {
            try //объявления начала точки обработыки исключений
            {
            FlagName: //точка возврата для имени
                Console.Write("\n TARIFF NAME: "); // вывод на экран сообщения
                string nameTarrif = Console.ReadLine(); //считывание строки

                if (nameTarrif.Length < 4) //проверка на длинну строки
                {
                    Console.Write("\n You must input greater 4 sybhol: "); // вывод на экран сообщения
                    Console.ReadKey(); //ожидание ввода символа
                    goto FlagName; //отправиться к точке возврата
                }

                Console.Write("\n--------------------------------------------"); //вывод рамки

            FlagDep: //точка возвращения
                Console.Write("\n POINT OF DEPARTURE: "); // вывод на экран сообщения
                string departure = Console.ReadLine(); //считывание строки

                if (departure.Length < 4) //проверка на длинну строки
                {
                    Console.Write("\n You must input greater 4 sybhol: "); // вывод на экран сообщения
                    Console.ReadKey(); //ожидание ввода символа
                    goto FlagDep; //отправиться к точке возврата
                }

                Console.WriteLine("\n DEPARTURE DATE"); // вывод на экран сообщения

                Console.Write(" Year: "); // вывод на экран сообщения
                int year = Int32.Parse(Console.ReadLine()); // считывание года
                Console.Write(" Month: "); // вывод на экран сообщения
                int month = Int32.Parse(Console.ReadLine()); // считывание месяца
                Console.Write(" Day: "); // вывод на экран сообщения
                int day = Int32.Parse(Console.ReadLine()); // считывание дня

                DateTime dateOfDeparture = new DateTime(year, month, day); // создание объекта даты

                Console.Write("\n--------------------------------------------"); // Рамка

            FlagArr: // точка возврата
                Console.Write("\n POINT OF ARRIVAL: "); // вывод на экран сообщения
                string arrival = Console.ReadLine(); // считывание строки

                if (arrival.Length < 4) //проверка на длинну строки
                {
                    Console.Write("\n You must input greater 4 sybhol: "); // вывод на экран сообщения
                    Console.ReadKey(); // одидание ввода символа
                    goto FlagArr; // отправиться к точке возврата
                }

            FlagDate: // точка возврата
                Console.WriteLine("\n ARRIVAL DATE"); // вывод на экран сообщения
                Console.Write(" Year: "); // вывод на экран сообщения
                year = Int32.Parse(Console.ReadLine()); // считывание года
                Console.Write(" Month: "); // вывод на экран сообщения
                month = Int32.Parse(Console.ReadLine()); // считывание месяца
                Console.Write(" Day: "); // вывод на экран сообщения
                day = Int32.Parse(Console.ReadLine());// считывание дня

                DateTime dateOfArrival = new DateTime(year, month, day); // создание объекта даты

                if (dateOfDeparture > dateOfArrival) // проверка на корректность ввода даты
                {
                    Console.WriteLine("\n /(0 o 0)/ Arrival Date must be greater than the Departure Date! Retruy! "); // вывод на экран сообщения
                    goto FlagDate; // отправиться к точке возврата
                }

            FlagCost: // точка возврата
                Console.Write("\n COST of tarrif: "); // вывод на экран сообщения
                double cost = double.Parse(Console.ReadLine()); // чтение числа
                if (cost < 1) // проверка на нулевую или отрицательную цену
                {
                    Console.WriteLine("\n /(0 o 0)/ Too small cost! Retruy! ");// вывод на экран сообщения
                    goto FlagCost; // отправиться к точке возврата
                }

                StreamWriter save = new StreamWriter("base.txt", true, Encoding.Default); // открыте файла для записи (режим доавления строки)

                Tariff tar = new Tariff(nameTarrif, departure, arrival, dateOfDeparture, dateOfArrival, cost); // создание объекта класса Тариф
                save.WriteLine(tar); // записать в файл параметров объекта 
                save.Close(); // Закрытие файла
            }
            catch (Exception e) // ловим все исключительные ситуации
            {
                Console.WriteLine(" " + e.Message); // вывод на экран текста ошибки
                Console.ReadKey(); // ожидание ввода символа
                Console.Clear(); // чистка консоли
                AddTariff(); // вызов метода
            }

        }

        static void PriceUP() // метод повышения цены
        {
            Console.Write(" Allowance: ");  // вывод на экран сообщения
            double up = double.Parse(Console.ReadLine()); // чтение строки

            StreamReader read = new StreamReader("base.txt", Encoding.Default); // открытие файла для чтения 
            string line; // объявление строчной переменной
            
            List<Tariff> list = new List<Tariff>(); // список объектов

            while ((line = read.ReadLine()) != null) //цикл чтения построчно пока файл не закончится 
            {
                string[] var = line.Split(' '); // разбиение строки на массив строк
                Tariff t = new Tariff(var[0], var[1], var[2], Convert.ToDateTime(var[3]), Convert.ToDateTime(var[4]), Convert.ToInt32(var[5])) + Math.Abs(up); // добавляем стоимость
                list.Add(t); // добавление объекта в список
            }
            read.Close(); //закрытие файла
  
            StreamWriter save = new StreamWriter("base.txt", false, Encoding.Default); // открытие файла для перезаписи
            foreach (Tariff t in list) { // перебор объектов по списку
                save.WriteLine(t); // запись данных объекта в файл
            }
            save.Close(); // закрытие файла

        }


        static double Sale(Tariff summ) { // Метод расчёта скидки
            try // точка старта обработки исключений
            {
                Console.WriteLine("\n SALE: Fix - 1 || % - 2"); // Вывод на экран сообщения
                byte sale = byte.Parse(Console.ReadLine()); // чтение строки
                double res; // объявление переменной результата

                if (sale == 1) // проверка на ввод еденицы
                {
                    Console.Write(" Input sale summ: "); // Вывод на экран сообщения
                    double fix = double.Parse(Console.ReadLine()); // чтение строки
                    if (fix > summ.Cost) // проверка на то, чтобы скидка не была больше стоимости билета
                    {
                        Console.Write(" Wrong! Sale must be less Cost! "); // Вывод на экран сообщения
                        Sale(summ); //вызов метода расчёта скидки
                    }
                    else // иначе
                    {
                        return res = summ.Cost - fix; // расчёт конечной стоимости и возврат значения
                    }
                }
                else if (sale == 2) // проверка на ввод двойки
                {
                    Console.Write(" Input sale %: "); // Вывод на экран сообщения
                    double per = double.Parse(Console.ReadLine()); // считывание строки
                    if (per > 100 || per <= 0) // проверка на адекватность скидки
                    {
                        Console.Write(" Wrong! Sale must be less Cost! ");  // Вывод на экран сообщения
                        Sale(summ); //вызов метода расчёта скидки
                    }
                    else // иначе
                    {
                        return res = summ.Cost - (per * 100 / summ.Cost); // расчёт конечной стоимости и возврат значения
                    }
                }
                else
                {
                    Console.Write(" Wrong! Sale must be less Cost! "); // Вывод на экран сообщения
                    Sale(summ);  //вызов метода расчёта скидки
                }
            }
            catch (Exception e) // ловим все исключительные ситуации
            {
                Console.WriteLine(" " + e.Message); // вывод текста ошибки
                Console.ReadKey(); // ожидание ввода 
                Console.Clear(); // чистка консоли
                Sale(summ); //вызов метода расчёта скидок

            }
            return summ.Cost; //возврат значения
        }


        static void AddClient() // Метод создания нового клиента и сохранения данных в файл
        {
            try // точка старта обработки исключений
            {
                FlagName: // точка возврата

                Console.Write("\n Full name:"); // Вывод текста на экран
                string name = Console.ReadLine(); // Считывание строки

                if (name.Length < 4) //проверка на длинну строки
                {
                    Console.Write("\n Wrong input! Name must be greater 4 symbol!"); // вывод на экран сообщения
                    Console.ReadKey(); // одидание ввода символа
                    goto FlagName; // отправиться к точке возврата
                }

                FlagPas:
                Console.Write("\n Passport number:"); // Вывод текста на экран
                string passport = Console.ReadLine(); // Считывание строки

                if (passport.Length < 8) {
                    Console.Write("\n Wrong input! Passport  must be greater 8! "); // вывод на экран сообщения
                    Console.ReadKey(); // одидание ввода символа
                    goto FlagPas; // отправиться к точке возврата
                }

                Console.Write("\n--------------------------------------------"); // рамка
                ShowTariff(); // вызов метода отобрадения всех тарифов
                Console.Write("\n--------------------------------------------"); // рамка


                Console.Write("\n Input name TARIFF:"); // Вывод текста на экран
                string tar = Console.ReadLine(); // Считывание строки

                FlagCount: // точка возврата

                Console.Write(" Count of TICKETS: "); // Вывод текста на экран
                int count = Int32.Parse(Console.ReadLine()); // Считывание строки

                if (count < 1) //проверка на идиотизм
                {
                    Console.Write("\n Count must be greater 0 "); // вывод на экран сообщения
                    Console.ReadKey(); // одидание ввода символа
                    Console.Clear(); // чистка консоли
                    goto FlagCount; // отправиться к точке возврата
                }



                // Find tariff in file

                StreamReader readT = new StreamReader("base.txt", Encoding.Default); // Открытие файла для чтения

                string line; // объявление строчной переменной
                while ((line = readT.ReadLine()) != null) // цикл, считыввающий файл построчно, пока там есть информация
                {
                    string[] var = line.Split(' '); // разбиение строки на строчный массив
                    if (var[0] == tar) // проверка на искомый тариф
                    {
                        Tariff t = new Tariff(var[0], var[1], var[2], Convert.ToDateTime(var[3]), Convert.ToDateTime(var[4]), Convert.ToInt32(var[5])); // запись тарифа из файла

                        Client client = new Client(name, passport, t, count, Sale(t));  // создание объекта клинт

                        StreamWriter save = new StreamWriter("clients.txt", true, Encoding.Default); // открыте файла для записи (режим доавления строки)
                        save.WriteLine(client); // сохранение строки в файл
                        save.Close(); // закрытие файла записи
                        break; // прерывание цикла
                    }
                }
                readT.Close(); // закрытие файла чтения
            }
            catch (Exception e) // ловим все исключения
            {
                Console.Clear(); // чистка экрана консоли
                Console.WriteLine(" " + e.Message); // вывод текста ошибки
                Console.ReadKey(); // ожидание ввода символа
                AddClient(); // вызов метода занова
            }
        }

        static void Calac() { // Метод расчёта количества и стоимости проданных билетов
            try { // старта обработки исключений

                StreamReader read = new StreamReader("clients.txt", Encoding.Default); // открытие файла для чтения 
                string line; // объявление строчной переменной
                int count = 0; // объявление перемнной подсчёта колличества билетов
                double amount = 0; // объявление перемнной подсчёта стоимости

                while ((line = read.ReadLine()) != null) //цикл чтения построчно пока файл не закончится 
                {
                    string[] var = line.Split(' '); // разбиение строки на массив строк
                    count += Convert.ToInt32(var[2]); // суммируем билеты
                    amount += Convert.ToInt32(var[3]); // суммируем стоимость
                }
                read.Close(); //закрытие файла

                Console.WriteLine(" TICKETS | SUMM"); //вывод строки на экран
                Console.WriteLine("    {0}  |   {1}", count, amount); // вывод результата

            }
            catch (Exception e) // ловим все исключительные ситуации
            {
                Console.Clear(); // чистка консоли
                Console.WriteLine(" " + e.Message); // вывод текста ошибки
                Console.ReadKey(); // ожидание ввода символа
            }
        }

        


        static void ShowTariff() // метод чтения всех тарифов из файла
        {
            try
            {
                StreamReader read = new StreamReader("base.txt", Encoding.Default); // открытие файла для чтения 

                Console.Write("\n NAME TARIFF | DEPARTURE | ARRIVAL | DEP. DATE | ARR. DATE | COST\n"); // Вывод строки на экран
                Console.WriteLine("\n" + read.ReadToEnd()); // вывод результата целеком
                read.Close(); // закрываем файл
            }
            catch // ловим все исключительные ситуации 
            {
                File.Create("base.txt"); // создание файла
            }
        }

        static void ShowKlients() // метод чтения всех клиентов из файла
        {
            try // старта обработки исключений
            {
                StreamReader read = new StreamReader("clients.txt", Encoding.Default); // открытие файла для чтения 

                Console.Write("\n NAME  | PASSPORT | COUNT TICKETS | TOTAL COST | TARIFF \n"); // Вывод строки на экран
                Console.WriteLine("\n" + read.ReadToEnd()); // вывод результата целеком
                read.Close(); // закрываем файл
            }
            catch // ловим все исключительные ситуации 
            {
                File.Create("clients.txt"); // создание файла
            }
        }

        static void Main(string[] args) // точка входа в программу 
        {
            while (true) // бесконечный цикл
            { 
                try // старта обработки исключений
                {
                    Console.Clear(); // чистка консоли
                    Console.WriteLine("\n 1. Add TARIFF"); // Вывод строки на экран
                    Console.WriteLine(" 2. Show TARIFF"); // Вывод строки на экран
                    Console.WriteLine(" 3. Add CLIENT"); // Вывод строки на экран
                    Console.WriteLine(" 4. Show CLIENTS"); // Вывод строки на экран
                    Console.WriteLine(" 5. CALC"); // Вывод строки на экран
                    Console.WriteLine(" 6. Rise PRICEs"); // Вывод строки на экран
                    Console.WriteLine(" 7. Exit"); // Вывод строки на экран
                    Console.Write(" Your choise: "); // Вывод строки на экран

                    int choise = int.Parse(Console.ReadLine()); // считывание строки

                    switch (choise) //переключатель 
                    {
                        case 1: // кейс 
                            AddTariff();  // вызов метода добавления тарифа
                            break; // прерывание
                        case 2: // кейс 
                            ShowTariff(); // вызов метода чтения тарифов из файла
                            break; // прерывание
                        case 3: // кейс 
                            AddClient(); // вызов метода добавления клиента
                            break; // прерывание
                        case 4: // кейс 
                            ShowKlients(); // вызов метода чтения клиентов из файла
                            break; // прерывание
                        case 5: // кейс 
                            Calac(); //расчёт проданных билетов
                            break; // прерывание
                        case 6: // кейс 
                            PriceUP(); // повышение цены
                            break; // прерывание
                        case 7: // кейс 
                            Environment.Exit(0); // завершение программы
                            break; // прерывание
                        default: // по умолчанию
                            break; // прерывание
                    }
                    Console.ReadKey(); // ожидание ввода символа
                }
                catch (Exception e) // ловим все исключительные ситуации
                {
                    Console.Clear(); // чистка консоли
                    Console.WriteLine("\n " + e.Message); // вывод текста ошибки
                    Console.ReadKey(); // ожидание ввода символа
                }
            }
        }
    }
}
