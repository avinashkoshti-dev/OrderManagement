using System;
using System.Collections.Generic;

namespace OrderAPI.Models;

public partial class OrderHeader
{
    public int Id { get; set; }

    public string OrderNo { get; set; } = null!;

    public string OrderDate { get; set; } = null!;

    public int CustomerId { get; set; }

    public double TotalAmount { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
