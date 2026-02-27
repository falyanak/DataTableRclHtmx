namespace DataTable.Rcl.Models
{
   public class DataTableViewModel<T, TKey> 
    where T : IEntity<TKey> 
    where TKey : IEquatable<TKey>
{
       public TKey? SelectedId { get; set; }
        public List<T> Items { get; set; } = new();
        public List<string> Columns { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public string BaseApiUrl { get; set; } = "";     // Ex: /Product/GetTable
        public string? DetailsUrl { get; set; }          // Ex: /Product/Details/
        public string? DeleteApiUrl { get; set; }        // Ex: /Product/Delete/
        public string? ExportApiUrl { get; set; }        // Ex: /Product/ExportPdf
        public string TargetId { get; set; } = "";
        public string Search { get; set; } = "";
        public string SortBy { get; set; } = "";
        public bool IsAsc { get; set; }
    }
}