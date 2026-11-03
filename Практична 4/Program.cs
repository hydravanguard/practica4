using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("ПРАКТИЧНА РОБОТА 4");
        Console.WriteLine("1. Магазин");
        Console.WriteLine("2. Соціальна мережа");

        Console.Write("Оберіть завдання: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Введіть кількість покупців: ");
                int buyersCount = Convert.ToInt32(Console.ReadLine());

                if (buyersCount <= 0)
                {
                    Console.WriteLine("Кількість має бути більшою за 0.");
                    return;
                }

                Store store = new Store();
                store.Simulate(buyersCount);
                break;

            case "2":
                SocialNetwork network = new SocialNetwork();
                network.Run();
                break;

            default:
                Console.WriteLine("Невірний вибір!");
                break;
        }
    }
}