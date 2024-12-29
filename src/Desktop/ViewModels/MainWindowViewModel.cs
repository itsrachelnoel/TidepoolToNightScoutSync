using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dumpify;
using Microsoft.Extensions.Options;
using TidepoolToNightScoutSync.Core.Services.Tidepool;

namespace Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string TidepoolUsername { get; set; } = "";
    public string TidepoolPassword { get; set; } = "";
    public string NightscoutUrl { get; set; } = "";
    public string NightscoutApiKey { get; set; } = "";

    public async Task Test(IServiceProvider services)
    {
        StateManager.State.TidepoolUsername = TidepoolUsername;
        StateManager.State.TidepoolPassword = TidepoolPassword;
        await StateManager.SaveState();

        var tidepool = await new TidepoolClientFactory(Options.Create(new TidepoolClientOptions
        {
            Username = TidepoolUsername,
            Password = TidepoolPassword
        }), new HttpClient()).CreateAsync();

        var boluses = await tidepool.GetBolusAsync(DateTime.Now.AddMonths(-1));
        boluses.DumpConsole();
    }
}