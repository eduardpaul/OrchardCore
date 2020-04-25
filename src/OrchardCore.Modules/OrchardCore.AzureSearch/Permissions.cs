using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Security.Permissions;

namespace OrchardCore.AzureSearch
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageIndexes = new Permission("ManageIndexes", "Manage Indexes");
        public static readonly Permission QueryAzureSearchApi = new Permission("QueryAzureSearchApi", "Query AzureSearch Api", new[] { ManageIndexes });
        public static readonly Permission QueryAzureSearchSearch = new Permission("QueryAzureSearchSearch", "Query AzureSearch Search", new[] { ManageIndexes });

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageIndexes,
                QueryAzureSearchApi,
                QueryAzureSearchSearch
            }
            .AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageIndexes }
                },
                new PermissionStereotype
                {
                    Name = "Editor",
                    Permissions = new[] { QueryAzureSearchApi }
                }
            };
        }
    }
}
