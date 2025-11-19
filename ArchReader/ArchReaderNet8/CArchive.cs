namespace ArchReaderNet8
{
    /// <summary>
    /// Visibility state for UI elements
    /// </summary>
    public enum ZhtmVisibility
    {
        Default = 0,
        Show = 1,
        Hide = -1
    }

    /// <summary>
    /// Archive class representing a document archive
    /// </summary>
    public class CArchive
    {
        public ZhtmVisibility zvShowLeft { get; set; }
        public ZhtmVisibility zvShowMenu { get; set; }
        public ZhtmVisibility zvShowStatusBar { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string Catalog { get; set; } = string.Empty;
        public string DefaultFile { get; set; } = string.Empty;

        public ZZZState ShowLeft
        {
            get
            {
                return zvShowLeft switch
                {
                    ZhtmVisibility.Show => ZZZState.Yes,
                    ZhtmVisibility.Hide => ZZZState.No,
                    _ => ZZZState.Undefined
                };
            }
        }

        public ZZZState ShowMenu
        {
            get
            {
                return zvShowMenu switch
                {
                    ZhtmVisibility.Show => ZZZState.Yes,
                    ZhtmVisibility.Hide => ZZZState.No,
                    _ => ZZZState.Undefined
                };
            }
        }

        public ZZZState ShowStatusBar
        {
            get
            {
                return zvShowStatusBar switch
                {
                    ZhtmVisibility.Show => ZZZState.Yes,
                    ZhtmVisibility.Hide => ZZZState.No,
                    _ => ZZZState.Undefined
                };
            }
        }

        public string[] Entries { get; set; } = Array.Empty<string>();
    }
}
