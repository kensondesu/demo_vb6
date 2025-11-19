namespace ArchReaderNet8
{
    /// <summary>
    /// URL information class
    /// </summary>
    public class CURLInfo
    {
        public string URL { get; set; } = string.Empty;
        public string Protocol { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Query { get; set; } = string.Empty;
        public string Fragment { get; set; } = string.Empty;
    }
}
