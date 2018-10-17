using CrudApiRest.Application.Interfaces;
using CrudApiRest.Data.Models;
using CrudApiRest.Shared.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

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
        [ProducesResponseType(typeof(ArgumentOutOfRangeException), (int)HttpStatusCode.NotFound)]
        public IEnumerable<User> Get(PagingData paging)
        {
            return _service.List(paging);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ArgumentOutOfRangeException), (int)HttpStatusCode.NotFound)]
        public User Get(int id)
        {
            return _service.FindById(id);
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ArgumentOutOfRangeException), (int)HttpStatusCode.NotFound)]
        public ActionResult Post([FromBody] User user)
        {
            _service.Insert(user);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ArgumentOutOfRangeException), (int)HttpStatusCode.NotFound)]
        public ActionResult Put(int id, [FromBody] User user)
        {
            _service.Update(id, user);
            return Ok();
        }

        [HttpPut]
        [Route("UpdatePass")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ArgumentOutOfRangeException), (int)HttpStatusCode.NotFound)]
        public ActionResult PutPass([FromBody] User user)
        {
            _service.UpdatePassword(user);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ArgumentOutOfRangeException), (int)HttpStatusCode.NotFound)]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
