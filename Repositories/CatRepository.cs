using System;
using System.Collections.Generic;
using System.Linq;

namespace CatBasicExample.Repositories
{
    public interface ICatRepository
    {
        public List<Cat> Cats();

        public Cat Add(Cat cat);

        public Cat Update(string id, Cat newCat);

        public bool Delete(string id);
    }

    public class FakeCatRepository : ICatRepository
    {
        private readonly List<Cat> cats = new List<Cat>();

        private readonly string[] names = new[] {
            "crank", "arm", "crank arm", "regular seat", "regular", "seat", "regular seat",
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
         };

        private readonly int maxCatRandom = 20;

        private readonly int minCatRandom = 2;

        private Random random;

        public FakeCatRepository(Random random)
        {
            this.random = random;
            IEnumerable<Cat> cats = Enumerable.Range(minCatRandom, random.Next(minCatRandom, maxCatRandom))
                                              .Select(item => GenerateCat());
            this.cats.AddRange(cats);
        }

        public Cat Add(Cat cat)
        {
            cat.Id = Guid.NewGuid().ToString();
            cats.Add(cat);
            return cat;
        }

        public List<Cat> Cats() => cats;

        public bool Delete(string id)
        {
            return cats.RemoveAll(cat => String.Equals(id, cat.Id, StringComparison.CurrentCultureIgnoreCase)) > 0;
        }

        public Cat Update(string id, Cat newCat)
        {
            int index = cats.FindIndex(cat => string.Equals(id, cat.Id, StringComparison.CurrentCultureIgnoreCase));
            if (index > -1)
            {
                if (newCat.Id == null) newCat.Id = id;
                cats.RemoveAt(index);
                cats.Insert(index, newCat);
            }
            return newCat;
        }

        private Cat GenerateCat()
        {
            return new Cat
            {
                Id = Guid.NewGuid().ToString(),
                Age = (uint)random.Next(1, 15),
                Name = names[random.Next(names.Length)]
            };
        }


    }
}
