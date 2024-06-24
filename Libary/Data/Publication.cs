using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libary.Data;

public partial class Publication
{
    public int Id { get; set; }

    public int GenreId { get; set; }

    public int EpochId { get; set; }
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "Поле не може містити числа")]
    public string? BookName { get; set; }
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "Поле не може містити числа")]
    public string? Annotation { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Кількість сторінок повинна бути не менше 0")]
    public int? PageCout { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Ціна повинна бути не менше 0")]
    public int? Price { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Рік має бути більше 0")]
    public int? Year { get; set; }

    public virtual Epoch? Epoch { get; set; } = null!;

    public virtual Genre? Genre { get; set; } = null!;

    public virtual ICollection<LibaryCheck> LibaryChecks { get; set; } = new List<LibaryCheck>();

    public virtual ICollection<PublicationAutor> PublicationAutors { get; set; } = new List<PublicationAutor>();
}
