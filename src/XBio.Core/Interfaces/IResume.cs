namespace XBio.Core.Interfaces
{
    public interface IResume : IAuditable
    {
        int Id { get; set; }
        string Name { get; set; }
        string Display { get; set; }
        string Description { get; set; }
        int PersonId { get; set; }
        int? SummaryId { get; set; }
        int? ObjectiveId { get; set; }
        bool IsActive { get; set; }
    }
}