using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class WebApiTeam
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
    }

    public class TeamFieldValues
    {
        public FieldReference field { get; set; }
        public string defaultValue { get; set; }
        public string url { get; set; }
        public List<TeamFieldValue> values { get; set; }
        public Dictionary<string,Link> _links { get; set; }
        public string Team { get; set; }
    }

    public class FieldReference
    {
        public string referenceName { get; set; }
        public string url { get; set; }
    }

    public class TeamFieldValue
    {
        public string value { get; set; }
        public bool includeChildren { get; set; }
    }
}