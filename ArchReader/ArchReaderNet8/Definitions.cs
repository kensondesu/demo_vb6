namespace ArchReaderNet8
{
    /// <summary>
    /// ZZZ State enumeration
    /// </summary>
    public enum ZZZState
    {
        Undefined = 0,
        Yes = 1,
        No = -1
    }

    /// <summary>
    /// ZZZ String Pair structure
    /// </summary>
    public struct ZZZStringPair
    {
        public string LValue { get; set; }
        public string RValue { get; set; }
    }

    /// <summary>
    /// List Node structure
    /// </summary>
    public struct TListNode
    {
        public string Text { get; set; }
        public string Entry { get; set; }
    }

    /// <summary>
    /// Info structure
    /// </summary>
    public struct TInfo
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Catalog { get; set; }
        public string Publisher { get; set; }
        public string Entry { get; set; }
    }

    /// <summary>
    /// Plugin type constants
    /// </summary>
    public static class PluginType
    {
        public const int Reader = 0;
        public const int Document = 1;
        public const int Viewer = 2;
    }
}
