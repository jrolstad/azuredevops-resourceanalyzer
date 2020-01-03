namespace azuredevopsresourceanalyzer.core.Models
{
    public class Branch
    {
        public string Name { get; set; }
        public int CommitsAhead { get; set; }
        public int CommitsBehind { get; set; }
        public string Url { get; set; }
    }
}