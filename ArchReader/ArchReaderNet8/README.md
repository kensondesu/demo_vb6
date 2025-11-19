# ArchReader .NET 8

This is a migration of the ArchReader VB6 application to .NET 8 using Windows Forms.

## Project Information

- **Framework**: .NET 8.0 (Windows)
- **Application Type**: Windows Forms Application
- **Original**: Visual Basic 6.0 Application

## Features

- **ZIP Archive Support**: Open and browse ZIP archives
- **Tree View Navigation**: Hierarchical display of archive contents
- **Document Viewer**: Preview text files, images, and other content
- **File Type Detection**: Automatic detection and handling of different file types
- **Metadata Display**: Shows archive information (title, author, publisher)
- **Cross-Platform Ready**: Built on .NET 8 with Windows Forms

## Building the Application

```bash
dotnet build
```

## Running the Application

```bash
dotnet run
```

## Architecture

The application follows the original VB6 architecture with the following components:

### Core Classes

- **CArchive**: Represents an archive with metadata (title, author, publisher, etc.)
- **CProcesser**: Handles archive operations (open file, folder, URL) with ZIP support
- **CURLInfo**: Parses and stores URL information
- **CStringPair**: Utility class for key-value pairs
- **CDocumentInfo**: Document metadata
- **Utilities**: Helper functions for path handling, MIME types, and file operations

### Interfaces

- **IReader**: Interface for document reading operations
- **IViewer**: Interface for document viewing
- **IDocumentHandler**: Interface for document handling

### UI Components

- **MainForm**: Main application window with menu, tree view, and document viewer
- **DocumentViewer**: Custom control for viewing text files, images, and other content

## Migration Notes

### Key Changes from VB6

1. **UI Framework**: Migrated from VB6 Forms to Windows Forms .NET
2. **WebBrowser Control**: Prepared for migration to WebView2 (currently using Panel placeholder)
3. **Type System**: Migrated from VB6 types to C# types
4. **Properties**: VB6 property getters converted to C# properties
5. **Error Handling**: VB6 error handling converted to try-catch blocks
6. **Collections**: VB6 collections converted to .NET collections

### Implemented Features

✅ ZIP archive reading and browsing  
✅ Hierarchical tree view with folder structure  
✅ Text file viewer (txt, html, xml, json, code files)  
✅ Image viewer (jpg, png, gif, bmp)  
✅ Metadata parsing from archive info files  
✅ Menu system (File, View, Help)  
✅ Status bar with file information  
✅ File size formatting  
✅ MIME type detection  

### Future Enhancements

The following features from the original VB6 application could be added:

1. **WebView2 Integration**: For HTML document viewing with JavaScript support
2. **Plugin System**: Extensible architecture for custom readers and viewers
3. **HTTP Server**: Built-in web server for remote access
4. **Bookmark Management**: Save and manage favorite archive entries
5. **Options Dialog**: User preferences and settings
6. **Advanced Navigation**: History, search, and filtering
7. **RAR/7Z Support**: Additional archive format support
8. **File Extraction**: Extract files from archives

### Dependencies

- **System.IO.Compression**: Used for ZIP archive support (built-in to .NET)
- **System.Windows.Forms**: Windows Forms UI framework
- **System.Drawing**: For image display capabilities

## License

Copyright (C) 2008-2025 MYPLACE

## Original Project

This application is a migration from the VB6 ArchReader project located in the parent directory.
