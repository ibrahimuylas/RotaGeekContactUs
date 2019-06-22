using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RGContactUs.Core.Repository;
using RGContactUs.Data.EF;
using RGContactUs.Domain.Entities;
using RGContactUs.Toolkit;

namespace RGContactUs.Repository
{
    public class ContactUsRepository : IContactUsRepository
    {
        private ContactUsContext context;
        public ContactUsRepository(ContactUsContext context)
        {
            this.context = context;
        }

        #region IContactUsRepository Member(s)
        public async Task<IList<ContactUs>> GetAllAsync()
        {
            List<ContactUs> query = await context.Set<ContactUs>().Where(x => x.State == EntityStatus.Active).ToListAsync();
            return query;
        }

        public async Task<ContactUs> GetByIdAsync(int id)
        {
            return await context.Set<ContactUs>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(ContactUs entity)
        {
            entity.State = EntityStatus.Active;
            entity.Id = context.Contacts.Count() + 1;
            await context.Set<ContactUs>().AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(ContactUs entity)
        {
            entity.CanceledDate = DateTime.Now;
            entity.State = EntityStatus.Deleted;
            await SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            ContactUs entity = await context.Set<ContactUs>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }

        public async Task UpdateAsync(ContactUs entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            foreach (var item in context.ChangeTracker.Entries<ContactUs>())
            {
                if (item.State == EntityState.Added || item.State == EntityState.Modified)
                {
                    if (item.Entity.State != EntityStatus.Deleted)
                    {
                        item.Entity.ModifiedDate = DateTime.Now;
                    }
                }
            }

            await context.SaveChangesAsync();
        }

        #endregion

        #region IDisposable Member(s)

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {

            if (!this.disposed)
            {

                if (disposing)
                {
                    context.Dispose();
                }

            }

            this.disposed = true;

        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion


    }
}
