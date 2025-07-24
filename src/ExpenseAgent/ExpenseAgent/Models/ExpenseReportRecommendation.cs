namespace ExpenseAgent.Models;

public class ExpenseReportRecommendation
{
    public string EmployeeName { get; set; } = string.Empty;

    public DateTime ReportDate { get; set; }

    public decimal AmountReported { get; set; }

    public decimal ReceiptsTotal { get; set; }

    public string Recommendation { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;
}
