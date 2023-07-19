using System;
using System.Linq;

class Soldier
{
    public string Name { get; private set; }
    public int Health { get; private set; }

    public Soldier(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void Attack(Soldier enemy)
    {
        enemy.TakeDamage(1);
    }

    private void TakeDamage(int damage)
    {
        Health -= damage;
    }
}

class Squad
{
    public Soldier[] Soldiers { get; private set; }

    public Squad(Soldier[] soldiers)
    {
        Soldiers = soldiers;
    }

    public void RemoveSoldier(Soldier soldier)
    {
        Soldiers = Soldiers.Where(s => s != soldier).ToArray();
    }
}

class BattleSimulator
{
    private const string SoldierEliminatedMessage = " был элиминирован.";
    private const string WinnerMessage = " победил!";
    private const string DrawMessage = "Бой окончился вничью.";

    public static void SimulateBattle(string countryOneName, string countryTwoName, int squadOneSize, int squadTwoSize)
    {
        Console.WriteLine("Начинается бой между " + countryOneName + " и " + countryTwoName + "!");
        Console.WriteLine();

        Soldier[] squadOneSoldiers = GenerateSoldiers(squadOneSize);
        Soldier[] squadTwoSoldiers = GenerateSoldiers(squadTwoSize);

        Squad squadOne = new Squad(squadOneSoldiers);
        Squad squadTwo = new Squad(squadTwoSoldiers);

        while (squadOne.Soldiers.Length > 0 && squadTwo.Soldiers.Length > 0)
        {
            Soldier attacker = squadOne.Soldiers[0];
            Soldier defender = squadTwo.Soldiers[new Random().Next(0, squadTwo.Soldiers.Length)];

            Console.WriteLine(attacker.Name + " атакует " + defender.Name + "!");
            attacker.Attack(defender);

            if (defender.Health <= 0)
            {
                Console.WriteLine(defender.Name + SoldierEliminatedMessage);
                squadTwo.RemoveSoldier(defender);
            }

            Squad temp = squadOne;
            squadOne = squadTwo;
            squadTwo = temp;

            Console.WriteLine();
        }

        if (squadOne.Soldiers.Length > 0)
        {
            Console.WriteLine(countryOneName + WinnerMessage);
        }
        else if (squadTwo.Soldiers.Length > 0)
        {
            Console.WriteLine(countryTwoName + WinnerMessage);
        }
        else
        {
            Console.WriteLine(DrawMessage);
        }
    }

    private static Soldier[] GenerateSoldiers(int squadSize)
    {
        Soldier[] soldiers = new Soldier[squadSize];
        Random random = new Random();

        for (int i = 0; i < squadSize; i++)
        {
            string soldierName = GenerateRandomName();
            int soldierHealth = random.Next(10, 20);

            soldiers[i] = new Soldier(soldierName, soldierHealth);
        }

        return soldiers;
    }

    private static string GenerateRandomName()
    {
        string[] names = { "Александр", "Дмитрий", "Иван", "Максим", "Артем", "Андрей", "Николай", "Сергей", "Михаил", "Алексей" };
        string[] surnames = { "Смирнов", "Иванов", "Кузнецов", "Соколов", "Попов", "Лебедев", "Козлов", "Новиков", "Морозов", "Петров" };

        Random random = new Random();
        string name = names[random.Next(names.Length)];
        string surname = surnames[random.Next(surnames.Length)];

        return name + " " + surname;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите название страны один:");
        string countryOneName = Console.ReadLine();

        Console.WriteLine("Введите количество солдат в взводе страны один:");
        int squadOneSize = int.Parse(Console.ReadLine());

        Console.WriteLine();

        Console.WriteLine("Введите название страны два:");
        string countryTwoName = Console.ReadLine();

        Console.WriteLine("Введите количество солдат в взводе страны два:");
        int squadTwoSize = int.Parse(Console.ReadLine());

        Console.WriteLine();

        BattleSimulator.SimulateBattle(countryOneName, countryTwoName, squadOneSize, squadTwoSize);

        Console.ReadLine();
    }
}
