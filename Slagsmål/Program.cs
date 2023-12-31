﻿using System.Text.Json;
using NAudio.Wave;

List<Move> moves;
List<Charater> charaters;
Random rand = new Random();
string filePath = "Invalid.wav";

PlayAudioAsync(filePath); 
GameManager();

//Manages the game state
void GameManager()
{
    loadData();
    Console.WriteLine(@"                                                                                                                                              
 _____    _____ _____ _____ _____ _____ _____ _____ _____ _____    _____ _____ _____ _____ _____    _____ _____ _____ ____  _____ _____ _____ _____ _____ 
|  _  |  |_   _|  |  | __  |     |  |  |     |     |   __|   __|  |   __|  _  |     |   __|   __|  |  _  | __  |  |  |    \|  |  |     |_   _|     |     |
|     |    | | |  |  |    -|  |  |  |  |  |  |-   -|__   |   __|  |  |  |     | | | |   __|__   |  |   __|    -|  |  |  |  |  |  |   --| | | |-   -|  |  |
|__|__|    |_| |_____|__|__|__  _|_____|_____|_____|_____|_____|  |_____|__|__|_|_|_|_____|_____|  |__|  |__|__|_____|____/|_____|_____| |_| |_____|_____|                                                                                                                       
");
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
            PlayerVsPlayer();
        }
        else if (resp.ToUpper().Equals("B"))
        {
            Console.Clear();
            PlayerVsBot();
        }
        else if (resp.ToUpper().Equals("C"))
        {
            Console.Clear();
            BotVsBot();
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

//Manages the player vs player game
void PlayerVsPlayer()
{
    //Seting up the game
    Charater p1 = PlayerChooseCharacter();
    Charater p2 = PlayerChooseCharacter();
    Console.Clear();

    p1.currentHp = p1.hp;
    p2.currentHp = p2.hp;

    int damage;

    //While any player isent dead let them choose how to attack
    while (true)
    {
        damage = p1.GetAttackDamage(p1.ChooseMOve(ConsoleColor.Blue), rand);
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

        damage = p2.GetAttackDamage(p2.ChooseMOve(ConsoleColor.Red), rand);
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

//the same as above but with one bot that chooses what to do with one bot that uses random
void PlayerVsBot()
{
    Charater p1 = PlayerChooseCharacter();
    Charater bot = charaters[rand.Next(0, charaters.Count)];

    Console.WriteLine(bot + " is playing " + bot.name);
    Thread.Sleep(3000);

    Console.Clear();

    p1.currentHp = p1.hp;
    bot.currentHp = bot.hp;

    int damage;

    while (true)
    {
        damage = p1.GetAttackDamage(p1.ChooseMOve(ConsoleColor.Blue), rand);
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
        damage = bot.GetAttackDamage(bot.moves[rand.Next(0, bot.moves.Length)], rand);
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

//the same as above but with one bot that chooses what to do with 2 bot that uses random
void BotVsBot()
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
        damage = bot1.GetAttackDamage(bot1.moves[rand.Next(0, bot1.moves.Length)], rand);
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
        damage = bot2.GetAttackDamage(bot2.moves[rand.Next(0, bot2.moves.Length)], rand);
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

//lets the player decide what character to use by using makePlayerSelectValueBetweenValues
Charater PlayerChooseCharacter()
{
    Console.WriteLine("Choose your Character");
    for (int i = 0; i < charaters.Count; i++)
    {
        Console.WriteLine("    #" + i + ": " + charaters[i].name);
    }
    int charaterIndex = MakePlayerSelectValueWithMaxMin(0, charaters.Count);
    Console.WriteLine("You choose to play as " + charaters[charaterIndex].name + "\n");
    return charaters[charaterIndex];
}

//takes input as an string and converts it to and in between 2 numbers
int MakePlayerSelectValueWithMaxMin(int minChooise, int maxChooise)
{
    while (true)
    {
        Console.WriteLine("Choose a number inbetween " + minChooise + " - " + (maxChooise - 1));

        string resp = Console.ReadLine() ?? "";
        int respValue;

        if (int.TryParse(resp, out respValue))//makes sure it can be an int
        {
            if (minChooise <= respValue && maxChooise >= respValue)//makes sure it is between the values
            {
                return respValue;
            }
            Console.WriteLine("That is not a value you can choose");
        }
        Console.WriteLine("You cant choose that value");
    }
}

//using mutlithreading to get teh audio to play at the same time. Method plays audio
async Task PlayAudioAsync(string filePath)
{
    using (var reader = new WaveFileReader(filePath))//Makes a new WaveFileReader with the file so it know what to play
    using (var waveOut = new WaveOutEvent()) //makes the output deivde refernse
    {
        waveOut.Init(reader); //setting up the system
        waveOut.Play(); //Play the audio

        while (waveOut.PlaybackState == PlaybackState.Playing)
        {
            await Task.Delay(100);//lets the code for the main game run at the same time
        }
    }
}

#endregion

#region SaveSystem
//loads the data from the files moves and characters
void loadData()
{
    if (File.ReadAllText("moves.txt").Length == 0)//if there is nothing in the text file make a new list
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

//writing new data if i whould add characters
void writeData()
{
    //writing data to the file
    try
    {
        using (StreamWriter writer = new StreamWriter("moves.txt"))
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true }; //gör allt läsbart
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