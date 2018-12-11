using Agilisium.TalentManager.Model;
using Agilisium.TalentManager.Model.Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Data.Abstract
{
    public abstract class RepositoryBase<T> : IDisposable where T : class
    {
        private TalentManagerDataContext dataContext;

        public DbSet<T> Entities => DataContext.Set<T>();

        protected TalentManagerDataContext DataContext => dataContext ?? (dataContext = new TalentManagerDataContext());

        public void Dispose()
        {
            if (dataContext != null)
            {
                dataContext.Dispose();
            }
        }
    }
}
