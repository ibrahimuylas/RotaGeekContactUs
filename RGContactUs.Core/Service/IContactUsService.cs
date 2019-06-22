using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RGContactUs.Domain.Models;

namespace RGContactUs.Core.Service
{
    public interface IContactUsService
    {
        Task<int> AddAsync(ContactUsModel model);

        Task<IList<ContactUsModel>> GetAllAsync();

        Task<ContactUsModel> GetByIdAsync(int Id);

    }
}
