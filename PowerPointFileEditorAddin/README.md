# PowerPoint Folder Tree VSTO Add-in

## Overview

This PowerPoint VSTO add-in displays a folder structure via a WPF TreeView window using MVVM. The folder structure reflects a configurable base path and supports folder creation with lazy loading.

## How to Build and Run

1. Open `PowerPointFolderViewerAddin.sln` in Visual Studio.
2. Set `PowerPointFileEditorAddin` as the startup project.
3. Ensure Office Developer Tools are installed.
4. Build and run the solution.
5. Open PowerPoint → Ribbon Tab → Folder Manager → Open Folder Tree.

## Folder Structure Management

- The root folder path is configured in `App.config` under `BaseFolderPath`.
- TreeView loads subfolders lazily on expansion.
- New folders can be created directly from the TreeView node UI.
- All changes reflect on the actual file system.

## Assumptions

- Base folder path exists and is accessible.
- Folder names are unique within a directory.
- Error messages are logged to `log.txt`.

## Logging

- Any file system error (permissions, IO issues) is logged in `log.txt` in the app directory.
- 