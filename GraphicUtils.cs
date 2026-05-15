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
        public static void RenderFrame(string filePath)
        {
            try
            {
                using StreamReader reader = new(filePath);
                string fileContent = reader.ReadToEnd();

                Console.WriteLine(fileContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: is your path correct?");
                Console.WriteLine(ex.Message);
            }
        }

        public static void RenderFrames(string folderPath)
        {
            string[] framesArray = Directory.GetFiles(folderPath, "*_frame.txt");
            Array.Sort(framesArray);

            foreach (string frame in framesArray)
            {
                using StreamReader reader = new(frame);
                string fileContent = reader.ReadToEnd();

                Console.WriteLine(fileContent);
                Thread.Sleep(2000);
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
    }
}
