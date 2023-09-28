class Charater
{
    public string name { get; set; }
    public int hp { get; set; }
    public int attack { get; set; }
    public Move[] moves { get; set; }

    public int currentHp;

    public Charater(string name, int hp, int attack, Move[] moves)
    {
        this.name = name;
        this.hp = hp;
        this.attack = attack;
        this.moves = moves;
    }

    //Lets the player choose the move
    public Move ChooseMOve(ConsoleColor color)
    {
        Console.ForegroundColor = color;
        while (true)
        {
            Console.WriteLine("CHoose your move");
            Console.WriteLine("    A: " + moves[0].name);
            Console.WriteLine("    B: " + moves[1].name);

            string resp = Console.ReadLine() ?? "";

            if (resp.ToUpper().Equals("A"))
            {
                return moves[0];
            }
            else if (resp.ToUpper().Equals("B"))
            {
                return moves[1];
            }
            else
            {
                Console.WriteLine("Not a valid option try agian");
            }
        }
    }

    //Gets the damage as an int to later apply to the enemy
    public int GetAttackDamage(Move move, Random rand)
    {
        if (move.accuracy > rand.Next(0, 101))
        {
            Console.WriteLine(name + ": Seems like the attack will hit");
            Thread.Sleep(1000);
            return attack + move.damage + rand.Next(-5, 6);
        }
        Console.WriteLine(name + ": Missed the attack");
        Thread.Sleep(1000);
        return 0;
    }
}