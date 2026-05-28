using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Titlescreen
    {
        enum GameState
        {
            TitleScreen,
            Playing
        }

        static GameState gameState = GameState.TitleScreen;

        public static void ShowTitleScreen()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"                                                                                         
  *   )    )           (                         (        (                 )  (        
` )  /( ( /(    (      )\     (   (          (   )\ )     )\      )      ( /(  )\   (   
 ( )(_)))\())  ))\   (((_)   ))\  )(   (    ))\ (()/(   (((_)  ( /(  (   )\())((_) ))\  
(_(_())((_)\  /((_)  )\___  /((_)(()\  )\  /((_) ((_))  )\___  )(_)) )\ (_))/  _  /((_) 
|_   _|| |(_)(_))   ((/ __|(_))(  ((_)((_)(_))   _| |  ((/ __|((_)_ ((_)| |_  | |(_))   
  | |  | ' \ / -_)   | (__ | || || '_|(_-</ -_)/ _` |   | (__ / _` |(_-<|  _| | |/ -_)  
  |_|  |_||_|\___|    \___| \_,_||_|  /__/\___|\__,_|    \___|\__,_|/__/ \__| |_|\___|  
                                                                                        ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" || A Text-based Escape Room ||");
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

        static void RunGame()
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

        static void PlayTitleScreen ()
        {
            Console.CursorVisible = false;


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
    }
}