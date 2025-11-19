using System.IO.Compression;

namespace ArchReaderNet8
{
    /// <summary>
    /// Processer class for handling archive operations
    /// </summary>
    public class CProcesser
    {
        /// <summary>
        /// Open a file and return archive information
        /// </summary>
        public CArchive? OpenFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            var extension = Path.GetExtension(path).ToLowerInvariant();
            
            return extension switch
            {
                ".zip" => OpenZipArchive(path),
                _ => OpenFolderArchive(path)
            };
        }

        /// <summary>
        /// Open a folder and return archive information
        /// </summary>
        public CArchive? OpenFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                return null;
            }

            return OpenFolderArchive(path);
        }

        /// <summary>
        /// Open a URL and return archive information
        /// </summary>
        public CArchive? OpenUrl(string url)
        {
            // TODO: Implement URL opening logic (download and process)
            return null;
        }

        /// <summary>
        /// Open an entry in an archive
        /// </summary>
        public string OpenArchive(CArchive archive, string entry)
        {
            // TODO: Implement archive entry opening logic
            return string.Empty;
        }

        private CArchive? OpenZipArchive(string zipPath)
        {
            try
            {
                using var zipArchive = ZipFile.OpenRead(zipPath);
                
                var archive = new CArchive
                {
                    Title = Path.GetFileNameWithoutExtension(zipPath),
                    Entries = zipArchive.Entries
                        .Select(e => e.FullName)
                        .ToArray()
                };

                // Try to find and read metadata from a manifest or info file
                var infoEntry = zipArchive.Entries.FirstOrDefault(e => 
                    e.Name.Equals("info.txt", StringComparison.OrdinalIgnoreCase) ||
                    e.Name.Equals("manifest.txt", StringComparison.OrdinalIgnoreCase));

                if (infoEntry != null)
                {
                    using var reader = new StreamReader(infoEntry.Open());
                    ParseMetadata(archive, reader.ReadToEnd());
                }

                return archive;
            }
            catch
            {
                return null;
            }
        }

        private CArchive? OpenFolderArchive(string folderPath)
        {
            try
            {
                var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
                
                var archive = new CArchive
                {
                    Title = Path.GetFileName(folderPath),
                    Entries = files
                        .Select(f => Path.GetRelativePath(folderPath, f))
                        .ToArray()
                };

                return archive;
            }
            catch
            {
                return null;
            }
        }

        private void ParseMetadata(CArchive archive, string metadata)
        {
            var lines = metadata.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ':', '=' }, 2);
                if (parts.Length != 2) continue;

                var key = parts[0].Trim().ToLowerInvariant();
                var value = parts[1].Trim();

                switch (key)
                {
                    case "title":
                        archive.Title = value;
                        break;
                    case "author":
                        archive.Author = value;
                        break;
                    case "publisher":
                        archive.Publisher = value;
                        break;
                    case "date":
                    case "created":
                        archive.CreatedDate = value;
                        break;
                    case "catalog":
                        archive.Catalog = value;
                        break;
                    case "defaultfile":
                    case "default":
                        archive.DefaultFile = value;
                        break;
                }
            }
        }

        /// <summary>
        /// Get URL information
        /// </summary>
        public CURLInfo GetUrlInfo(string url)
        {
            var urlInfo = new CURLInfo { URL = url };
            
            try
            {
                var uri = new Uri(url);
                urlInfo.Protocol = uri.Scheme;
                urlInfo.Host = uri.Host;
                urlInfo.Path = uri.AbsolutePath;
                urlInfo.Query = uri.Query;
                urlInfo.Fragment = uri.Fragment;
            }
            catch
            {
                // Invalid URL
            }

            return urlInfo;
        }

        /// <summary>
        /// Check if processor can handle the given URL
        /// </summary>
        public bool CanHandleURL(string url)
        {
            // TODO: Implement URL handling check
            return false;
        }
    }
}
