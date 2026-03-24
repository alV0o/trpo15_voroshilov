using System;
using System.Collections.Generic;

namespace trpo15_voroshilov.Models;

public partial class Tag
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
