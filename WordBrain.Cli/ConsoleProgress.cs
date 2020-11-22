using System;

namespace WordBrain.Cli
{
    internal class ConsoleProgress : IProgress<Solution>
    {
        private int _reportLength;

        public void Report(Solution value)
        {
            if (value == null)
            {
                return;
            }

            string report = value.ToString();
            _reportLength = report.Length;
            Console.Write($"{report}\r");
        }

        public void ClearReport()
        {
            Console.Write($"{new string(' ', _reportLength)}\r");
        }
    }
}
