using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Desktop;

public static class StateManager
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };

    private static readonly string FilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "TidepoolToNightscoutSync",
        "state.json"
    );

    public static AppState State { get; private set; } = new();

    public static async Task SaveState()
    {
        var json = JsonSerializer.Serialize(State, Options);
        var directoryName = Path.GetDirectoryName(FilePath);
        ArgumentNullException.ThrowIfNull(directoryName);
        Directory.CreateDirectory(directoryName);
        await File.WriteAllTextAsync(FilePath, json);
    }

    public static async Task LoadState()
    {
        if (!File.Exists(FilePath))
        {
            return;
        }

        var json = await File.ReadAllTextAsync(FilePath);
        State = JsonSerializer.Deserialize<AppState>(json) ?? new AppState();
    }
}