using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task Post_CheckServiceCalled_ReturnsTrue()
        {
            // Arrange
            var mockContactUsService = new Mock<IContactUsService>();
            mockContactUsService.Setup(service => service.AddAsync(It.IsAny<ContactUsModel>())).ReturnsAsync(await Task.FromResult(1));

            ContactUsModel contactUsModel = new ContactUsModel();
            var controller = new ContactUsController(mockContactUsService.Object);
            //Act
            await controller.Post(contactUsModel);
            mockContactUsService.Verify(x => x.AddAsync(It.IsAny<ContactUsModel>()), Times.Once);
        }

        [Theory]
        [InlineData("a.b@abc.com", "", "")]
        [InlineData("a.b@abc1.com", "ibrahim", "hi there")]
        [InlineData("a.b@abc2.com", "ahmet kural", "")]
        public async Task Post_ValidModel_ReturnsOkResultAsync(string email, string name, string message)
        {
            // Arrange
            var mockContactUsService = new Mock<IContactUsService>();
            mockContactUsService.Setup(service => service.AddAsync(It.IsAny<ContactUsModel>())).ReturnsAsync(await Task.FromResult(1));

            ContactUsModel contactUsModel = new ContactUsModel() { Email = email, Name = name, Message = message };
            var controller = new ContactUsController(mockContactUsService.Object);
            //Act
            var result = await controller.Post(contactUsModel);

            //Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
