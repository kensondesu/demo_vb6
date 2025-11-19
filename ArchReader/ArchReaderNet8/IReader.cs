namespace ArchReaderNet8
{
    /// <summary>
    /// Reader interface for handling document reading operations
    /// </summary>
    public interface IReader
    {
        void BeforeNavigate(ref object url, ref bool cancel, object ieView, string? targetFrameName = null);
        void NavigateComplete(ref object url, object ieView);
        void StatusTextChange(string text, object ieView);
        void GetView(string shortFile, object ieView, string? targetFrameName = null);
        void StartUp();
        void EndUp();
        void Navigate(string url, object ie, string? targetFrameName = null);
    }
}
