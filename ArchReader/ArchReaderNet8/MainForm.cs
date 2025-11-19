using System.Windows.Forms;

namespace ArchReaderNet8
{
    /// <summary>
    /// Main form for ArchReader application
    /// </summary>
    public partial class MainForm : Form
    {
        private TreeView? treeView;
        private StatusStrip? statusStrip;
        private ToolStripStatusLabel? statusLabel;
        private SplitContainer? splitContainer;
        private Panel? contentPanel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Set form properties
            this.Text = "ArchReader - .NET 8";
            this.Size = new System.Drawing.Size(1024, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            // Create split container
            splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 250
            };

            // Create tree view for archive contents
            treeView = new TreeView
            {
                Dock = DockStyle.Fill,
                Name = "treeView"
            };
            splitContainer.Panel1.Controls.Add(treeView);

            // Create content panel
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Name = "contentPanel",
                BackColor = System.Drawing.Color.White
            };
            splitContainer.Panel2.Controls.Add(contentPanel);

            // Create status strip
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel
            {
                Name = "statusLabel",
                Text = "Ready",
                Spring = true,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            statusStrip.Items.Add(statusLabel);

            // Create menu strip
            var menuStrip = new MenuStrip();
            
            // File menu
            var fileMenu = new ToolStripMenuItem("&File");
            var openMenuItem = new ToolStripMenuItem("&Open", null, OnOpenFile);
            openMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            var exitMenuItem = new ToolStripMenuItem("E&xit", null, (s, e) => Application.Exit());
            fileMenu.DropDownItems.Add(openMenuItem);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(exitMenuItem);

            // View menu
            var viewMenu = new ToolStripMenuItem("&View");
            var refreshMenuItem = new ToolStripMenuItem("&Refresh", null, OnRefresh);
            refreshMenuItem.ShortcutKeys = Keys.F5;
            viewMenu.DropDownItems.Add(refreshMenuItem);

            // Help menu
            var helpMenu = new ToolStripMenuItem("&Help");
            var aboutMenuItem = new ToolStripMenuItem("&About", null, OnAbout);
            helpMenu.DropDownItems.Add(aboutMenuItem);

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(viewMenu);
            menuStrip.Items.Add(helpMenu);

            // Add controls to form
            this.Controls.Add(splitContainer);
            this.Controls.Add(statusStrip);
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;
        }

        private void OnOpenFile(object? sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Archive Files (*.zip)|*.zip|All Files (*.*)|*.*",
                Title = "Open Archive"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadArchive(openFileDialog.FileName);
            }
        }

        private void LoadArchive(string filePath)
        {
            try
            {
                if (statusLabel != null)
                {
                    statusLabel.Text = $"Loading: {filePath}";
                }

                var processer = new CProcesser();
                var archive = processer.OpenFile(filePath);

                if (archive != null && treeView != null)
                {
                    treeView.Nodes.Clear();
                    var rootNode = treeView.Nodes.Add(archive.Title ?? "Archive");
                    rootNode.Tag = archive;
                    
                    // Add metadata if available
                    if (!string.IsNullOrEmpty(archive.Author))
                    {
                        rootNode.Text += $" - {archive.Author}";
                    }

                    // Build tree structure from entries
                    var folderNodes = new Dictionary<string, TreeNode>();
                    
                    foreach (var entry in archive.Entries.OrderBy(e => e))
                    {
                        var parts = entry.Split('/', '\\');
                        TreeNode parentNode = rootNode;

                        // Create folder nodes
                        for (int i = 0; i < parts.Length - 1; i++)
                        {
                            var folderPath = string.Join("/", parts.Take(i + 1));
                            
                            if (!folderNodes.TryGetValue(folderPath, out var folderNode))
                            {
                                folderNode = parentNode.Nodes.Add(parts[i]);
                                folderNode.Tag = folderPath;
                                folderNodes[folderPath] = folderNode;
                            }
                            
                            parentNode = folderNode;
                        }

                        // Add file node
                        var fileNode = parentNode.Nodes.Add(parts[^1]);
                        fileNode.Tag = entry;
                    }

                    rootNode.Expand();

                    if (statusLabel != null)
                    {
                        var fileCount = archive.Entries.Length;
                        statusLabel.Text = $"Loaded: {Path.GetFileName(filePath)} ({fileCount} files)";
                    }
                }
                else
                {
                    MessageBox.Show("Unable to load archive. The file may be corrupted or in an unsupported format.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    if (statusLabel != null)
                    {
                        statusLabel.Text = "Failed to load archive";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading archive: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                if (statusLabel != null)
                {
                    statusLabel.Text = "Error loading archive";
                }
            }
        }

        private void OnRefresh(object? sender, EventArgs e)
        {
            if (statusLabel != null)
            {
                statusLabel.Text = "Refreshing...";
            }
            // TODO: Implement refresh logic
        }

        private void OnAbout(object? sender, EventArgs e)
        {
            MessageBox.Show(
                "ArchReader - .NET 8\n\n" +
                "Archive Reader Application\n" +
                "Migrated from VB6 to .NET 8\n\n" +
                "Copyright (C) 2008-2025 MYPLACE",
                "About ArchReader",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
