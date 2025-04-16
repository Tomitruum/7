public static class Proverka
{
    public static int VvodCeloe(string soobshenie)
    {
        int chislo;
        Console.Write(soobshenie);
        while (!int.TryParse(Console.ReadLine(), out chislo))
        {
            Console.WriteLine("Ошибка. Введите целое число.");
            Console.Write(soobshenie);
        }
        return chislo;
    }

    public static double VvodVeshestvennoe(string soobshenie)
    {
        double chislo;
        Console.Write(soobshenie);
        while (!double.TryParse(Console.ReadLine(), out chislo))
        {
            Console.WriteLine("Ошибка. Введите целое число.");
            Console.Write(soobshenie);
        }
        return chislo;
    }

    public static string VvodStroki(string soobshenie)
    {
        Console.Write(soobshenie);
        string stroka = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(stroka))
        {
            Console.WriteLine("Ошибка. Строка не должна быть пустой.");
            Console.Write(soobshenie);
            stroka = Console.ReadLine();
        }
        return stroka;
    }
}
