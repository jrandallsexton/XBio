namespace XBio.Core.Interfaces
{
    public interface IAchievement : IAuditable
    {
        int Id { get; set; }
        int PositionId { get; set; }
        string Title { get; set; }
        string Value { get; set; }
        int Order { get; set; }
    }
}