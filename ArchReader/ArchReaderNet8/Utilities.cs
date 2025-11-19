using System.Text;

namespace ArchReaderNet8
{
    /// <summary>
    /// Utility functions for the ArchReader application
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Convert bytes to string with null termination handling
        /// </summary>
        public static string BytesToString(byte[] bytes, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            
            // Find null terminator
            int nullIndex = Array.IndexOf(bytes, (byte)0);
            int length = nullIndex >= 0 ? nullIndex : bytes.Length;
            
            return encoding.GetString(bytes, 0, length);
        }

        /// <summary>
        /// Convert string to bytes with null termination
        /// </summary>
        public static byte[] StringToBytes(string str, Encoding? encoding = null, bool addNullTerminator = true)
        {
            encoding ??= Encoding.UTF8;
            var bytes = encoding.GetBytes(str);
            
            if (addNullTerminator)
            {
                var result = new byte[bytes.Length + 1];
                Array.Copy(bytes, result, bytes.Length);
                result[bytes.Length] = 0;
                return result;
            }
            
            return bytes;
        }

        /// <summary>
        /// Convert Unix path to Windows path
        /// </summary>
        public static string ToWindowsPath(string path)
        {
            return path.Replace('/', Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Convert Windows path to Unix path
        /// </summary>
        public static string ToUnixPath(string path)
        {
            return path.Replace(Path.DirectorySeparatorChar, '/');
        }

        /// <summary>
        /// Get file extension without dot
        /// </summary>
        public static string GetExtension(string path)
        {
            var ext = Path.GetExtension(path);
            return string.IsNullOrEmpty(ext) ? string.Empty : ext.TrimStart('.');
        }

        /// <summary>
        /// Check if path is a URL
        /// </summary>
        public static bool IsUrl(string path)
        {
            return Uri.TryCreate(path, UriKind.Absolute, out var uri) && 
                   (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Check if file is an archive
        /// </summary>
        public static bool IsArchive(string path)
        {
            var ext = GetExtension(path).ToLowerInvariant();
            return ext switch
            {
                "zip" => true,
                "rar" => true,
                "7z" => true,
                "tar" => true,
                "gz" => true,
                _ => false
            };
        }

        /// <summary>
        /// Get MIME type from file extension
        /// </summary>
        public static string GetMimeType(string path)
        {
            var ext = GetExtension(path).ToLowerInvariant();
            return ext switch
            {
                "html" or "htm" => "text/html",
                "txt" => "text/plain",
                "css" => "text/css",
                "js" => "application/javascript",
                "json" => "application/json",
                "xml" => "application/xml",
                "jpg" or "jpeg" => "image/jpeg",
                "png" => "image/png",
                "gif" => "image/gif",
                "svg" => "image/svg+xml",
                "pdf" => "application/pdf",
                "zip" => "application/zip",
                _ => "application/octet-stream"
            };
        }
    }
}
