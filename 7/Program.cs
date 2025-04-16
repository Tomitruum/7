using static Zadaniya;
class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Выведите номер задания от 1 до 10: ");
        if (int.TryParse(Console.ReadLine(), out var taskNumber))
        {
            Console.WriteLine();
            switch (taskNumber)
            {
                case 1:
                    Zadanie1();
                    break;
                case 2:
                    Zadanie2();
                    break;
                case 3:
                    Zadanie3();
                    break;
                case 4:
                    Zadanie4();
                    break;
                case 5:
                    Zadanie5();
                    break;
                case 6:
                    Zadanie6();
                    break;
                case 7:
                    Zadanie7();
                    break;
                case 8:
                    Zadanie8();
                    break;
                case 9:
                    Zadanie9();
                    break;
                case 10:
                    Zadanie10();
                    break;
                default:
                    Console.WriteLine("Ошибка ввода. Завершение работы.");
                    break;
            }
        }
        else
            Console.WriteLine("Ошибка ввода. Завершение работы.");
    }
}
