using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.Json;

using NAudio.Wave;



List<Move> moves;
List<Charater> charaters;
Random rand = new Random();
string title = @"                                                                                                                                              
 _____    _____ _____ _____ _____ _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ _____ _____ ____  _____ _____ _____ _____ _____ 
|  _  |  |_   _|  |  | __  |     |  |  |     |     |   __|   __|  |   __|  _  |     |   __|   __|  |  _  | __  |  |  |    \|  |  |     |_   _|     |     |
|     |    | | |  |  |    -|  |  |  |  |  |  |-   -|__   |   __|  |  |  |     | | | |   __|__   |  |   __|    -|  |  |  |  |  |  |   --| | | |-   -|  |  |
|__|__|    |_| |_____|__|__|__  _|_____|_____|_____|_____|_____|  |_____|__|__|_|_|_|_____|_____|  |__|  |__|__|_____|____/|_____|_____| |_| |_____|_____|                                                                                                                       
";



string filePath = "Invalid.wav"; 


var playbackTask = PlayAudioAsync(filePath);

gameManager();
await playbackTask;

Console.WriteLine("Audio playback finished.");



//Takes care of what the player wants to do
void gameManager()
{
    loadData();
    Console.WriteLine(title);
    Thread.Sleep(3000);
    Console.Clear();
    while (true)
    {
        Console.WriteLine("Time to play");
        Console.WriteLine("A: Player VS Player");
        Console.WriteLine("B: Player VS Bot");
        Console.WriteLine("C: Bot vs Bot");
        Console.WriteLine("D: Quit");

        string resp = Console.ReadLine() ?? "";

        if (resp.ToUpper().Equals("A"))
        {
            Console.Clear();
            playerVSplayer();
        }
        else if (resp.ToUpper().Equals("B"))
        {
            Console.Clear();
            playerVSbot();
        }
        else if (resp.ToUpper().Equals("C"))
        {
            Console.Clear();
            botVsBot();
        }
        else if (resp.ToUpper().Equals("D"))
        {
            return;
        }
        else
        {
            Console.WriteLine("That is not an valid option, Try Again?");
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}

#region Fight

void playerVSplayer()
{
    Charater p1 = playerChooseCharacter();
    Charater p2 = playerChooseCharacter();
    Console.Clear();

    p1.currentHp = p1.hp;
    p2.currentHp = p2.hp;

    int damage;

    while (true)
    {
        damage = p1.getAttakDamage(p1.chooseMove(ConsoleColor.Blue), rand);
        if (p2.currentHp - damage <= 0)
        {
            Console.WriteLine("Player 1 won, " + p1.name);
            break;
        }
        else
        {
            p2.currentHp -= damage;
            Console.WriteLine("Player 2/" + p2.name + " got hit with " + damage + ". Player 2 now have " + p2.currentHp + " hp \n");
        }

        damage = p2.getAttakDamage(p2.chooseMove(ConsoleColor.Red), rand);
        if (p1.currentHp - damage <= 0)
        {
            Console.WriteLine("Player 2 won, " + p2.name);
            break;
        }
        else
        {
            p1.currentHp -= damage;
            Console.WriteLine("Player 1/" + p1.name + " got hit with " + damage + ". Player 1 now have " + p1.currentHp + " hp \n");
        }
    }
    Console.ForegroundColor = ConsoleColor.White;
    Thread.Sleep(5000);
    Console.Clear();
}

void playerVSbot()
{
    Charater p1 = playerChooseCharacter();
    Charater bot = charaters[rand.Next(0, charaters.Count)];

    Console.WriteLine(bot + " is playing " + bot.name);
    Thread.Sleep(3000);

    Console.Clear();

    p1.currentHp = p1.hp;
    bot.currentHp = bot.hp;

    int damage;

    while (true)
    {
        damage = p1.getAttakDamage(p1.chooseMove(ConsoleColor.Blue), rand);
        if (bot.currentHp - damage <= 0)
        {
            Console.WriteLine("Player 1 won, " + p1.name);
            break;
        }
        else
        {
            bot.currentHp -= damage;
            Console.WriteLine("Bot/" + bot.name + " got hit with " + damage + ". Bot now have " + bot.currentHp + " hp \n");
        }

        Console.ForegroundColor = ConsoleColor.Red;
        damage = bot.getAttakDamage(bot.moves[rand.Next(0, moves.Count)], rand);
        if (p1.currentHp - damage <= 0)
        {
            Console.WriteLine("Bot won, " + bot.name);
            break;
        }
        else
        {
            p1.currentHp -= damage;
            Console.WriteLine("Player 1/" + p1.name + " got hit with " + damage + ". Player 1 now have " + p1.currentHp + " hp \n");
        }
    }
    Console.ForegroundColor = ConsoleColor.White;
    Thread.Sleep(5000);
    Console.Clear();
}

void botVsBot()
{
    Charater bot1 = charaters[rand.Next(0, charaters.Count)];
    Charater bot2 = charaters[rand.Next(0, charaters.Count)];

    Console.WriteLine(bot1 + " is playing " + bot1.name);
    Console.WriteLine(bot2 + " is playing " + bot2.name);
    Thread.Sleep(3000);
    Console.Clear();

    bot1.currentHp = bot1.hp;
    bot2.currentHp = bot2.hp;

    int damage;


    while (true)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        damage = bot1.getAttakDamage(bot1.moves[rand.Next(0, moves.Count)], rand);
        if (bot2.currentHp - damage <= 0)
        {
            Console.WriteLine("Bot 1 won, " + bot1.name);
            break;
        }
        else
        {
            bot2.currentHp -= damage;
            Console.WriteLine("Bot 2/" + bot2.name + " got hit with " + damage + ". Bot 2 now have " + bot2.currentHp + " hp \n");
        }

        Thread.Sleep(2000);

        Console.ForegroundColor = ConsoleColor.Red;
        damage = bot2.getAttakDamage(bot2.moves[rand.Next(0, moves.Count)], rand);
        if (bot1.currentHp - damage <= 0)
        {
            Console.WriteLine("Bot 2 won, " + bot2.name);
            break;
        }
        else
        {
            bot1.currentHp -= damage;
            Console.WriteLine("Bot 1/" + bot1.name + " got hit with " + damage + ". Bot 1 now have " + bot1.currentHp + " hp \n");
        }
        Thread.Sleep(4000);
    }
    Console.ForegroundColor = ConsoleColor.White;
    Thread.Sleep(5000);
    Console.Clear();
}

#endregion

#region Misc

Charater playerChooseCharacter()
{
    Console.WriteLine("Choose your Character");
    for (int i = 0; i < charaters.Count; i++)
    {
        Console.WriteLine("    #" + i + ": " + charaters[i].name);
    }
    int charaterIndex = numberGetNumberFromChooise(0, charaters.Count);
    Console.WriteLine("You choose to play as " + charaters[charaterIndex].name + "\n");
    return charaters[charaterIndex];
}
//takes input as an string and converts it to and in between 2 numbers
int numberGetNumberFromChooise(int minChooise, int maxChooise)
{
    while (true)
    {
        Console.WriteLine("Choose a number inbetween " + minChooise + " - " + (maxChooise - 1));

        string resp = Console.ReadLine() ?? "";
        int respValue;

        if (int.TryParse(resp, out respValue))
        {
            if (minChooise <= respValue && maxChooise >= respValue)
            {
                return respValue;
            }
            Console.WriteLine("That is not a value you can choose");
        }
        Console.WriteLine("You cant choose that value");
    }
}

#endregion

#region SaveSystem
void loadData()
{
    if (File.ReadAllText("moves.txt").Length == 0)
    {
        moves = new List<Move>();
    }
    else
    {
        moves = JsonSerializer.Deserialize<List<Move>>(File.ReadAllText("moves.txt"));
    }


    if (File.ReadAllText("characters.txt").Length == 0)
    {
        charaters = new List<Charater>();
    }
    else
    {
        charaters = JsonSerializer.Deserialize<List<Charater>>(File.ReadAllText("characters.txt"));
    }
}

void writeData()
{
    try
    {
        using (StreamWriter writer = new StreamWriter("moves.txt"))
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<List<Move>>(moves, opt);
            writer.Write(strJson);
        }
    }
    catch (Exception exp)
    {
        Console.Write(exp.Message);
    }

    try
    {
        using (StreamWriter writer = new StreamWriter("characters.txt"))
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<List<Charater>>(charaters, opt);
            writer.Write(strJson);
        }
    }
    catch (Exception exp)
    {
        Console.Write(exp.Message);
    }
}
#endregion


static async Task PlayAudioAsync(string filePath)
{
    using (var reader = new WaveFileReader(filePath))
    using (var waveOut = new WaveOutEvent())
    {
        waveOut.Init(reader);
        waveOut.Play();

        while (waveOut.PlaybackState == PlaybackState.Playing)
        {
            await Task.Delay(100);
        }
    }
}

// moves = new List<Move>();
// moves.Add(new Move("Haze Enveloping Lightning Pulse", 50, 50));
// moves.Add(new Move("Photon Holographic Phantasm", 80, 20));
// moves.Add(new Move("Mousequake ", 60, 40));
// moves.Add(new Move("Wall of Resilience ", 20, 80));

// charaters = new List<Charater>();
// charaters.Add(new Charater("TickleMaster", 300, 20, new Move[2]{moves[0], moves[1]}));
// charaters.Add(new Charater("Jellybelly Jiu-Jits", 250, 50, new Move[2]{moves[2], moves[3]}));
// charaters.Add(new Charater("Sausage Slinger", 200, 70, new Move[2]{moves[0], moves[2]}));
// charaters.Add(new Charater("Banana Peel Brawler", 300, 20, new Move[2]{moves[3], moves[1]}));