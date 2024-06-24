using System;
using System.Collections.Generic;

namespace OrderAPI.Models;

public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderHeaderId { get; set; }

    public int ProductId { get; set; }

    public double Rate { get; set; }

    public int Qty { get; set; }

    public int Gst { get; set; }

    public double Total { get; set; }

    public double LineTotal { get; set; }

    public virtual OrderHeader OrderHeader { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
