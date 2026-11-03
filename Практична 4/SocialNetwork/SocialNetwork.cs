using System;
using System.Collections.Generic;

class SocialNetwork
{
    private List<User> users;

    public SocialNetwork()
    {
        users = new List<User>();

        User user1 = new User("Біллі Бутчер", "billybutcher@gmail.com", "@billybutcher");
        User user2 = new User("Дядюшка АУ", "dyadkoau@gmail.com", "@audyadko");
        User user3 = new User("Роберт Полсон", "robertpolson@gmail.com", "@bigtitsrobert");

        user1.Posts.Add(new Post("Супери - гімно"));
        user1.Posts.Add(new Post("Дав Хьюї препарат В)))"));

        user2.Posts.Add(new Post("Дереву рибний суп!"));
        user2.Posts.Add(new Post("Я, зі своєю новою подругою Ріммою!"));
        user2.Posts.Add(new Post("На відпочинку з Кузею!"));

        user3.Posts.Add(new Post("Його ім'я - Тайлер Дрезден"));

        users.Add(user1);
        users.Add(user2);
        users.Add(user3);
    }

    public void Run()
    {
        Console.WriteLine("\nСОЦІАЛЬНА МЕРЕЖА");

        Console.WriteLine("Оберіть користувача:");

        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {users[i].Nickname} ({users[i].Name})");
        }

        Console.Write("Ваш вибір: ");

        if (!int.TryParse(Console.ReadLine(), out int choice) ||
            choice < 1 || choice > users.Count)
        {
            Console.WriteLine("Невірний вибір!");
            return;
        }

        User currentUser = users[choice - 1];

        bool running = true;

        while (running)
        {
            Console.WriteLine($"\n=== ПРОФІЛЬ {currentUser.Nickname} ===");
            Console.WriteLine("1. Переглянути свої дописи");
            Console.WriteLine("2. Переглянути дописи іншого користувача");
            Console.WriteLine("3. Створити допис");
            Console.WriteLine("4. Поставити лайк");
            Console.WriteLine("5. Написати коментар");
            Console.WriteLine("6. Видалити свій допис");
            Console.WriteLine("0. Вийти");

            Console.Write("Ваш вибір: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ShowPosts(currentUser);
                    break;

                case "2":
                    User otherUser = SelectOtherUser(currentUser);

                    if (otherUser != null)
                        ShowLastFivePosts(otherUser);

                    break;

                case "3":
                    CreatePost(currentUser);
                    break;

                case "4":
                    LikePost(currentUser);
                    break;

                case "5":
                    CommentPost(currentUser);
                    break;

                case "6":
                    DeletePost(currentUser);
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Невірний вибір!");
                    break;
            }
        }
    }

    private User SelectOtherUser(User currentUser)
    {
        Console.WriteLine("\nОберіть користувача:");

        int count = 0;

        for (int i = 0; i < users.Count; i++)
        {
            if (users[i] != currentUser)
            {
                count++;
                Console.WriteLine($"{count}. {users[i].Nickname}");
            }
        }

        if (!int.TryParse(Console.ReadLine(), out int choice) ||
            choice < 1 || choice > count)
        {
            Console.WriteLine("Невірний вибір!");
            return null;
        }

        count = 0;

        for (int i = 0; i < users.Count; i++)
        {
            if (users[i] != currentUser)
            {
                count++;

                if (count == choice)
                    return users[i];
            }
        }

        return null;
    }

    private void ShowPosts(User user)
    {
        Console.WriteLine($"\nДописи {user.Nickname}:");

        if (user.Posts.Count == 0)
        {
            Console.WriteLine("Дописи відсутні.");
            return;
        }

        for (int i = 0; i < user.Posts.Count; i++)
        {
            DisplayPost(user.Posts[i], i + 1);
        }
    }

    private void ShowLastFivePosts(User user)
    {
        Console.WriteLine($"\nОстанні дописи {user.Nickname}:");

        if (user.Posts.Count == 0)
        {
            Console.WriteLine("Дописи відсутні.");
            return;
        }

        int start = Math.Max(0, user.Posts.Count - 5);

        for (int i = user.Posts.Count - 1; i >= start; i--)
        {
            DisplayPost(user.Posts[i], i + 1);
        }
    }

    private void DisplayPost(Post post, int number)
    {
        Console.WriteLine($"\n--- Допис №{number} ---");
        Console.WriteLine($"Дата: {post.CreatedAt}");
        Console.WriteLine($"Текст: {post.Text}");
        Console.WriteLine($"Лайки: {post.Likes}");
        Console.WriteLine("Коментарі:");

        if (post.Comments.Count == 0)
        {
            Console.WriteLine("Коментарів немає.");
        }
        else
        {
            foreach (string comment in post.Comments)
            {
                Console.WriteLine("- " + comment);
            }
        }
    }

    private void CreatePost(User user)
    {
        Console.Write("\nВведіть текст допису: ");
        string text = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Допис не може бути порожнім.");
            return;
        }

        user.Posts.Add(new Post(text));

        Console.WriteLine("Допис створено!");
    }

    private void LikePost(User currentUser)
    {
        User target = SelectOtherUser(currentUser);

        if (target == null || target.Posts.Count == 0)
        {
            Console.WriteLine("У цього користувача немає дописів.");
            return;
        }

        ShowPosts(target);

        Console.Write("Номер допису для лайка: ");

        if (!int.TryParse(Console.ReadLine(), out int number) ||
            number < 1 || number > target.Posts.Count)
        {
            Console.WriteLine("Невірний номер!");
            return;
        }

        target.Posts[number - 1].Likes++;

        Console.WriteLine("Лайк поставлено!");
    }

    private void CommentPost(User currentUser)
    {
        User target = SelectOtherUser(currentUser);

        if (target == null || target.Posts.Count == 0)
        {
            Console.WriteLine("У цього користувача немає дописів.");
            return;
        }

        ShowPosts(target);

        Console.Write("Номер допису для коментаря: ");

        if (!int.TryParse(Console.ReadLine(), out int number) ||
            number < 1 || number > target.Posts.Count)
        {
            Console.WriteLine("Невірний номер!");
            return;
        }

        Console.Write("Введіть коментар: ");
        string comment = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(comment))
        {
            Console.WriteLine("Коментар не може бути порожнім.");
            return;
        }

        target.Posts[number - 1].Comments.Add(
            currentUser.Nickname + ": " + comment
        );

        Console.WriteLine("Коментар додано!");
    }

    private void DeletePost(User user)
    {
        ShowPosts(user);

        if (user.Posts.Count == 0)
            return;

        Console.Write("Номер допису для видалення: ");

        if (!int.TryParse(Console.ReadLine(), out int number) ||
            number < 1 || number > user.Posts.Count)
        {
            Console.WriteLine("Невірний номер!");
            return;
        }

        user.Posts.RemoveAt(number - 1);

        Console.WriteLine("Допис видалено!");
    }
}