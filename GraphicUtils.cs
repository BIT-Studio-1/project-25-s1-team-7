using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class GraphicUtils
    {
        /// <summary>
        /// Writes the contents of a text file to the console.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="delay"></param>
        /// <param name="renderMultiple"></param>
        public static void RenderFrame(string filePath, [Optional] int delay, [Optional] bool renderMultiple)
        {
            Console.CursorVisible = false;

            try
            {
                Console.SetCursorPosition(0, 0);

                foreach (string line in File.ReadLines(filePath))
                {
                    string formattedLine = line.Replace("[ANSI]", "\x1b")
                                               .Replace("[/]", "\u001b[0m");

                    Console.WriteLine(formattedLine);
                }

                if (delay > 0)
                {
                    Thread.Sleep(delay);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (!renderMultiple)
                {
                    Console.CursorVisible = true;
                }
            }
        }

        /// <summary>
        /// Writes the contents of a series of text files to the console.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="delay"></param>
        public static void RenderFrames(string folderPath, int delay)
        {
            try
            {
                string[] frames = Directory.GetFiles(folderPath, "*_frame.txt");
                Array.Sort(frames);

                foreach (string frame in frames)
                {
                    RenderFrame(frame, delay, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.CursorVisible = true;
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
    }
}
