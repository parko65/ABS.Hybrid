using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

namespace Entities.Models;
public class Job
{
    [Column("JobId")]
    public int Id { get; set; }

    [Range(1, 65000, ErrorMessage = "Job number must be between 1 and 999999")]
    public int JobNumber { get; set; }

    public DateTime CreateDate { get; set; } // auto generated in repository

    [Range(0.5, 320.0, ErrorMessage = "Tonnage must be btween 0.5 and 320 tonnes")]
    public double Tonnage { get; set; }
    public JobStatus Status { get; set; }


    [ForeignKey(nameof(Recipe))]
    public int RecipeId { get; set; }
    public Recipe? Recipe { get; set; }

    [ForeignKey(nameof(Destination))]
    public int DestinationId { get; set; }
    public Destination? Destination { get; set; }
}
