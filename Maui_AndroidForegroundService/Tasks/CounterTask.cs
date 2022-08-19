using CommunityToolkit.Mvvm.Messaging;
using Maui_AndroidForegroundService.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_AndroidForegroundService.Tasks;

public class CounterTask
{
    public async Task RunCounter(CancellationToken token)
    {
        await Task.Run(async () =>
        {
            for (long i = 0; i < long.MaxValue; i++)
            {
                token.ThrowIfCancellationRequested();

                await Task.Delay(1000);
                var message = new TickedMessage("");

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    WeakReferenceMessenger.Default.Send(message);
                });
            }
        }, token);
    }
}
