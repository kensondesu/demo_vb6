# ArchReader .NET 8

This is a migration of the ArchReader VB6 application to .NET 8 using Windows Forms.

## Project Information

- **Framework**: .NET 8.0 (Windows)
- **Application Type**: Windows Forms Application
- **Original**: Visual Basic 6.0 Application

## Features

- Archive file reader
- Tree view navigation
- Document viewing capabilities
- Plugin architecture support

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
- **CProcesser**: Handles archive operations (open file, folder, URL)
- **CURLInfo**: Parses and stores URL information
- **CStringPair**: Utility class for key-value pairs
- **CDocumentInfo**: Document metadata

### Interfaces

- **IReader**: Interface for document reading operations
- **IViewer**: Interface for document viewing
- **IDocumentHandler**: Interface for document handling

### Forms

- **MainForm**: Main application window with tree view and content panel

## Migration Notes

### Key Changes from VB6

1. **UI Framework**: Migrated from VB6 Forms to Windows Forms .NET
2. **WebBrowser Control**: Prepared for migration to WebView2 (currently using Panel placeholder)
3. **Type System**: Migrated from VB6 types to C# types
4. **Properties**: VB6 property getters converted to C# properties
5. **Error Handling**: VB6 error handling converted to try-catch blocks
6. **Collections**: VB6 collections converted to .NET collections

### Not Yet Implemented

The following features from the original VB6 application need implementation:

1. Archive file loading (ZIP support)
2. HTML document viewing
3. Plugin system
4. HTTP server functionality
5. Bookmark management
6. Options/Settings dialog
7. Full navigation features

### Dependencies Needed

- **System.IO.Compression**: For ZIP archive support
- **Microsoft.Web.WebView2.WinForms**: For modern web content display
- **HtmlAgilityPack** (optional): For HTML parsing

## License

Copyright (C) 2008-2025 MYPLACE

## Original Project

This application is a migration from the VB6 ArchReader project located in the parent directory.
