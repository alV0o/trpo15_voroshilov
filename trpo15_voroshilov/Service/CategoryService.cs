using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using trpo15_voroshilov.Models;

namespace trpo15_voroshilov.Service
{
    public class CategoryService
    {
        private readonly Trpo15VoroshilovContext _db = DBService.Instance.Context;

        public ObservableCollection<Category> Categories { get; set; } = new();

        public int Commit() => _db.SaveChanges();
        public void Add(Category category)
        {
            var _category = new Category
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products,
            };
            _db.Add(_category);
            Commit();
            Categories.Add(_category);
        }
        public void GetAll()
        {
            var categories = _db.Categories
                                .Include(c => c.Products)
                                .ToList();
            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

        public CategoryService()
        {
            GetAll();
        }

        public void Remove(Category category)
        {
            _db.Remove(category);
            if (Commit()>0)
                if (Categories.Contains(category))
                    Categories.Remove(category);
        }
    }
}
