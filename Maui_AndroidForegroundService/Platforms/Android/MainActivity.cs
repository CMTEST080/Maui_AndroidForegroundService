using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CommunityToolkit.Mvvm.Messaging;
using Maui_AndroidForegroundService.Messages;
using Maui_AndroidForegroundService.Platforms.Android;

namespace Maui_AndroidForegroundService;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity, IRecipient<StartLongRunningTaskMessage>, IRecipient<StopLongRunningTaskMessage>
{

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        WeakReferenceMessenger.Default.Register<StartLongRunningTaskMessage>(this);
        WeakReferenceMessenger.Default.Register<StopLongRunningTaskMessage>(this);
    }

    void IRecipient<StartLongRunningTaskMessage>.Receive(StartLongRunningTaskMessage message)
    {
        var intent = new Intent(this, typeof(LongRunningTaskServcie));

        StopService(intent);

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            StartForegroundService(intent);
        }
        else
        {
            StartService(intent);
        }
    }

    void IRecipient<StopLongRunningTaskMessage>.Receive(StopLongRunningTaskMessage message)
    {
        var intent = new Intent(this, typeof(LongRunningTaskServcie));
        StopService(intent);
    }
}
