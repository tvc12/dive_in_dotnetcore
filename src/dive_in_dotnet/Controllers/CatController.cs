using System.Collections.Generic;
using AuthService.Domain;
using CatBasicExample.Domain;
using CatBasicExample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatBasicExample.Controllers
{
    [ApiController]
    [Route("api/cat")]
    public class CatController : ControllerBase
    {
        private ICatService service;

        public CatController(ICatService repository)
        {
            this.service = repository;
        }

        [HttpPost]
        public ActionResult<Cat> Add([FromForm] Cat cat)
        {
            return service.Add(cat);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            return service.Delete(id);
        }

        [HttpPost("{id}")]
        public ActionResult<Cat> Update(string id, [FromBody] Cat cat)
        {
            return service.Update(id, cat);
        }

        [HttpGet()]
        [Authorize]
        public ActionResult<List<Cat>> Get()
        {
            return service.Cats();
        }

    }
}