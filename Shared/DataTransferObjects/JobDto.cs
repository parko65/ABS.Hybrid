using Enums;

namespace Shared.DataTransferObjects;
public record JobDto(int Id, int JobNumber, double Tonnage, DateTime CreateDate, JobStatus Status, RecipeDto? Recipe, DestinationDto Destination);