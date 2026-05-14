using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class GraphicUtils
    {
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
    }
}
