using System.Collections.Generic;
using System.Threading.Tasks;
using RGContactUs.Domain.Entities;

namespace RGContactUs.Core.Repository
{
    public interface IContactUsRepository
    {
        Task<IList<ContactUs>> GetAllAsync();
        Task<ContactUs> GetByIdAsync(int id);
        Task AddAsync(ContactUs entity);
        Task UpdateAsync(ContactUs entity);
        Task DeleteAsync(ContactUs entity);
        Task DeleteByIdAsync(int id);
    }
}
