using ExpenseAgent;
using ExpenseAgent.Models;

var deploymentName = "";
var endpoint = "";
var apiKey = "";

var expenseAgent = new Agent(deploymentName, endpoint, apiKey);

var employees = new[] {"Alex", "Sam"};

foreach (var employee in employees)
{
    var recommendation = await expenseAgent.ProcessExpenseReportAsync(employee);
    ArgumentNullException.ThrowIfNull(recommendation, nameof(ExpenseReportRecommendation));

    Console.WriteLine($"Recommendation for {employee}:");
    Console.WriteLine($"Employee Name: {recommendation.EmployeeName}");
    Console.WriteLine($"Report Date: {recommendation.ReportDate.ToShortDateString()}");
    Console.WriteLine($"Amount Reported: {recommendation.AmountReported:C}");
    Console.WriteLine($"Receipts Total: {recommendation.ReceiptsTotal:C}");
    Console.WriteLine($"Recommendation: {recommendation.Recommendation}");
    Console.WriteLine($"Summary: {recommendation.Summary}");
    Console.WriteLine("-------------------------------------------------------");
}

Console.WriteLine("Press any key to continue...");
Console.ReadKey();
