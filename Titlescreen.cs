using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Titlescreen
    {
    }
}




/* This is example code, feel free to use or get a feel for how structure could be, you don't have to use it, but might be helpful!
using System;
using System.Threading;

public static class TitleScreen
{
    enum GameState
    {
        TitleScreen,
        Playing
    }

    static GameState gameState = GameState.TitleScreen;

    static void Main()
    {
        Console.CursorVisible = false;
Console.WriteLine("HI");

        while (true)
        {
            if (gameState == GameState.TitleScreen)
            {
                ShowTitleScreen();
            }
            else if (gameState == GameState.Playing)
            {
                RunGame();
            }
        }
    }

    public static void ShowTitleScreen()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(@"                                    (                      
                                   )\ )                   
 (                )           (   (()/(              )    
 )\   (    (   ( /(  `  )    ))\   /(_)) (    (     (     
((_)  )\   )\  )(_)) /(/(   /((_) (_))   )\   )\    )\  ' 
| __|((_) ((_)((_)_ ((_)_\ (_))   | _ \ ((_) ((_) _((_))  
| _| (_-</ _| / _` || '_ \)/ -_)  |   // _ \/ _ \| '  \() 
|___|/__/\__| \__,_|| .__/ \___|  |_|_\\___/\___/|_|_|_|  
                    |_|                                   ");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("             || A Text-based Adventure ||");
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("        Press ENTER to start...");

        Console.ResetColor();

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                gameState = GameState.Playing;
                break;
            }
        }

    }

    public static void RunGame()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Game started!");
        Console.ResetColor();

        Console.WriteLine();
        Console.WriteLine("Your game code goes here.");
        Console.WriteLine();
        Console.WriteLine("Press ESC to quit.");

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }
    }
}*/