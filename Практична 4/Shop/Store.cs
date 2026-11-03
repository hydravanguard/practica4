using System;

class Store
{
    private Product[] products;
    private int profit;
    private Random random;

    public Store()
    {
        profit = 0;
        random = new Random();

        products = new Product[]
        {
            new Product("Хліб", 300, 20),
            new Product("Хоумлендер", 450, 1),
            new Product("Кир сосічка", 120, 10),
            new Product("Шоколад", 120, 25),
            new Product("Кава", 400, 8),
            new Product("Вода", 20, 30),
            new Product("Сік", 100, 12),
            new Product("Яблука", 25, 18),
            new Product("Напій безалкогольний 'за перемогу'", 41, 22),
            new Product("Трежерка Дота 2", 200, 50),
            new Product("Картопля", 15, 40),
            new Product("Морква", 20, 35),
            new Product("Звільнення від армії", 7800, 2),
            new Product("Долар", 46, 100),
        };
    }

    public Buyer CreateBuyer()
    {
        int cardNumber = random.Next(1, 1001);
        int money = random.Next(1000, 10001);

        return new Buyer(cardNumber, money);
    }

    public void BuyProducts(ref Buyer buyer)
    {
        while (buyer.Money > 0)
        {
            int availableCount = 0;

            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Quantity > 0 &&
                    products[i].Price <= buyer.Money)
                {
                    availableCount++;
                }
            }

            if (availableCount == 0)
                break;

            int selected = random.Next(availableCount);
            int index = 0;

            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Quantity > 0 &&
                    products[i].Price <= buyer.Money)
                {
                    if (selected == 0)
                    {
                        index = i;
                        break;
                    }

                    selected--;
                }
            }

            products[index].Quantity--;
            buyer.Money -= products[index].Price;
            buyer.PurchasedCount++;
            profit += products[index].Price;
        }
    }

    public void Simulate(int buyersCount)
    {
        int maxPurchased = -1;
        int bestCardNumber = 0;

        for (int i = 0; i < buyersCount; i++)
        {
            Buyer buyer = CreateBuyer();

            BuyProducts(ref buyer);

            Console.WriteLine(
                $"Картка: {buyer.CardNumber} | " +
                $"Куплено товарів: {buyer.PurchasedCount} | " +
                $"Залишок грошей: {buyer.Money}"
            );

            if (buyer.PurchasedCount > maxPurchased)
            {
                maxPurchased = buyer.PurchasedCount;
                bestCardNumber = buyer.CardNumber;
            }
        }

        Console.WriteLine("\n===== ПІДСУМКИ =====");
        Console.WriteLine($"Прибуток магазину: {profit}");
        Console.WriteLine($"Картка покупця з найбільшою кількістю покупок: {bestCardNumber}");
        Console.WriteLine($"Кількість придбаних товарів: {maxPurchased}");
    }
}