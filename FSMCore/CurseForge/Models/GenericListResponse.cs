using System.Collections.Generic;

namespace CurseForge.APIClient.Models
{
    public class GenericListResponse<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public Pagination Pagination { get; set; }
    }
}
