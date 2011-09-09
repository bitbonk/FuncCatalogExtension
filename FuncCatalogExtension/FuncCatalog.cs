namespace FuncCatalogExtension
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;

    public class FuncCatalog : ComposablePartCatalog, INotifyComposablePartCatalogChanged
    {
        private readonly List<ComposablePartDefinition> parts = new List<ComposablePartDefinition>();

        public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed = delegate { };

        public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing = delegate { };

        public override IQueryable<ComposablePartDefinition> Parts
        {
            get
            {
                return this.parts.AsQueryable();
            }
        }

        public FuncPartDefinitionBase AddPart<T>(Func<ExportProvider, object> factory)
        {
            var definition = new FuncPartDefinition<T>(factory);
            this.parts.Add(definition);
            this.Changed(this, new ComposablePartCatalogChangeEventArgs(new[] { definition }, new FuncPartDefinition<T>[] { }, null));
            return definition;
        }

        public void AddParts(params FuncPartDefinitionBase[] parts)
        {
            this.parts.AddRange(parts);
            this.Changed(this, new ComposablePartCatalogChangeEventArgs(parts, new FuncPartDefinitionBase[] { }, null));
        }

        public void RemoveParts(params FuncPartDefinitionBase[] parts)
        {
            this.parts.RemoveAll(parts.Contains);
            this.Changed(this, new ComposablePartCatalogChangeEventArgs(new FuncPartDefinitionBase[] { }, parts, null));
        }
    }
}