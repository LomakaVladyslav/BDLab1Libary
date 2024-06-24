using System;
using System.Collections.Generic;

namespace Libary.Data;

public partial class Delivery
{
    public int Id { get; set; }

    public int? PostOfficeNumber { get; set; }

    public string? PostOfficeAdress { get; set; }

    public int? ReaderId { get; set; }

    public virtual ICollection<LibaryCheck> LibaryChecks { get; set; } = new List<LibaryCheck>();

    public virtual Reader? Reader { get; set; }
}
