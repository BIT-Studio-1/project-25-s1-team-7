using static ConsoleApp1.GraphicUtils;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Assets Path
            string PathAssets = @"..\..\..\assets\";

            /* Do not change this path, it should
               work on any machine. */

            RenderFrame(PathAssets + "TestFile.txt");
        }
    }
}
