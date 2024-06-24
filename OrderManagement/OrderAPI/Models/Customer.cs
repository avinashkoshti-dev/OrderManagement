using System;
using System.Collections.Generic;

namespace OrderAPI.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = null!;

    public virtual ICollection<OrderHeader> OrderHeaders { get; set; } = new List<OrderHeader>();
}
