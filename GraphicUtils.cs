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
        public class Renderer
        {
            public Panel InputPanel { get; private set; }
            public Panel MapPanel { get; private set; }
            public Panel MenuPanel  { get; private set; }

            public Renderer()
            {
                MainInterface();
            }

            public void RenderMainInterface()
            {
                Console.Clear();

                DrawPanel(InputPanel);
                DrawPanel(MapPanel);
                DrawPanel(MenuPanel);
            }

            private void MainInterface()
            {
                int consoleWidth = Console.WindowWidth;
                int consoleHeight = Console.WindowHeight;

                int leftWidth = consoleWidth / 2;
                int rightWidth = consoleWidth - leftWidth;

                int sceneHeight = consoleHeight / 2;
                int menuHeight = consoleHeight - sceneHeight;

                InputPanel = new Panel(0, 0, leftWidth, consoleHeight, "Input");
                MapPanel = new Panel(leftWidth, 0, rightWidth, sceneHeight, "Map");
                MenuPanel = new Panel(leftWidth, sceneHeight, rightWidth, menuHeight, "Menu");
            }

            public void DrawBox(int x, int y, int width, int height)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("┌" + new string('─', width - 2) + "┐");

                for (int row = 1; row < height - 1; row++)
                {
                    Console.SetCursorPosition(x, y + row);
                    Console.Write("│" + new string(' ', width - 2) + "│");
                }

                Console.SetCursorPosition(x, y + height - 1);
                Console.Write("└" + new string('─', width - 2) + "┘");
            }

            public void DrawPanel(Panel panel)
            {
                DrawBox(panel.X, panel.Y, panel.Width, panel.Height);

                Console.SetCursorPosition(panel.X + 2, panel.Y);
                Console.Write($" {panel.Title} ");
            }
        }

        public class Panel
        {
            public int X { get; }
            public int Y { get; }
            public int Width { get; }
            public int Height { get; }
            public string Title { get; }

            public Panel(int x, int y, int width, int height, string title)
            {
                X = x;
                Y = y;
                Width = width;
                Height = height;
                Title = title;
            }
        }

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
