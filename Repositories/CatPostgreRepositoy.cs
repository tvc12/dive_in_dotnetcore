using System.Collections.Generic;
using System.Linq;
using CatBasicExample.Domain;
using Microsoft.EntityFrameworkCore;

namespace CatBasicExample.Repositories
{
    public class CatPostgreRepository : ICatRepository
    {
        private CatContext context;

        private DbSet<Cat> cats;

        public CatPostgreRepository(CatContext context)
        {
            this.context = context;
            this.cats = context.Cat;
        }

        public Cat Add(Cat cat)
        {
            Cat newCat = cats.Add(cat).Entity;
            context.SaveChanges();
            return newCat;
        }

        public List<Cat> Cats()
        {
            return cats.ToList();
        }

        public bool Delete(string id)
        {
            Cat cat = cats.First((cat) => cat.Id.Equals(id) == true);
            cats.Remove(cat);
            return context.SaveChanges() > 0;
        }

        public Cat Update(string id, Cat newCat)
        {
            cats.Update(newCat);
            context.SaveChanges();
            return newCat;
        }
    }
}