using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities;
public partial class Order
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; }
}
