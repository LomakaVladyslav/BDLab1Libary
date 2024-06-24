using System;
using System.Collections.Generic;

namespace Libary.Data;

public partial class LibaryCheck
{
    public int Id { get; set; }

    public int PublicationId { get; set; }

    public int DeliveryId { get; set; }

    public DateTime? DateTime { get; set; }

    public virtual Delivery Delivery { get; set; } = null!;

    public virtual Publication Publication { get; set; } = null!;
}
