using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using Arch.Queries.Container;
using System;

namespace Arch.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}