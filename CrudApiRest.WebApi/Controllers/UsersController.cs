using CrudApiRest.Application.Interfaces;
using CrudApiRest.Data.Models;
using CrudApiRest.Shared.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace CrudApiRest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : Controller
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IEnumerable<User> Get(PagingData paging)
        {
            var headers = HttpContext.Request.Headers;
            return _service.List(paging);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public dynamic GetById(int id)
        {
            var result = _service.FindById(id);
            if (result != null)
                return result;
            else
                return NotFound();
        }

        [HttpGet]
        [Route("{id}/GenerateToken")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public dynamic GetToken(int id)
        {
            var result = _service.GetJwtToken(id);
            if (result != null)
                return result;
            else
                return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public dynamic Post([FromBody] User user)
        {
            var result = _service.Insert(user);
            if (result != null)
                return result;
            else
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public dynamic Put(int id, [FromBody] User user)
        {
            var result = _service.Update(id, user);
            if (result != null)
                return result;
            else
                return NotFound();
        }

        [HttpPut]
        [Route("{id}/UpdatePass")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public dynamic PutPass(int id, [FromBody] User user)
        {
            var result = _service.UpdatePassword(id, user);

            if (result > 0)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public dynamic Delete(int id)
        {
            var result = _service.Delete(id);

            if (result > 0)
                return Ok();
            else
                return NotFound();
        }
    }
}
