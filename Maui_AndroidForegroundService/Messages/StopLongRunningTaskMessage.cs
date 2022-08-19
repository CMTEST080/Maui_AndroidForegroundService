using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_AndroidForegroundService.Messages;

public class StopLongRunningTaskMessage : ValueChangedMessage<string>
{
    public StopLongRunningTaskMessage(string value) : base(value)
    {
    }
}
