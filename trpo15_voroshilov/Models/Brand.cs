using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace trpo15_voroshilov.Models;

public partial class Brand :ObservableObject
{
    private int _id = 0;
    public int Id { get => _id; set => SetProperty(ref _id, value); }

    private string _name = null!;
    public string Name { get => _name; set => SetProperty(ref _name, value); }

    public virtual ICollection<Product> Products { get; set; } = new ObservableCollection<Product>();
}
