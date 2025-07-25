using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

namespace Entities.Models;
public class Destination
{
    [Column("DestinationId")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    public int LocationNumber { get; set; }

    public DestinationType DestinationType { get; set; }

    public ICollection<Job>? Jobs { get; set; }
}
