# Music Player System

[English](README.md) | [ÖÐÎÄ](README_CN.md)

A simple and elegant cross-platform music player built with Avalonia UI and .NET 8.

![Music Player System](screenshot.png)

## :sparkles: Features

- :musical_note: **Audio Playback** - Play multiple audio formats (MP3, WAV, M4A, FLAC)
- :star: **Favorites Management** - Mark your favorite songs for quick access
- :memo: **Song Library** - Browse, add, edit, and delete songs
- :floppy_disk: **Data Persistence** - All your songs and favorites are saved automatically
- :art: **Modern UI** - Clean and intuitive interface with Fluent design
- :file_folder: **File Browser** - Import audio files from your computer
- :arrows_counterclockwise: **Reset Function** - Reset to default song list anytime

## :computer: System Requirements

- **Operating System**: Windows 10 or later (64-bit)
- **Framework**: .NET 8.0 Runtime (included in self-contained release)
- **Memory**: 512 MB RAM minimum
- **Storage**: 100 MB free space

## :package: Installation

### Option 1: Download Release (Recommended)

1. Download the latest release from the [Releases](https://github.com/futurefe414/MusicPlayer/releases) page
2. Extract the ZIP file to your preferred location
3. Run `Music.exe` to start the application

### Option 2: Build from Source

**Prerequisites:**
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or JetBrains Rider (optional)

**Steps:**

```bash
# Clone the repository
git clone https://github.com/futurefe414/MusicPlayer.git
cd MusicPlayer/Music

# Restore dependencies
dotnet restore

# Build the project
dotnet build -c Release

# Run the application
dotnet run
```

## :rocket: Publishing

To create a standalone executable:

```bash
# Navigate to project directory
cd Music

# Publish for Windows x64
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true

# Output will be in: bin\Release\net8.0\win-x64\publish\
```

## :book: Usage

### Playing Music

1. **Select a song** from the song list by clicking on it
2. **Click the Play button** (??) or use the play button in the Actions column
3. Use the control buttons:
   - ?? **Previous** - Go to the previous song
   - ?? **Play** - Play or resume playback
   - ?? **Pause** - Pause the current song
   - ?? **Stop** - Stop playback
   - ?? **Next** - Go to the next song

### Managing Your Library

#### Adding Songs

**Method 1: Browse Files**
1. Click **:file_folder: Browse Audio Files**
2. Select one or multiple audio files
3. Files will be automatically added to your library

**Method 2: Manual Entry**
1. Click **:heavy_plus_sign: Add New Song**
2. Fill in the song details:
   - Song Name
   - Artist
   - Album
   - Example Lyrics (optional)
   - File Path
3. Click **Save**

#### Editing Songs

1. Click the **:pencil2: Edit** button in the Actions column
2. Modify the song information
3. Click **Save** to apply changes

#### Deleting Songs

1. Click the **:x: Delete** button in the Actions column
2. The song will be removed from your library

### Favorites

- Click the **:heart: heart icon** in the Actions column to add/remove favorites
- Click **:star: Show Favorites** to filter and view only your favorite songs
- Click again to show all songs

### Data Management

- **:file_folder: Open Data Folder** - Opens the folder where your data is stored
  - Location: `%AppData%\MusicPlayer\`
  - Contains: `songs.json` and `favorites.json`

- **:arrows_counterclockwise: Reset to Default** - Restores the default song list
  - :warning: Warning: This will delete all user-added songs and favorites

## :wrench: Technology Stack

- **Framework**: [.NET 8.0](https://dotnet.microsoft.com/)
- **UI Framework**: [Avalonia UI 11.3.6](https://avaloniaui.net/)
- **Audio Engine**: [NAudio 2.2.1](https://github.com/naudio/NAudio)
- **Data Serialization**: System.Text.Json
- **Architecture**: MVVM pattern with code-behind

## :file_folder: Project Structure

```
Music/
©À©¤©¤ Assets/                      # Application resources
©¦   ©À©¤©¤ DefaultSongs/           # Default music files
©¦   ©¸©¤©¤ logo.ico                # Application icon
©À©¤©¤ Services/
©¦   ©À©¤©¤ AudioPlayerService.cs   # Audio playback logic
©¦   ©¸©¤©¤ MusicDataService.cs     # Data persistence logic
©À©¤©¤ MainWindow.axaml            # Main window UI
©À©¤©¤ MainWindow.axaml.cs         # Main window logic
©À©¤©¤ ManageSongWindow.axaml      # Song management dialog
©À©¤©¤ ManageSongWindow.axaml.cs   # Song management logic
©À©¤©¤ ConfirmDialog.axaml         # Confirmation dialog UI
©À©¤©¤ ConfirmDialog.axaml.cs      # Confirmation dialog logic
©À©¤©¤ App.axaml                   # Application styles
©À©¤©¤ App.axaml.cs                # Application entry point
©¸©¤©¤ Music.csproj                # Project configuration
```

## :gear: Configuration

### Default Songs

The application comes with 5 default Chinese songs. You can modify them by:

1. Editing the default songs in `MainWindow.axaml.cs` (line ~52)
2. Replacing MP3 files in `Assets/DefaultSongs/`
3. Rebuilding the project

### Data Storage

User data is stored in:
```
Windows: C:\Users\[YourUsername]\AppData\Roaming\MusicPlayer\
```

Files:
- `songs.json` - Your song library
- `favorites.json` - Your favorite songs list

## :handshake: Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## :memo: License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## :bug: Known Issues

- NAudio only supports Windows platform for audio playback
- First-time startup may be slightly slower due to file extraction

## :email: Contact

- **Author**: Your Name
- **Email**: your.email@example.com
- **GitHub**: [@futurefe414](https://github.com/futurefe414)

## :pray: Acknowledgments

- [Avalonia UI](https://avaloniaui.net/) - Cross-platform UI framework
- [NAudio](https://github.com/naudio/NAudio) - Audio playback library
- All contributors who have helped improve this project

---

:star: If you find this project useful, please consider giving it a star!
