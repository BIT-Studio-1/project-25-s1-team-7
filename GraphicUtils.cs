using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class GraphicUtils
    {
        public class Renderer
        {
            /* Create a regex to match the ANSI escape codes so we can
             * detect and strip them from lines when calculating the 
             * length of each line for centering. */

            private static readonly Regex AnsiRegex = new(@"\x1B\[[0-9;]*m", RegexOptions.Compiled);

            /* If you have not heard of regular expressions before and want to
             * know how the above code works, check out the .NET documention:
             *
             * https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expressions
             *
             * Good luck and have fun... it's like reading hieroglyphics. */

            /// <summary>
            /// Render a single Frame.
            /// </summary>
            /// <param name="filePath"></param>
            /// <param name="delay"></param>
            public static void Render(string filePath, int delay = 0)
            {
                return;
            }

            /// <summary>
            /// Render a series of Frames.
            /// </summary>
            /// <param name="folderPath"></param>
            /// <param name="delay"></param>
            /// <param name="loop"></param>
            public static void Render(string folderPath, int delay, bool loop)
            {
                return;
            }
        }

        /// <summary>
        /// Writes the contents of a string one char at a time with a delay.
        /// </summary>
        /// <param name="stringInput"></param>
        /// <param name="delay"></param>
        /// <param name="cursorVisible"></param>
        public static void Teleprinter(string stringInput, int delay, [Optional] bool cursorVisible)
        {
            if (cursorVisible == false)
            {
                Console.CursorVisible = false;
            }

            foreach (string line in stringInput.Split('\n'))
            {
                // Ignore comments
                if (line.StartsWith("//"))
                {
                    continue;
                }

                // Print each character with a delay
                foreach (char c in line)
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }

                // Print a newline after each line
                Thread.Sleep(delay);
                Console.Write("\n");
            }

            Console.CursorVisible = true;
        }

        /// <summary>
        /// Writes the contents of a text file one char at a time with a delay.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="delay"></param>
        /// <param name="cursorVisible"></param>
        public static void TeleprinterFromFile(string filePath, int delay, [Optional] bool cursorVisible)
        {
            if (cursorVisible == false)
            {
                Console.CursorVisible = false;
            }

            try
            {
                using StreamReader reader = new(filePath);
                string textInput = reader.ReadToEnd();

                foreach (string line in textInput.Split('\n'))
                {
                    // Ignore comments
                    if (line.StartsWith("//"))
                    {
                        continue;
                    }

                    // Print each character with a delay
                    foreach (char c in line)
                    {
                        Console.Write(c);
                        Thread.Sleep(delay);
                    }

                    // Print a newline after each line
                    Thread.Sleep(delay);
                    Console.Write("\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }

            Console.CursorVisible = true;
        }

        /* ANSI HELPERS */

        public static void CursorVisible(bool visible)
        {
            if (visible)
            {
                Console.Write($"\u001b[?25h");
            }
            else
            {
                Console.Write($"\u001b[?25l");
            }
        }

        public static void ClearScreen(bool clearScrollBackBuffer = false)
        {
            if (clearScrollBackBuffer)
            {
                Console.Write($"\u001b[3J" + "\u001b[2J");
            }
            else
            {
                Console.Write($"\u001b[2J");
            }
        }

        public static void SetForegroundColor(int r, int g, int b)
        {
            Console.Write($"\u001b[38;2;{r};{g};{b}m");
        }

        public static void SetBackgroundColor(int r, int g, int b)
        {
            Console.Write($"\u001b[48;2;{r};{g};{b}m");
        }

        public static void SetBlinky(bool modeRapid = false)
        {
            if (modeRapid)
            {
                Console.Write("\u001b[6m");
            }
            else
            {
                Console.Write("\u001b[5m");
            }
        }

        public static void InvertColor()
        {
            Console.Write("\u001b[7m");
        }

        public static void ResetGraphics()
        {
            Console.Write($"\u001b[0m");
        }
    }
}
