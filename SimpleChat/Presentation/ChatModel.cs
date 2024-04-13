using Azure.AI.OpenAI;

namespace SimpleChat.Presentation;

public partial record ChatModel(
    Entity Entity,
    IOptions<AppConfig> Config,
    IOptions<SettingsInfo> Settings)
{
    public IState<string> Query => State<string>.Value(this, () => "Explain AI");

    public IState<string> Response => State<string>.Value(this, () => string.Empty);

    public async Task Send(string query)
    {
        var response = await GetChatGPTResponseAsync(query);
        await Response.SetAsync(response);
    }

    private async Task<string> GetChatGPTResponseAsync(string query)
    {
        var client = new OpenAIClient(Config.Value.ApiKey);

        var completionOptions = new ChatCompletionsOptions
        {
            DeploymentName = Settings.Value.ServiceModel,
        };

        completionOptions.Messages.Add(new ChatRequestUserMessage(query));

        var resp = await client.GetChatCompletionsAsync(completionOptions);

        if (resp?.Value?.Choices.FirstOrDefault() is { } response &&
            response.Message?.Content is { } responseMessage)
        {
            return responseMessage;
        }
        return "No response";
    }
}
