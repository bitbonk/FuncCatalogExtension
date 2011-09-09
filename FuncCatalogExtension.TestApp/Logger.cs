namespace FuncCatalogExtension.TestApp
{
    using System;

    internal class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
