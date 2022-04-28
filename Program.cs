// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;

Console.WriteLine("Hello, World!");

foreach (KnownFolder knownFolder in Enum.GetValues<KnownFolder>())
{
    try
    {
        Console.Write($"{knownFolder}: ");
        Console.WriteLine(KnownFolders.GetPath(knownFolder));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"<Exception> {ex.Message}");
    }
    Console.WriteLine();
}

enum KnownFolder
{
    Documents,
    Downloads,
    Music,
    Pictures,
    SavedGames,
    Favorites,
    SkyDriveCameraRoll,
    CommonPrograms,
    // ...
}

static class KnownFolders
{
    private static readonly Dictionary<KnownFolder, Guid> _knownFolderGuids = new()
    {
        [KnownFolder.Documents] = new("FDD39AD0-238F-46AF-ADB4-6C85480369C7"),
        [KnownFolder.Downloads] = new("374DE290-123F-4565-9164-39C4925E467B"),
        [KnownFolder.Music] = new("4BD8D571-6D19-48D3-BE97-422220080E43"),
        [KnownFolder.Pictures] = new("33E28130-4E1E-4676-835A-98395C3BC3BB"),
        [KnownFolder.Favorites] = new("1777F761-68AD-4D8A-87BD-30B759FA33DD"),
        [KnownFolder.SkyDriveCameraRoll] = new("767E6811-49CB-4273-87C2-20F355E1085B"),
        [KnownFolder.CommonPrograms] = new("0139D44E-6AFE-49F2-8690-3DAFCAE6FFB8"),
        [KnownFolder.SavedGames] = new("4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4")
    };

    public static string GetPath(KnownFolder folder)
    {
        return SHGetKnownFolderPath(_knownFolderGuids[folder], 0);
    }

    [DllImport("shell32", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
    private static extern string SHGetKnownFolderPath(
        [MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, nint hToken = default);
}

