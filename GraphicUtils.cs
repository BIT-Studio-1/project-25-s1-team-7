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
                ConsoleFormatter.SetCursor(false);
                ConsoleFormatter.Clear(true);

                try
                {
                    string[] lines = File.ReadAllLines(filePath);

                    string[] formattedLines = lines.Select(FormatLine)
                        .ToArray();

                    int frameHeight = formattedLines.Length;

                    int frameWidth = formattedLines
                        .Select(GetLineLength)
                        .DefaultIfEmpty(0)
                        .Max();

                    int top = (Console.WindowHeight - frameHeight) / 2;
                    int left = (Console.WindowWidth - frameWidth) / 2;

                    if (top < 0 || left < 0)
                    {
                        throw new ArgumentException("Frame is too large for the console window.");
                    }

                    for (int i = 0; i < formattedLines.Length; i++)
                    {
                        int y = top + i;

                        if (y >= Console.WindowHeight)
                            break;

                        Console.SetCursorPosition(left, y);
                        Console.Write(formattedLines[i]);
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
                    ConsoleFormatter.SetCursor(true);
                    ConsoleFormatter.Clear(true);
                }
            }

            /// <summary>
            /// Render a series of Frames.
            /// </summary>
            /// <param name="folderPath"></param>
            /// <param name="delay"></param>
            /// <param name="loop"></param>
            public static void Render(string folderPath, int delay, bool loop)
            {
                ConsoleFormatter.SetCursor(false);
                ConsoleFormatter.Clear(true);

                try
                {
                    string[] frames = Directory.GetFiles(folderPath, "*_frame.txt");
                    Array.Sort(frames);

                    if (frames.Length == 0)
                    {
                        Console.WriteLine("No frames found.");
                        return;
                    }

                    string[] initialFrameLines = File.ReadAllLines(frames[0]);

                    for (int i = 0; i < initialFrameLines.Length; i++)
                    {
                        initialFrameLines[i] = FormatLine(initialFrameLines[i]);
                    }

                    int frameHeight = initialFrameLines.Length;
                    int frameWidth = 0;

                    for (int i = 0; i < initialFrameLines.Length; i++)
                    {
                        int visibleLength = GetLineLength(initialFrameLines[i]);

                        if (visibleLength > frameWidth)
                        {
                            frameWidth = visibleLength;
                        }
                    }

                    int top = (Console.WindowHeight - frameHeight) / 2;
                    int left = (Console.WindowWidth - frameWidth) / 2;

                    if (top < 0 || left < 0)
                    {
                        throw new ArgumentException("Frame is too large for the console window.");
                    }

                    do
                    {
                        foreach (string frame in frames)
                        {
                            string[] lines = File.ReadAllLines(frame);

                            for (int i = 0; i < lines.Length; i++)
                            {
                                string formattedLine = FormatLine(lines[i]);

                                int y = top + i;

                                if (y >= Console.WindowHeight)
                                {
                                    break;
                                }

                                Console.SetCursorPosition(left, y);
                                Console.Write(formattedLine);

                                int visibleLength = GetLineLength(formattedLine);
                                int spacesToClear = frameWidth - visibleLength;

                                if (spacesToClear > 0)
                                {
                                    Console.Write(new string(' ', spacesToClear));
                                }
                            }

                            if (delay > 0)
                            {
                                Thread.Sleep(delay);
                            }
                        }
                    }
                    while (loop);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    ConsoleFormatter.SetCursor(true);
                    ConsoleFormatter.Clear(true);
                }
            }

            private static string FormatLine(string line)
            {
                return line.Replace("[ESC]", "\x1b")
                           .Replace("[/]", "\x1b[0m");
            }

            private static int GetLineLength(string line)
            {
                string withoutAnsi = AnsiRegex.Replace(line, "");
                return withoutAnsi.Length;
            }
        }

        public class ConsoleFormatter
        {
            public static void SetForegroundColor(int r, int g, int b)
            {
                Console.Write($"\u001b[38;2;{r};{g};{b}m");
            }

            public static void SetBackgroundColor(int r, int g, int b)
            {
                Console.Write($"\u001b[48;2;{r};{g};{b}m");
            }

            public static void Clear(bool scrollBuffer = false)
            {
                if (scrollBuffer)
                {
                    Console.Write("\u001b[3J");
                }

                Console.Write("\u001b[2J");
                Console.SetCursorPosition(0, 0);
            }

            public static void Restore()
            {
                Console.Write($"\u001b[0m");
            }

            public static void SetCursor(bool visible)
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

            public static void SetCursor(int style)
            {
                if (style > 6)
                {
                    throw new ArgumentException("Invalid cursor style. Please use a value between 0 and 6.");
                }

                Console.Write($"\u001b[{style} q");
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
