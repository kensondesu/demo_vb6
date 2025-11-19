namespace ArchReaderNet8
{
    /// <summary>
    /// Document handler interface
    /// </summary>
    public interface IDocumentHandler
    {
        bool CanHandle(string documentPath);
        CDocumentInfo GetDocumentInfo(string documentPath);
        string[] GetEntries(string documentPath);
        byte[] GetContent(string documentPath, string entry);
    }
}
