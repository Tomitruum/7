using System.Xml.Serialization;

[Serializable]
public struct Igrushka
{
    public string nazvanie;
    public double cena;
    public int vozrastOt;
    public int vozrastDo;
}

public static class Zadaniya
{
    // Задание 1
    public static void Zadanie1()
    {
        string imyaFaila = "chisla1.txt";
        int kolichestvo = Proverka.VvodCeloe("Сколько чисел записать в файл? ");
        SozdatFail1(imyaFaila, kolichestvo);

        double summa = 0;
        int schetchik = 0;

        using (StreamReader chtenie = new StreamReader(imyaFaila))
        {
            string stroka;
            while ((stroka = chtenie.ReadLine()) != null)
            {
                if (int.TryParse(stroka, out int chislo))
                {
                    summa += chislo;
                    schetchik++;
                }
            }
        }

        if (schetchik > 0)
            Console.WriteLine("Среднее арифметическое: " + (summa / schetchik));
        else
            Console.WriteLine("Файл пустой.");
    }

    private static void SozdatFail1(string imya, int kolvo)
    {
        Random rnd = new Random();
        using (StreamWriter zapis = new StreamWriter(imya))
        {
            for (int i = 0; i < kolvo; i++)
            {
                zapis.WriteLine(rnd.Next(1, 101));
            }
        }
    }

    // Задание 2
    public static void Zadanie2()
    {
        string imyaFaila = "chisla2.txt";
        int strok = Proverka.VvodCeloe("Сколько строк записать в файл? ");
        int chiselVStroke = Proverka.VvodCeloe("Сколько чисел в строке? ");
        SozdatFail2(imyaFaila, strok, chiselVStroke);

        long proizvedenie = 1;
        bool estNechetnie = false;

        using (StreamReader chtenie = new StreamReader(imyaFaila))
        {
            string stroka;
            while ((stroka = chtenie.ReadLine()) != null)
            {
                string[] chasti = stroka.Split(' ');
                foreach (string chisloStr in chasti)
                {
                    if (int.TryParse(chisloStr, out int chislo) && chislo % 2 != 0)
                    {
                        proizvedenie *= chislo;
                        estNechetnie = true;
                    }
                }
            }
        }

        if (estNechetnie)
            Console.WriteLine("Произведение нечётных: " + proizvedenie);
        else
            Console.WriteLine("Нет нечётных чисел.");
    }

    private static void SozdatFail2(string imya, int strok, int chiselVStroke)
    {
        Random rnd = new Random();
        using (StreamWriter zapis = new StreamWriter(imya))
        {
            for (int i = 0; i < strok; i++)
            {
                for (int j = 0; j < chiselVStroke; j++)
                {
                    zapis.Write(rnd.Next(1, 21) + " ");
                }
                zapis.WriteLine();
            }
        }
    }

    // Задание 3
    public static void Zadanie3()
    {
        string ishodniy = "tekst3.txt";
        string noviy = "rezultat3.txt";
        Console.WriteLine("Введите строки (для окончания введите пустую строку):");
        using (StreamWriter zapis = new StreamWriter(ishodniy))
        {
            string stroka;
            while (true)
            {
                stroka = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(stroka)) break;
                zapis.WriteLine(stroka);
            }
        }

        using (StreamReader chtenie = new StreamReader(ishodniy))
        using (StreamWriter zapis = new StreamWriter(noviy))
        {
            string stroka;
            while ((stroka = chtenie.ReadLine()) != null)
            {
                bool estBukva = false;
                foreach (char c in stroka)
                {
                    if (char.IsLetter(c))
                    {
                        estBukva = true;
                        break;
                    }
                }
                if (!estBukva)
                    zapis.WriteLine(stroka);
            }
        }

