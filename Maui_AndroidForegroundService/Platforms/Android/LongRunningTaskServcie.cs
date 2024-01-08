using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using CommunityToolkit.Mvvm.Messaging;
using Maui_AndroidForegroundService.Messages;
using Maui_AndroidForegroundService.Tasks;
using OperationCanceledException = System.OperationCanceledException;

namespace Maui_AndroidForegroundService.Platforms.Android;

[Service(Name = "Maui_AndroidForegroundService.Platforms.Android.LongRunningTaskServcie",
         ForegroundServiceType = ForegroundService.TypeDataSync)]
public class LongRunningTaskServcie : Service
{
    CancellationTokenSource _cts;

    public override IBinder OnBind(Intent intent)
    {
        return null;
    }

    [return: GeneratedEnum]
    public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
    {
        _cts = new CancellationTokenSource();

        Task.Run(() =>
        {
            try
            {
                var counter = new CounterTask();

                counter.RunCounter(_cts.Token).Wait();
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                if (_cts.IsCancellationRequested)
                {
                    var message = new CancelledMessage("");

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        WeakReferenceMessenger.Default.Send(message);
                    });
                }
            }
        });
        return base.OnStartCommand(intent, flags, startId);
    }
}
