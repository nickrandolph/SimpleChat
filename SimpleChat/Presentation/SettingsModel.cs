namespace SimpleChat.Presentation;

public partial class SettingsModel
{
    private readonly IWritableOptions<SettingsInfo> _settings;

    public string[] ServiceModels { get; } =
        [
            "gpt-35-turbo-0301",
            "gpt-35-turbo",
            "gpt-35-turbo-16k-0613",
            "gpt-35-turbo-1106",
            "gpt-35-turbo-0125",
            "gpt-4",
            "gpt-4-32k-0314",
            "gpt-4",
            "gpt-4-32k-0613",
            "gpt-4-1106-preview",
            "gpt-4-0125-preview",
            "gpt-4-vision-preview",
            "gpt-3.5-turbo-instruct-0914",
            "text-embedding-ada-002-2",
            "text-embedding-ada-002-1",
            "text-embedding-3-small",
            "text-embedding-3-large"
        ];


    public IState<string> SelectedServiceModel => State<string>.Value(this, () => _settings.Value.ServiceModel?? "gpt-4-1106-preview");

    public SettingsModel(IWritableOptions<SettingsInfo> settings)
    {
        _settings = settings;

        SelectedServiceModel.ForEachAsync(async (serviceModel, ct) =>
        {
            await _settings.UpdateAsync(settings => settings with { ServiceModel = serviceModel });
        });
    }
}
