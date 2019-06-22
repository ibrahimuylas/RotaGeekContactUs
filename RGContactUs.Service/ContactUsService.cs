using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RGContactUs.Core.Repository;
using RGContactUs.Core.Service;
using RGContactUs.Domain.Entities;
using RGContactUs.Domain.Models;
using RGContactUs.Toolkit;

namespace RGContactUs.Service
{
    public class ContactUsService : ServiceBase, IContactUsService
    {
        private IMapper mapper;
        private IContactUsRepository repository;

        public ContactUsService(IMapper mapper, IContactUsRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<int> AddAsync(ContactUsModel model)
        {
            if (!RGValidator.IsValidEmail(model.Email))
            {
                throw new Exception("Please enter a valid email.");
            }

            ContactUs contactUs = mapper.Map<ContactUsModel, ContactUs>(model);
            await repository.AddAsync(contactUs);
            return contactUs.Id;
        }
        
        public async Task<IList<ContactUsModel>> GetAllAsync()
        {
            return mapper.Map<IList<ContactUs>, IList<ContactUsModel>>(await repository.GetAllAsync());
        }

        public async Task<ContactUsModel> GetByIdAsync(int Id)
        {
            return mapper.Map<ContactUs, ContactUsModel>(await repository.GetByIdAsync(Id));
        }

    }
}
