using System;
using System.Linq;

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

class Soldier
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    private int damage;

    public Soldier(string name, int health, int damage)
    {
        Name = name;
        Health = health;
        this.damage = damage;
    }

    public void Attack(Soldier enemy)
    {
        enemy.TakeDamage(damage);
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

    private static Random random = new Random();

    public static void SimulateBattle(string countryOneName, string countryTwoName, int squadOneSize, int squadTwoSize)
    {
        Console.WriteLine("Начинается бой между " + countryOneName + " и " + countryTwoName + "!");
        Console.WriteLine();

        Squad squadOne = new Squad(GenerateSoldiers(squadOneSize));
        Squad squadTwo = new Squad(GenerateSoldiers(squadTwoSize));

        while (squadOne.Soldiers.Length > 0 && squadTwo.Soldiers.Length > 0)
        {
            for (int i = 0; i < squadOne.Soldiers.Length; i++)
            {
                if (squadTwo.Soldiers.Length == 0)
                    break;

                Soldier attacker = squadOne.Soldiers[i];
                Soldier defender = squadTwo.Soldiers[random.Next(0, squadTwo.Soldiers.Length)];

                Console.WriteLine(attacker.Name + " атакует " + defender.Name + "!");
                attacker.Attack(defender);

                if (defender.Health <= 0)
                {
                    Console.WriteLine(defender.Name + SoldierEliminatedMessage);
                    squadTwo.RemoveSoldier(defender);
                }

                Console.WriteLine();
            }

            Squad temp = squadOne;
            squadOne = squadTwo;
            squadTwo = temp;
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

        for (int i = 0; i < squadSize; i++)
        {
            string soldierName = GenerateRandomName();
            int soldierHealth = random.Next(10, 20);
            int soldierDamage = random.Next(2, 5);

            soldiers[i] = new Soldier(soldierName, soldierHealth, soldierDamage);
        }

        return soldiers;
    }

    private static string GenerateRandomName()
    {
        string[] names = { "Александр", "Дмитрий", "Иван", "Максим", "Артем", "Андрей", "Николай", "Сергей", "Михаил", "Алексей" };
        string[] surnames = { "Смирнов", "Иванов", "Кузнецов", "Соколов", "Попов", "Лебедев", "Козлов", "Новиков", "Морозов", "Петров" };

        string name = names[random.Next(names.Length)];
        string surname = surnames[random.Next(surnames.Length)];

        return name + " " + surname;
    }
}
