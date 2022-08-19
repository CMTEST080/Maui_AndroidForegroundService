using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Maui_AndroidForegroundService.Messages;

public class StartLongRunningTaskMessage : ValueChangedMessage<string>
{
    public StartLongRunningTaskMessage(string value) : base(value)
    {
    }
}