using System.IO.Compression;
using System.Text;
using System.Windows.Forms;

namespace ArchReaderNet8
{
    /// <summary>
    /// Document viewer control for displaying archive contents
    /// </summary>
    public class DocumentViewer : UserControl
    {
        private readonly RichTextBox textViewer;
        private readonly PictureBox imageViewer;
        private readonly Panel placeholderPanel;
        private readonly Label placeholderLabel;
        private string? currentArchivePath;
        private CArchive? currentArchive;

        public DocumentViewer()
        {
            // Text viewer for text files
            textViewer = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new System.Drawing.Font("Consolas", 10),
                Visible = false
            };

            // Image viewer for images
            imageViewer = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Visible = false
            };

            // Placeholder panel
            placeholderPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.WhiteSmoke
            };

            placeholderLabel = new Label
            {
                Text = "Select a file from the tree to view its contents",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Italic),
                ForeColor = System.Drawing.Color.Gray
            };

            placeholderPanel.Controls.Add(placeholderLabel);

            Controls.Add(textViewer);
            Controls.Add(imageViewer);
            Controls.Add(placeholderPanel);
        }

        public void SetArchive(string archivePath, CArchive archive)
        {
            currentArchivePath = archivePath;
            currentArchive = archive;
        }

        public void ViewEntry(string entryPath)
        {
            if (string.IsNullOrEmpty(currentArchivePath) || currentArchive == null)
            {
                ShowPlaceholder("No archive loaded");
                return;
            }

            try
            {
                using var zipArchive = ZipFile.OpenRead(currentArchivePath);
                var entry = zipArchive.Entries.FirstOrDefault(e => 
                    e.FullName.Equals(entryPath, StringComparison.OrdinalIgnoreCase));

                if (entry == null)
                {
                    ShowPlaceholder($"Entry not found: {entryPath}");
                    return;
                }

                var extension = Utilities.GetExtension(entry.Name).ToLowerInvariant();

                // Handle different file types
                if (IsTextFile(extension))
                {
                    ViewTextEntry(entry);
                }
                else if (IsImageFile(extension))
                {
                    ViewImageEntry(entry);
                }
                else
                {
                    ShowPlaceholder($"Cannot preview {extension.ToUpperInvariant()} files\n\n" +
                                  $"File: {entry.Name}\n" +
                                  $"Size: {FormatFileSize(entry.Length)}");
                }
            }
            catch (Exception ex)
            {
                ShowPlaceholder($"Error viewing file:\n{ex.Message}");
            }
        }

        private void ViewTextEntry(ZipArchiveEntry entry)
        {
            using var stream = entry.Open();
            using var reader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
            var content = reader.ReadToEnd();

            textViewer.Text = content;
            ShowControl(textViewer);
        }

        private void ViewImageEntry(ZipArchiveEntry entry)
        {
            using var stream = entry.Open();
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            memoryStream.Position = 0;

            imageViewer.Image = System.Drawing.Image.FromStream(memoryStream);
            ShowControl(imageViewer);
        }

        private void ShowPlaceholder(string message)
        {
            placeholderLabel.Text = message;
            ShowControl(placeholderPanel);
        }

        private void ShowControl(Control control)
        {
            textViewer.Visible = false;
            imageViewer.Visible = false;
            placeholderPanel.Visible = false;

            control.Visible = true;
            control.BringToFront();
        }

        private bool IsTextFile(string extension)
        {
            return extension switch
            {
                "txt" or "text" or "md" or "markdown" or 
                "html" or "htm" or "xml" or "json" or 
                "css" or "js" or "cs" or "vb" or 
                "c" or "cpp" or "h" or "hpp" or 
                "py" or "java" or "php" or "rb" or 
                "sql" or "log" or "ini" or "cfg" or "config" => true,
                _ => false
            };
        }

        private bool IsImageFile(string extension)
        {
            return extension switch
            {
                "jpg" or "jpeg" or "png" or "gif" or 
                "bmp" or "ico" or "tif" or "tiff" => true,
                _ => false
            };
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }

        public void Clear()
        {
            textViewer.Text = string.Empty;
            imageViewer.Image = null;
            ShowPlaceholder("Select a file from the tree to view its contents");
        }
    }
}
