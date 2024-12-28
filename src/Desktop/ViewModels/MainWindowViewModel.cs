using System;

namespace Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string TidepoolUsername { get; set; } = "";
    public string TidepoolPassword { get; set; } = "";
    public string NightscoutUrl  { get; set; } = "";
    public string NightscoutApiKey  { get; set; } = "";

    public void Test()
    {
        Console.WriteLine($"TidepoolUsername: {TidepoolUsername}");
        Console.WriteLine($"TidepoolPassword: {TidepoolPassword}");
        Console.WriteLine($"NightscoutUrl: {NightscoutUrl}");
        Console.WriteLine($"NightscoutApiKey: {NightscoutApiKey}");
    }
}