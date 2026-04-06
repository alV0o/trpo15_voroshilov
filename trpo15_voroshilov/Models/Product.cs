using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace trpo15_voroshilov.Models;

public partial class Product : ObservableObject
{
    private int _id;
    public int Id { get => _id; set => SetProperty(ref _id, value); }

    private string _name = null!;
    public string Name { get=>_name; set=>SetProperty(ref _name, value); }

    private string _description = null!;
    public string Description { get => _description; set=>SetProperty(ref _description, value); }
    private decimal _price = 0;
    public decimal Price { get=>_price; set => SetProperty(ref _price, value); }
    private int _stock = 0;
    public int Stock { get => _stock; set=>SetProperty(ref _stock, value); }
    private double _rating = 0.0;
    public double Rating { get=>_rating; set=>SetProperty(ref _rating, value); }
    private DateOnly _createdAt;
    public DateOnly CreatedAt { get=>_createdAt; set => SetProperty(ref _createdAt, value); }

    private int _categoryId;
    public int CategoryId { get => _categoryId; set=>SetProperty(ref _categoryId, value); }
    private int _brandId;
    public int BrandId { get=>_brandId; set=>SetProperty(ref _brandId, value); }
    private Brand _brand = null!;
    public virtual Brand Brand
    { 
        get=>_brand;
        set 
        {
            SetProperty(ref _brand, value);
            if (value != null) BrandId = value.Id;
        }
    }
    private Category _category = null!;
    public virtual Category Category 
    { 
        get=>_category;
        set
        {
            SetProperty(ref _category, value);
            if (value != null)CategoryId = value.Id;
        }
    }
    private ICollection<Tag> _tags = new ObservableCollection<Tag>();
    public virtual ICollection<Tag> Tags { get =>_tags; set=>SetProperty(ref _tags, value); }
}
