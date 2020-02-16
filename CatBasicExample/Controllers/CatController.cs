using System.Collections.Generic;
using CatBasicExample.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatBasicExample.Controllers
{
    internal interface IController<T>
    {
        public ActionResult Get();

        public T Add(T data);

        public T Update(string id, T data);

        public bool Delete(string id);
    }

    [ApiController]
    [Route("api/cat")]
    public class CatController : ControllerBase, IController<Cat>
    {
        private ICatRepository<Cat> repository;

        public CatController(ICatRepository<Cat> repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public Cat Add([FromForm] Cat cat)
        {
            return repository.Add(cat);
        }

        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return repository.Delete(id);
        }

        [HttpPost("{id}")]
        public Cat Update(string id, [FromBody] Cat cat)
        {
            return repository.Update(id, cat);
        }

        [HttpGet()]
        public ActionResult Get()
        {
            return Ok(repository.Cats());
        }

    }
}