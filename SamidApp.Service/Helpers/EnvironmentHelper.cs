namespace SamidApp.Service.Helpers;

public class EnvironmentHelper
{
    public static string WebRootPath { get; set; }
    public static string AttachmentPath => Path.Combine(WebRootPath, "files");
    public static string FilePath => "files";
}