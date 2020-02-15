using System.Collections.Generic;
using CatBasicExample.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatBasicExample.Controllers
{
    internal interface IController<T>
    {
        public List<T> Get();

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

        public Cat Add(Cat cat)
        {
            return repository.Add(cat);
        }

        public bool Delete(string id)
        {
            return repository.Delete(id);
        }


        public Cat Update(string id, Cat cat)
        {
            return repository.Update(id, cat);
        }

        [HttpGet()]
        public List<Cat> Get()
        {
            return repository.Cats();
        }

    }
}