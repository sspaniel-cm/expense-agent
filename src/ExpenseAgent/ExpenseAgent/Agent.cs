using System.Text.Json;
using System.Text.Json.Schema;
using ExpenseAgent.Models;
using ExpenseAgent.Tools;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace ExpenseAgent;

public class Agent
{
    private readonly ChatCompletionAgent _chatCompletionAgent;

    public Agent(string deploymentName, string endpoint, string apiKey)
    {
        var kernel = Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(deploymentName, endpoint, apiKey)
            .Build();

        kernel.Plugins.AddFromType<ExpenseReportTools>(nameof(ExpenseReportTools));

        _chatCompletionAgent = new ChatCompletionAgent
        {
            Name = "Expense-Agent",
            Description = "Agent for expense report processing.",
            Instructions = $"""
                            You are a expense report processing agent.
                            Apply the organization's expense policy to recommend if expense reports should be approved, denied, or referred to a manager.
                            'Approve' means the total amount matches the receipts and is within policy limits and rules.
                            'Deny' means the total amount does not match the receipts, exceeds policy limits, or violates rules.
                            'Refer' means the expense report requires further review by a manager.
                            Return json with the schema:
                            {JsonSerializerOptions.Default.GetJsonSchemaAsNode(typeof(ExpenseReportRecommendation))}
                            """,
            Kernel = kernel,
            Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Required()
                })
        };
    }

    public async Task<ExpenseReportRecommendation?> ProcessExpenseReportAsync(string employeeName)
    {
        var chatMessage = new ChatMessageContent(AuthorRole.User, $"Process the expense report: {employeeName}.");

        await foreach (ChatMessageContent chatMessageContent in _chatCompletionAgent.InvokeAsync(chatMessage))
        {
            var response = chatMessageContent.Content ?? string.Empty;

            if (!response.StartsWith("{"))
            {
                continue;
            }

            var expensesReportDecision = JsonSerializer.Deserialize<ExpenseReportRecommendation>(response);
            return expensesReportDecision;
        }

        throw new InvalidOperationException("Failed to process expense report");
    }
}