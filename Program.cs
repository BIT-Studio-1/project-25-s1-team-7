using static ConsoleApp1.GraphicUtils;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Teleprinter("""
                Hello World!

                This is a demonstration of the Teleprinter method
                that I created a few days earlier and have since implemented
                into the GraphicsUtils class.

                You should be able to print things to the console in a much 
                more interesting way now!
                """, 25, false);
        }
    }
}
