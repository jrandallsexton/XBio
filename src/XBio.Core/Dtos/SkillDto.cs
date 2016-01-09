
namespace XBio.Core.Dtos
{
    public class SkillDto
    {
        public string TechnologyType { get; set; }
        public string Technology { get; set; }
        public int FirstUsedYear { get; set; }
        public int LastUsedYear { get; set; }
        public decimal NumYearsUsed { get; set; }
        public decimal Strength { get; set; }
    }
}