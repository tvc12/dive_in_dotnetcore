using System.Collections.Generic;
using CatBasicExample.Domain;

namespace CatBasicExample.Repositories {
    public class CatPostgreRepository : ICatRepository
    {
        public Cat Add(Cat cat)
        {
            throw new System.NotImplementedException();
        }

        public List<Cat> Cats()
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public Cat Update(string id, Cat newCat)
        {
            throw new System.NotImplementedException();
        }
    }
}