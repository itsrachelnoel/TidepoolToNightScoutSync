using System;
using System.Threading.Tasks;

namespace Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string TidepoolUsername { get; set; } = "";
    public string TidepoolPassword { get; set; } = "";
    public string NightscoutUrl  { get; set; } = "";
    public string NightscoutApiKey  { get; set; } = "";

    public async Task Test(IServiceProvider services)
    {
        Console.WriteLine($"TidepoolUsername: {TidepoolUsername}");
        Console.WriteLine($"TidepoolPassword: {TidepoolPassword}");

        await Task.Delay(TimeSpan.FromSeconds(0.5));
        Console.WriteLine($"NightscoutUrl: {NightscoutUrl}");
        Console.WriteLine($"NightscoutApiKey: {NightscoutApiKey}");
    }
}