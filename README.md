# expense-agent

This project showcases how to automate a business process using an AI agent built with .NET. It leverages [Semantic Kernel](https://aka.ms/SemanticKernel) to integrate with a model deployed via [Azure AI Foundry](https://learn.microsoft.com/en-us/azure/ai-foundry/what-is-azure-ai-foundry), using custom tools to analyze expense report data and generate recommendations for approval, denial, or further review.

## Prerequisites

Before you begin, ensure you have the following:

- An Azure AI Foundry model that supports custom tools  
  - Deployment name  
  - Endpoint  
  - API key  
  - [Documentation](https://ai.azure.com/explore/models)
- .NET SDK 8.0 or higher

## Getting Started

1. **Clone the repository**

2. **Add your credentials to `Program.cs`**

   ```csharp
   var deploymentName = "your deployment name";
   var endpoint = "your endpoint";
   var apiKey = "your api key";

3. **Build and run the application**

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.