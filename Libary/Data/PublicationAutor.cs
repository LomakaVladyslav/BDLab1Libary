using System;
using System.Collections.Generic;

namespace Libary.Data;

public partial class PublicationAutor
{
    public int Id { get; set; }

    public int PublicationId { get; set; }

    public int AutorId { get; set; }

    public virtual Autor? Autor { get; set; }

    public virtual Publication? Publication { get; set; }
}
