using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class GameTimer
    {
        private static readonly Stopwatch stopwatch = new Stopwatch();

        public static void Start()
        {
            stopwatch.Restart();
        }

        public static void Stop()
        {
            stopwatch.Stop();
        }
    }
}
