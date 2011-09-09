namespace FuncCatalogExtension
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;

    internal class FuncPart<TContract> : ComposablePart
    {
        private readonly FuncPartDefinition<TContract> definition;
        private ExportProvider provider;

        public FuncPart(FuncPartDefinition<TContract> definition)
        {
            this.definition = definition;
        }

        public override IEnumerable<ExportDefinition> ExportDefinitions
        {
            get
            {
                return this.definition.ExportDefinitions;
            }
        }

        public override IEnumerable<ImportDefinition> ImportDefinitions
        {
            get
            {
                return this.definition.ImportDefinitions;
            }
        }

        public override object GetExportedValue(ExportDefinition definition)
        {
            var funcExportDefinition = definition as FuncExportDefinition;
            return funcExportDefinition.Factory(this.provider);
        }

        public override void SetImport(ImportDefinition definition, IEnumerable<Export> exports)
        {
            this.provider = exports.First().Value as ExportProvider;
        }
    }
}