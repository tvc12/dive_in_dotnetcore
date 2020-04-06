using System.Collections.Generic;
using CatBasicExample.Domain;
using CatBasicExample.Repositories;

namespace CatBasicExample.Services
{
    public interface ICatService
    {
        public List<Cat> Cats();

        public Cat Add(Cat cat);

        public Cat Update(string id, Cat newCat);

        public bool Delete(string id);
    }

    public class CatService : ICatService
    {
        private ICatRepository repository;

        public CatService(ICatRepository repository)
        {
            this.repository = repository;
        }
        public Cat Add(Cat cat)
        {
            return repository.Add(cat);
        }

        public List<Cat> Cats()
        {
            return repository.Cats();
        }

        public bool Delete(string id)
        {
            return repository.Delete(id);
        }

        public Cat Update(string id, Cat newCat)
        {
            return repository.Update(id, newCat);
        }
    }
}