        Console.WriteLine("Результат записан в файле: " + noviy);
    }

    // Задание 4
    public static void Zadanie4()
    {
        string imyaFaila = "bin4.dat";
        int kolvo = Proverka.VvodCeloe("Сколько чисел записать в бинарный файл? ");
        SozdatBinarnyyFail4(imyaFaila, kolvo);

        double maxModul = double.MinValue;

        using (BinaryReader chitat = new BinaryReader(File.Open(imyaFaila, FileMode.Open)))
        {
            int index = 1;
            while (chitat.BaseStream.Position < chitat.BaseStream.Length)
            {
                double chislo = chitat.ReadDouble();
                if (index % 2 != 0)
                {
                    double modul = Math.Abs(chislo);
                    if (modul > maxModul)
                        maxModul = modul;
                }
                index++;
            }
        }

        Console.WriteLine("Наибольшее из значений модулей (нечётные): " + maxModul);
    }

    private static void SozdatBinarnyyFail4(string imya, int kolvo)
    {
        Random rnd = new Random();
        using (BinaryWriter zapis = new BinaryWriter(File.Open(imya, FileMode.Create)))
        {
            for (int i = 0; i < kolvo; i++)
            {
                zapis.Write(rnd.NextDouble() * 200 - 100); // chisla ot -100 do +100
            }
        }
    }

    // Задание 5
    public static void Zadanie5()
    {
        string imyaFaila = "igrushki.xml";
        int kolvo = Proverka.VvodCeloe("Сколько игрушек записать? ");
        SozdatIgrushkiFail5(imyaFaila, kolvo);

        List<Igrushka> spisok;
        XmlSerializer serializer = new XmlSerializer(typeof(List<Igrushka>));
        using (FileStream file = new FileStream(imyaFaila, FileMode.Open))
        {
            spisok = (List<Igrushka>)serializer.Deserialize(file);
        }

        Console.WriteLine("Игрушки для возроста от 4 до 5:");
        foreach (var igr in spisok)
        {
            if (igr.vozrastOt <= 4 && igr.vozrastDo >= 5)
            {
                Console.WriteLine(igr.nazvanie);
            }
        }
    }

    private static void SozdatIgrushkiFail5(string imya, int kolvo)
    {
        List<Igrushka> spisok = new List<Igrushka>();
        for (int i = 0; i < kolvo; i++)
        {
            Console.WriteLine($"Игрушка #{i + 1}:");
            string nazvanie = Proverka.VvodStroki("Название: ");
            double cena = Proverka.VvodVeshestvennoe("Цена: ");
            int vozrastOt = Proverka.VvodCeloe("Возраст от: ");
            int vozrastDo = Proverka.VvodCeloe("Возраст до: ");

            spisok.Add(new Igrushka
            {
                nazvanie = nazvanie,
                cena = cena,
                vozrastOt = vozrastOt,
                vozrastDo = vozrastDo
            });
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<Igrushka>));
        using (FileStream file = new FileStream(imya, FileMode.Create))
        {
            serializer.Serialize(file, spisok);
        }

        Console.WriteLine("Игрушки записаны в XML файл.");
    }

    // Задание 6
    public static void Zadanie6()
    {
        List<int> L1 = VvodSpiska("Введите элементы списка L1 (Через пробел): ");
        List<int> L2 = VvodSpiska("Введите элементы списка L2 (Через пробел): ");
        List<int> L = new List<int>();

        foreach (int chislo in L1)
        {
            if (!L2.Contains(chislo) && !L.Contains(chislo))
            {
                L.Add(chislo);
            }
        }

        Console.WriteLine("Список L: " + string.Join(" ", L));
    }

    // Задание 7 
    public static void Zadanie7()
    {
        List<int> dannye = VvodSpiska("Введите элементы списка (через пробел): ");
        LinkedList<int> spisok = new LinkedList<int>(dannye);

        int i = Proverka.VvodCeloe("Введите начальную позицию i (s 1): ");
        int j = Proverka.VvodCeloe("Введите конечную позицию j (s 1): ");

        if (i < 1 || j > spisok.Count || i >= j)
        {
            Console.WriteLine("Ошибка. Неверные значения i или j.");
            return;
        }

        List<int> fragment = new List<int>();
        int index = 1;
        foreach (int el in spisok)
        {
            if (index >= i && index <= j)
            {
                fragment.Add(el);
            }
            index++;
        }

        bool simmetrichen = true;
        for (int k = 0; k < fragment.Count / 2; k++)
        {
            if (fragment[k] != fragment[fragment.Count - 1 - k])
            {
                simmetrichen = false;
                break;
            }
        }

        Console.WriteLine(simmetrichen ? "Участок симметричен." : "Участок не симметричен.");
    }

    // Задание 8 
    public static void Zadanie8()
    {
        Console.WriteLine("Введите все названия шоколадок (Через запятую):");
        string[] vseShokoladki = Console.ReadLine().Split(',');
        HashSet<string> vse = new HashSet<string>();
        foreach (string s in vseShokoladki)
            vse.Add(s.Trim());

        int kolvo = Proverka.VvodCeloe("Сколько сладкоежек? ");
        List<HashSet<string>> predpochteniya = new List<HashSet<string>>();

        for (int i = 0; i < kolvo; i++)
        {
            Console.WriteLine($"Введите названия шоколадок для сладкоежки #{i + 1} (Через запятую):");
            string[] vvod = Console.ReadLine().Split(',');
            HashSet<string> mnozhestvo = new HashSet<string>();
            foreach (string s in vvod)
            {
                mnozhestvo.Add(s.Trim());
            }
            predpochteniya.Add(mnozhestvo);
        }

        HashSet<string> vsemNravyatsya = new HashSet<string>(vse);
        foreach (var prefer in predpochteniya)
        {
            vsemNravyatsya.IntersectWith(prefer);
        }

        HashSet<string> nekotorymNravyatsya = new HashSet<string>();
        foreach (var prefer in predpochteniya)
        {
            nekotorymNravyatsya.UnionWith(prefer);
        }
        nekotorymNravyatsya.ExceptWith(vsemNravyatsya);

        HashSet<string> nikomuNeNravyatsya = new HashSet<string>(vse);
        nikomuNeNravyatsya.ExceptWith(vsemNravyatsya);
        nikomuNeNravyatsya.ExceptWith(nekotorymNravyatsya);

        Console.WriteLine("Всем нравится: " + string.Join(", ", vsemNravyatsya));
        Console.WriteLine("Некоторым не нравится: " + string.Join(", ", nekotorymNravyatsya));
        Console.WriteLine("Никому не нравится: " + string.Join(", ", nikomuNeNravyatsya));
    }


    // Задание 9 
    public static void Zadanie9()
    {
        Console.WriteLine("Введите текст:");
        string tekst = Console.ReadLine().ToLower();

        HashSet<char> vseBukvy = new HashSet<char>("абвгдеёжзийклмнопрстуфхцчшщъыьэюя");
        HashSet<char> vstrechayutsya = new HashSet<char>();

        foreach (char bukva in tekst)
        {
            if (vseBukvy.Contains(bukva))
            {
                vstrechayutsya.Add(bukva);
            }
        }

        vseBukvy.ExceptWith(vstrechayutsya);
        Console.WriteLine("Не встречаются: " + string.Join(", ", vseBukvy));
    }

    // Задание 10 
    public static void Zadanie10()
    {
        Console.Write("Введите имя файла: ");
        string imyaFayla = Console.ReadLine();

        if (!File.Exists(imyaFayla))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        string[] stroki = File.ReadAllLines(imyaFayla);
        if (stroki.Length < 3)
        {
            Console.WriteLine("Недостаточно данных в файле.");
            return;
        }

        TimeSpan tekVremya = TimeSpan.Parse(stroki[0]);
        int N = int.Parse(stroki[1]);

        Dictionary<string, TimeSpan> passazhiry = new Dictionary<string, TimeSpan>();

        for (int i = 2; i < stroki.Length && i < N + 2; i++)
        {
            string[] chast = stroki[i].Split(' ');
            string familiya = chast[0];
            TimeSpan vremya = TimeSpan.Parse(chast[1]);
            passazhiry[familiya] = vremya;
        }

        List<KeyValuePair<string, TimeSpan>> rezultaty = new List<KeyValuePair<string, TimeSpan>>();

        foreach (var para in passazhiry)
        {
            TimeSpan raznica = para.Value - tekVremya;
            if (raznica.TotalMinutes > 0 && raznica.TotalMinutes <= 120)
            {
                rezultaty.Add(para);
            }
        }

        rezultaty.Sort((a, b) => a.Value.CompareTo(b.Value));

        Console.WriteLine("Пассажиры, которые должны освободить ячейки:");
        foreach (var para in rezultaty)
        {
            Console.WriteLine(para.Key);
        }
    }

    private static List<int> VvodSpiska(string soobshenie)
    {
        Console.WriteLine(soobshenie);
        string[] vvod = Console.ReadLine().Split(' ');
        List<int> spisok = new List<int>();
        foreach (string s in vvod)
        {
            if (int.TryParse(s, out int chislo))
            {
                spisok.Add(chislo);
            }
        }
        return spisok;
    }
}

