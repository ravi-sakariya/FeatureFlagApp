namespace FeatureFlag.Domain.Entity
{
    public class FeatureFlag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Enabled { get; set; }

        public string TargetUsersCsv { get; set; } = string.Empty;
        public string TargetGroupsCsv { get; set; } = string.Empty;

        public List<string> TargetUsers =>
            string.IsNullOrEmpty(TargetUsersCsv)
            ? new List<string>()
            : new List<string>(TargetUsersCsv.Split(','));

        public List<string> TargetGroups =>
            string.IsNullOrEmpty(TargetGroupsCsv)
            ? new List<string>()
            : new List<string>(TargetGroupsCsv.Split(','));
    }
}
