namespace EmployeeLeaveManagement.API.Helpers
{
    public class EmployeeQueryParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }

        public string? Department { get; set; }

        public string? SortBy { get; set; }

        public string Order { get; set; } = "asc";
    }
}