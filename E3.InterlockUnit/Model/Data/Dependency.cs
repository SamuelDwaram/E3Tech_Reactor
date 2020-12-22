namespace E3.InterlockUnit.Model.Data
{
    public class Dependency
    {
        public DependencyType Type { get; set; }

        public Property PropertyInfo { get; set; }
    }

    public enum DependencyType
    {
        And,
        Or
    }
}
