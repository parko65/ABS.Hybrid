using System.ComponentModel.DataAnnotations;
using Enums;

namespace Shared.DataTransferObjects;
public class JobForCreationDto
{
    public int JobNumber { get; set; }
    public double Tonnage { get; set; }
    public JobStatus Status { get; set; }    
    public int DestinationId { get; set; }

    private string _tonnageInput = string.Empty;

    [Required(ErrorMessage = "Tonnage is required")]    
    [RegularExpression(@"^([1-9]|[1-9][0-9]|[1-2][0-9]{2}|3[0-1][0-9]|320)(\.\d{1,2})?$",
                      ErrorMessage = "Tonnage must be between 1 and 320")]
    public string TonnageInput
    {
        get => _tonnageInput;
        set
        {
            _tonnageInput = value;
            if (double.TryParse(value, out var result))
                Tonnage = result;
        }
    }

    private string _jobInput = string.Empty;

    [Required(ErrorMessage = "Job number is required")]
    [RegularExpression(@"^([1-9]|[1-9][0-9]|[1-9][0-9]{2}|[1-9][0-9]{3}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65000)$",
                      ErrorMessage = "Job number must be between 1 and 65000")]
    public string JobInput
    {
        get => _jobInput;
        set
        {
            _jobInput = value;
            if (int.TryParse(value, out var result))
                JobNumber = result;
        }
    }
}