using System.Collections.Generic;

namespace CDS.AccessSample.Models
{

    /// <summary>
    /// Helper class to fetch data from CDS.
    /// </summary>
    public class Query
    {
        public List<string> Select { get; set; }
        public List<string> Filter { get; set; }
        public List<string> Expand { get; set; }
        public int PageSize { get; set; } = 5000;
        public string OrderBy { get; set; }
        public string EntityCollection { get; set; }
        public string Url
        {
            get
            {
                var segments = new List<string>();
                
                if (Select != null && Select.Count > 0)
                    segments.Add($"$select={string.Join(",", Select)}");

                if (Filter != null && Filter.Count > 0)
                    segments.Add($"$filter={string.Join(" and ", Filter)}");

                if (Expand != null && Expand.Count > 0)
                    segments.Add($"$expand={string.Join(",", Expand)}");

                if (!string.IsNullOrEmpty(OrderBy))
                    segments.Add($"$orderby={OrderBy}");

                return $"{EntityCollection}/?{string.Join("&", segments)}";
            }
        }
    }
}
