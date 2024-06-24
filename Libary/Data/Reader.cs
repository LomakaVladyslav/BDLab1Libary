using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libary.Data;

public partial class Reader
{
    public int Id { get; set; }

    public string? NickName { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "Поле не може містити числа")]
    public string? FirstName { get; set; }
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "Поле не може містити числа")]
    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}
