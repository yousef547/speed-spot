using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories
{
    public class UserKeysContext : DbContext, IDataProtectionKeyContext
    {
        // A recommended constructor overload when using EF Core 
        // with dependency injection.
        public UserKeysContext(DbContextOptions<UserKeysContext> options)
            : base(options) { }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
}
