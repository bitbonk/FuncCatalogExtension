namespace FuncCatalogExtension.TestApp
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using FuncCatalogExtension;

    internal class Program
    {
        private static CompositionContainer container;

        public static void Main(string[] args)
        {
            Configure();
            var logger = container.GetExportedValue<ILogger>();
            logger.Log("Hello");
            logger.Log("World");
            Console.ReadLine();
        }

        private static void Configure()
        {
            var funcCatalog = new FuncCatalog();
            funcCatalog.AddPart<ILogger>(ep => new ConsoleLogger());
            container = new CompositionContainer(funcCatalog);
            var batch = new CompositionBatch();
            batch.AddExportedValue<ExportProvider>(container);
            container.Compose(batch);
        }
    }
}
