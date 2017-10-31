using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Abstracts;
using Bulgarian_Apparel.Data.Models.Contracts;
using Bulgarian_Apparel.Data.Models.Enumerations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Data
{
    public class MsSqlDbContext : IdentityDbContext<User>
    {
        
        public MsSqlDbContext()
            : base("LocalConnection", throwIfV1Schema: false)
        {

        }
      

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Post> Posts { get; set; }
        

        public override int SaveChanges()
        {
            ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        public static MsSqlDbContext Create()
        {
            return new MsSqlDbContext();
        }
    }
}
