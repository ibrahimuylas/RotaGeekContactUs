using System;
using System.Threading.Tasks;
using Moq;
using RGContactUs.API.Controllers;
using RGContactUs.Core.Service;
using RGContactUs.Domain.Models;
using Xunit;

namespace RGContactUs.Tests
{
    public class ContactUsControllerTest
    {
        [Fact]
        public void TestFirstContactUs()
        {
            // Arrange
            var mockRepo = new Mock<IContactUsService>();
            mockRepo.Setup(repo => repo.AddAsync(GetFirstBook()));

            var controller = new ContactUsController(mockRepo.Object);

                 //Act
            var result = controller.Get(1);

            //Assert
            var viewResult = Assert.IsType<ContactUsModel>(result);
            var model = Assert.IsAssignableFrom<ContactUsModel>(viewResult);
            Assert.Equal("Ibrahim Uylas", model.Name);

        }

        private ContactUsModel GetFirstBook()
        {
            return new ContactUsModel() { Name = "Ibrahim Uylas 1", Email = "ibrahim@uylas.net", Message = "Hi there" };
        }
    }
}
