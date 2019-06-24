using AutoMapper;
using Moq;
using RGContactUs.Core.Repository;
using RGContactUs.Domain.Entities;
using RGContactUs.Domain.Models;
using RGContactUs.Service;
using RGContactUs.Toolkit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RGContactUs.Tests
{
    public class ServiceTest
    {
        [Theory]
        [InlineData(null, null, null)]
        [InlineData(null, "Ahmet Kural", null)]
        [InlineData("", "Ahmet Kural", null)]
        [InlineData(null, "Ahmet Kural", "Comment")]
        [InlineData("s", "Ahmet Kural", "Comment")]
        [InlineData("a.b@abc.com", null, null)]
        [InlineData("a.b@abc.com", "", null)]
        [InlineData("a.b@abc.com", null, "")]
        [InlineData("a.b@abc.com", "", "")]
        public async void AddAsync_InvalidData_ThrowsInvalidDataException(string email, string name, string message)
        {
            var mapper = new Mock<IMapper>();
            var repository = new Mock<IContactUsRepository>();

            ContactUsService contactUsService = new ContactUsService(mapper.Object, repository.Object);
            ContactUsModel contactUsModel = new ContactUsModel() { Email = email, Name = name, Message = message };

            await Assert.ThrowsAsync<InvalidDataException>(async () => { await contactUsService.AddAsync(contactUsModel); });
        }

        [Theory]
        [InlineData("abc.xyx@hotmail.com", "Ahmet Kural", "a valid comment")]
        public async void AddAsync_CheckRepositoryCalled_ReturnTrue(string email, string name, string message)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ContactUsProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var repository = new Mock<IContactUsRepository>();

            ContactUsModel contactUsModel = new ContactUsModel() { Email = email, Name = name, Message = message };
            ContactUsService contactUsService = new ContactUsService(mapper, repository.Object);

            await contactUsService.AddAsync(contactUsModel);

            repository.Verify(x => x.AddAsync(It.IsAny<ContactUs>()), Times.Once);
        }
    }
}
