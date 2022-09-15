using GraphQL.Types;
using Arch.Dto;

namespace Arch.Types
{
    public class OrganizationUnitType : ObjectGraphType<OrganizationUnitDto>
    {
        public OrganizationUnitType()
        {
            Name = "OrganizationUnitType";
            
            Field(x => x.Id);
            Field(x => x.Code);
            Field(x => x.DisplayName);
            Field(x => x.TenantId, nullable: true);
        }
    }
}
