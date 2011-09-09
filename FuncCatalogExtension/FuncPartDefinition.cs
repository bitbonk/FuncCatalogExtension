namespace FuncCatalogExtension
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;

    internal class FuncPartDefinition<TContract> : FuncPartDefinitionBase
    {
        private readonly List<ExportDefinition> exportDefinitions = new List<ExportDefinition>();
        private readonly List<ImportDefinition> importDefinitions = new List<ImportDefinition>();
        private static readonly ContractBasedImportDefinition exportProviderImportDefinition;

        static FuncPartDefinition()
        {
            string importContractName = typeof(ExportProvider).ToString();
            exportProviderImportDefinition = new ContractBasedImportDefinition(
                importContractName,
                AttributedModelServices.GetTypeIdentity(typeof(ExportProvider)),
                null,
                ImportCardinality.ZeroOrOne,
                false,
                false,
                CreationPolicy.Any);
        }

        public FuncPartDefinition(Func<ExportProvider, object> factory)
        {

            this.exportDefinitions.Add(
                new FuncExportDefinition(typeof(TContract), factory));
            this.importDefinitions.Add(exportProviderImportDefinition);
        }

        public FuncPartDefinition()
            : this(ep => (TContract)Activator.CreateInstance(typeof(TContract)))
        {
        }

        public override IEnumerable<ExportDefinition> ExportDefinitions
        {
            get
            {
                return this.exportDefinitions;
            }
        }

        public override IEnumerable<ImportDefinition> ImportDefinitions
        {
            get
            {
                return this.importDefinitions;
            }
        }

        public override ComposablePart CreatePart()
        {
            return new FuncPart<TContract>(this);
        }
    }
}