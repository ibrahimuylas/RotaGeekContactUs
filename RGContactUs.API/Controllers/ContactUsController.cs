using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RGContactUs.Core.Service;
using RGContactUs.Domain.Models;

namespace RGContactUs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _service;

        public ContactUsController(IContactUsService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<IList<ContactUsModel>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ContactUsModel> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpPost("add")]
        public async Task Post([FromBody] ContactUsModel model)
        {
            await _service.AddAsync(model);
        }

    }
}
