using System.ComponentModel;
using System.Text.Json;
using Microsoft.SemanticKernel;

namespace ExpenseAgent.Tools;

public class ExpenseReportTools
{
    private const string PolicyPath = "Data/ExpensePolicy.txt";

    [KernelFunction(nameof(GetExpenseReport))]
    [Description("Gets the expense report for an employee on the specified report date.")]
    public JsonDocument? GetExpenseReport(string employeeName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Data", $"{employeeName}.json");

        if (!File.Exists(path))
        {
            return null;
        }

        var jsonContent = File.ReadAllText(path);
        var report = JsonDocument.Parse(jsonContent);
        return report;
    }


    [KernelFunction(nameof(GetExpensePolicyAsync))]
    [Description("Gets the travel expense policy for the organization.")]
    public async Task<string> GetExpensePolicyAsync()
    {
        var fullPath = Path.Combine(AppContext.BaseDirectory, PolicyPath);
        var policy = await File.ReadAllTextAsync(fullPath);
        return policy.Trim();
    }

}