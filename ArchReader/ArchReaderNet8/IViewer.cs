namespace ArchReaderNet8
{
    /// <summary>
    /// Viewer interface for handling document viewing operations
    /// </summary>
    public interface IViewer
    {
        void ShowDocument(string documentPath);
        void Close();
        bool CanHandle(string documentPath);
    }
}
