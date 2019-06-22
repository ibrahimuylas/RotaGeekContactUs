using System;
using AutoMapper;
using RGContactUs.Domain.Entities;
using RGContactUs.Domain.Models;

namespace RGContactUs.Service
{
    public class ContactUsProfile : Profile
    {
        public ContactUsProfile()
        {
            CreateMap<ContactUs, ContactUsModel>().ReverseMap();
        }
    }
}
