using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libary.Data;

public partial class Region
{
    public int Id { get; set; }
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "Поле не може містити числа")]
    public string? RegionName { get; set; }

    public virtual ICollection<Autor> Autors { get; set; } = new List<Autor>();
}
