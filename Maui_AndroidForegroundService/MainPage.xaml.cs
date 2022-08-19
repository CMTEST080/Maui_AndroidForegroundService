using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Maui_AndroidForegroundService.Messages;
using Maui_AndroidForegroundService.Tasks;

namespace Maui_AndroidForegroundService;

public partial class MainPage : ContentPage, IRecipient<TickedMessage>
{
    int count = 0;

    CancellationTokenSource cts = new CancellationTokenSource();

    public MainPage()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<TickedMessage>(this);
    }


    void IRecipient<TickedMessage>.Receive(TickedMessage message)
    {
        count++;

        if (count == 1)
            counter.Text = $"Clicked {count} time";
        else
            counter.Text = $"Clicked {count} times";
    }

    private async void Button_LongRunningTaskStart_Clicked(object sender, EventArgs e)
    {
#if __ANDROID__
        var message = new StartLongRunningTaskMessage("");

        WeakReferenceMessenger.Default.Send(message);
#else
        var task = new CounterTask();
        await task.RunCounter(cts.Token);
#endif


    }

    private void Button_LongRunningTaskStop_Clicked(object sender, EventArgs e)
    {
#if __ANDROID__
        var message = new StopLongRunningTaskMessage("");

        WeakReferenceMessenger.Default.Send(message); 
#else
        cts.Cancel();
#endif
    }
}