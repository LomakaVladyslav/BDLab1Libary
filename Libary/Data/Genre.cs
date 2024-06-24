using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libary.Data;

public partial class Genre
{
    public int Id { get; set; }
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "Поле не може містити числа")]
    public string? GenreName { get; set; }

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();
}
