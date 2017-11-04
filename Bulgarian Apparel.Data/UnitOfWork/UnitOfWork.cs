namespace Bulgarian_Apparel.Data.SaveContext
{
    using System;
    using System.Data.Entity;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MsSqlDbContext context;

        public UnitOfWork(MsSqlDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int Commit()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
