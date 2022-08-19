using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Maui_AndroidForegroundService.Messages;

public class TickedMessage : ValueChangedMessage<string>
{
    public TickedMessage(string value) : base(value)
    {
    }
}

