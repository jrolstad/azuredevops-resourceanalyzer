using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class ApiResult<T>
    {
        public List<T> value { get; set; }
    }
}