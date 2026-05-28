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
        /// <summary>
        /// Handles rendering files to the console.
        /// </summary>
        public static class Renderer
        {
            /* Create a regex to match the ANSI escape codes so we can
             * detect and strip them from lines when calculating the 
             * length of each line for centering. */

            private static readonly Regex AnsiRegex = new(@"\x1B\[[0-9;]*m", RegexOptions.Compiled);

            /* If you have not heard of regular expressions before and want to
             * know how the above code works, check out the .NET documentation:
             *
             * https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expressions
             *
             * Good luck and have fun... it's like reading hieroglyphics. */

            /// <summary>
            /// Render a single Frame.
            /// </summary>
            /// <param name="filePath"></param>
            /// <param name="delay"></param>
            public static void Render(string filePath)
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

                    /* Formulas to calculate the position from which
                     * we will start rendering the frame so that it
                     * is centered in the console window. */

                    int top = (Console.WindowHeight - frameHeight) / 2;
                    int left = (Console.WindowWidth - frameWidth) / 2;

                    if (top < 0 || left < 0) // If the frame is too large, throw an exception.
                    {
                        throw new ArgumentException("Frame is too large for the console window.");
                    }

                    /* I imagine we probably won't need to worry about
                     * this because all our frames should be of the same
                     * size but just in case a frame is too large to
                     * fit in the console window, we can throw an exception. */

                    for (int i = 0; i < formattedLines.Length; i++)
                    {
                        int y = top + i; // Increment the y position for each line.

                        if (y >= Console.WindowHeight) // This shouldn't happen if the frame fits in the console, but just in case.
                            break;

                        Console.SetCursorPosition(left, y);
                        Console.Write(formattedLines[i]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    while (Console.ReadKey().Key != ConsoleKey.Escape)
                    {
                        continue;
                    }

                    /* If there is an error, we need to restore
                    * the cursor and screen here */

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
                    while (Console.ReadKey().Key != ConsoleKey.Escape)
                    {
                        string[] frames = Directory.GetFiles(folderPath, "*_frame.txt");
                        Array.Sort(frames);

                        if (frames.Length == 0)
                        {
                            Console.WriteLine("No frames found.");
                            return;
                        }

                        /* All frames should be of the same size so I'm only going
                         to bother checking the dimensions of the first frame */

                        string[] initialFrameLines = File.ReadAllLines(frames[0])
                            .Select(FormatLine)
                            .ToArray();

                        int frameHeight = initialFrameLines.Length;

                        int frameWidth = initialFrameLines
                            .Select(GetLineLength)
                            .DefaultIfEmpty(0)
                            .Max();

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
                                string[] formattedLines = File.ReadAllLines(frame)
                                    .Select(FormatLine)
                                    .ToArray();

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
                        }
                        while (loop); 
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

            /* Removes ANSI markup from a line and
             * replaces it with ACTUAL escape codes */

            private static string FormatLine(string line)
            {
                return line.Replace("[ESC]", "\x1b")
                           .Replace("[/]", "\x1b[0m");
            }

            /* Strips ANSI from a line and returns the length of the line
             * without the codes. This is used to calculate the width of
             * each line for centering purposes. */

            private static int GetLineLength(string line)
            {
                string withoutAnsi = AnsiRegex.Replace(line, "");
                return withoutAnsi.Length;
            }
        }

        /// <summary>
        /// Handles formatting text and manipulating the cursor in the console.
        /// </summary>
        public static class ConsoleFormatter
        {
            /// <summary>
            /// Set the text foreground color to the given RGB values.
            /// </summary>
            /// <param name="r"></param>
            /// <param name="g"></param>
            /// <param name="b"></param>
            public static void ForegroundColor(int r, int g, int b)
            {
                Console.Write($"\u001b[38;2;{r};{g};{b}m");
            }

            /// <summary>
            /// Set the text background color to the given RGB values.
            /// </summary>
            /// <param name="r"></param>
            /// <param name="g"></param>
            /// <param name="b"></param>
            public static void BackgroundColor(int r, int g, int b)
            {
                Console.Write($"\u001b[48;2;{r};{g};{b}m");
            }

            /// <summary>
            /// Enable bold text.
            /// </summary>
            public static void Bold()
            {
                Console.Write("\x1b[1m");
            }

            /// <summary>
            /// Enable dim text.
            /// </summary>
            public static void Dim()
            {
                Console.Write("\x1b[2m");
            }

            /// <summary>
            /// Enable italic text.
            /// </summary>
            public static void Italic()
            {
                Console.Write("\x1b[3m");
            }

            /// <summary>
            /// Enable underlined text.
            /// </summary>
            public static void Underline()
            {
                Console.Write("\x1b[4m");
            }

            /// <summary>
            /// Enable blinking text.
            /// </summary>
            public static void Blink()
            {
                Console.Write("\x1b[5m");
            }

            /// <summary>
            /// Invert foreground and background colors.
            /// </summary>
            public static void Invert()
            {
                Console.Write("\x1b[7m");
            }

            /// <summary>
            /// Enable strikethrough text.
            /// </summary>
            public static void Strikethrough()
            {
                Console.Write("\x1b[9m");
            }

            /// <summary>
            /// Clear the console and optionally the scroll buffer.
            /// </summary>
            /// <param name="scrollBuffer"></param>
            public static void Clear(bool scrollBuffer = true)
            {
                // Move cursor to top-left and clear visible screen
                Console.Write("\x1b[H\x1b[2J");

                // Clear scrollback buffer
                if (scrollBuffer)
                {
                    Console.Write("\x1b[3J");
                }
            }

            /// <summary>
            /// Restore all graphics back to default.
            /// </summary>
            public static void Restore()
            {
                Console.Write($"\u001b[0m");
            }

            /// <summary>
            /// Toggle the visibility of the cursor.
            /// </summary>
            /// <param name="visible"></param>
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

            // ┌────────────────────┬───────┐
            // │    Cursor Style    │ Value │
            // ├────────────────────┼───────┤
            // │ Default            │     0 │
            // │ Blinking Block     │     1 │
            // │ Steady Block       │     2 │
            // │ Blinking Underline │     3 │
            // │ Steady Underline   │     4 │
            // │ Blinking Bar       │     5 │
            // │ Steady Bar         │     6 │
            // └────────────────────┴───────┘

            /// <summary>
            /// Set the cursor style.
            /// </summary>
            /// <param name="style"></param>
            /// <exception cref="ArgumentException"></exception>
            public static void SetCursor(int style)
            {
                if (style > 6 || style < 0)
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
                // Ignore comments.
                if (line.StartsWith("//"))
                {
                    continue;
                }

                // Print each character with a delay.
                foreach (char c in line)
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }

                // Print a newline after each line.
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
                    // Ignore comments.
                    if (line.StartsWith("//"))
                    {
                        continue;
                    }

                    // Print each character with a delay.
                    foreach (char c in line)
                    {
                        Console.Write(c);
                        Thread.Sleep(delay);
                    }

                    // Print a newline after each line.
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
