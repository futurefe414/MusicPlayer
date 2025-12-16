# Music Player

[English](#english) | [中文](#中文)

---

## English

A simple and elegant music player built with Avalonia UI framework and .NET 8.

![Music Player Demo](demo.png)

### Features

- 🎵 **Audio Playback**: Play various audio formats (MP3, WAV, M4A, FLAC)
- ⭐ **Favorites Management**: Mark your favorite songs with a star
- 📝 **Song Management**: Add, edit, and delete songs from your library
- 📁 **File Browser**: Import audio files from your computer
- 💾 **Data Persistence**: Automatically saves your library and favorites
- 🎨 **Modern UI**: Clean and intuitive user interface with Fluent design
- 🎛️ **Playback Controls**: Play, pause, stop, previous, and next controls
- 🔄 **Reset Function**: Reset to default song list with confirmation dialog

### Technology Stack

- **Framework**: Avalonia UI 11.3.6
- **Runtime**: .NET 8.0
- **Audio Library**: NAudio 2.2.1
- **Data Storage**: System.Text.Json 8.0.0

### Project Structure

```
Music/
├── Assets/
│   ├── DefaultSongs/          # Default song files
│   └── logo.ico               # Application icon
├── Services/
│   ├── AudioPlayerService.cs  # Audio playback service
│   └── MusicDataService.cs    # Data persistence service
├── MainWindow.axaml           # Main window UI
├── MainWindow.axaml.cs        # Main window logic
├── ManageSongWindow.axaml     # Song management window UI
├── ManageSongWindow.axaml.cs  # Song management logic
├── ConfirmDialog.axaml        # Confirmation dialog UI
├── ConfirmDialog.axaml.cs     # Confirmation dialog logic
├── App.axaml                  # Application resources
├── App.axaml.cs               # Application entry
└── Music.csproj               # Project file
```

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/futurefe414/MusicPlayer.git
   cd Music
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run --project Music/Music.csproj
   ```

### Usage

#### Playing Music
1. Select a song from the list
2. Click the **Play** button or double-click on a song
3. Use playback controls to pause, stop, or navigate between songs

#### Managing Songs
- **Add Songs**: Click the **Browse** button to import audio files from your computer
- **Edit Song Info**: Click the **Edit** button (✏️) next to any song to modify its details
- **Delete Songs**: Click the **Delete** button (🗑️) to remove a song from the library
- **Manual Add**: Click the **Manage** button to manually add song information

#### Favorites
- Click the **Star** icon (⭐) next to any song to mark it as a favorite
- Click **Show Favorites** to filter and view only your favorite songs
- Click **Show All Songs** to return to the full library

#### Data Management
- **Open Data Folder**: Click to view the location where your library data is stored
- **Reset**: Restore the application to its default state with the original song list

### Data Storage

The application stores data in your system's AppData folder:
- **Windows**: `%AppData%\MusicPlayer\`
- **macOS**: `~/Library/Application Support/MusicPlayer/`
- **Linux**: `~/.config/MusicPlayer/`

Two JSON files are maintained:
- `songs.json`: Your complete song library
- `favorites.json`: List of favorited song IDs

### Supported Audio Formats

- MP3 (`.mp3`)
- WAV (`.wav`)
- M4A (`.m4a`)
- FLAC (`.flac`)

### License

This project is open source and available under the MIT License.

### Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

### Repository

[https://github.com/futurefe414/MusicPlayer](https://github.com/futurefe414/MusicPlayer)

---

## 中文

一个基于 Avalonia UI 框架和 .NET 8 构建的简洁优雅的音乐播放器。

![音乐播放器演示](demo.png)

### 功能特性

- 🎵 **音频播放**: 支持多种音频格式播放（MP3、WAV、M4A、FLAC）
- ⭐ **收藏管理**: 标记您喜爱的歌曲
- 📝 **歌曲管理**: 添加、编辑和删除歌曲库中的歌曲
- 📁 **文件浏览**: 从计算机导入音频文件
- 💾 **数据持久化**: 自动保存您的歌曲库和收藏
- 🎨 **现代化界面**: 简洁直观的用户界面，采用 Fluent 设计
- 🎛️ **播放控制**: 播放、暂停、停止、上一首、下一首控制
- 🔄 **重置功能**: 带确认对话框的重置到默认歌曲列表功能

### 技术栈

- **框架**: Avalonia UI 11.3.6
- **运行时**: .NET 8.0
- **音频库**: NAudio 2.2.1
- **数据存储**: System.Text.Json 8.0.0

### 项目结构

```
Music/
├── Assets/
│   ├── DefaultSongs/          # 默认歌曲文件
│   └── logo.ico               # 应用程序图标
├── Services/
│   ├── AudioPlayerService.cs  # 音频播放服务
│   └── MusicDataService.cs    # 数据持久化服务
├── MainWindow.axaml           # 主窗口 UI
├── MainWindow.axaml.cs        # 主窗口逻辑
├── ManageSongWindow.axaml     # 歌曲管理窗口 UI
├── ManageSongWindow.axaml.cs  # 歌曲管理逻辑
├── ConfirmDialog.axaml        # 确认对话框 UI
├── ConfirmDialog.axaml.cs     # 确认对话框逻辑
├── App.axaml                  # 应用程序资源
├── App.axaml.cs               # 应用程序入口
└── Music.csproj               # 项目文件
```

### 环境要求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0)

### 开始使用

1. **克隆仓库**
   ```bash
   git clone https://github.com/futurefe414/MusicPlayer.git
   cd Music
   ```

2. **还原依赖**
   ```bash
   dotnet restore
   ```

3. **构建项目**
   ```bash
   dotnet build
   ```

4. **运行应用程序**
   ```bash
   dotnet run --project Music/Music.csproj
   ```

### 使用说明

#### 播放音乐
1. 从列表中选择一首歌曲
2. 点击**播放**按钮或双击歌曲
3. 使用播放控制按钮暂停、停止或切换歌曲

#### 管理歌曲
- **添加歌曲**: 点击**浏览**按钮从计算机导入音频文件
- **编辑歌曲信息**: 点击歌曲旁边的**编辑**按钮（✏️）修改歌曲详细信息
- **删除歌曲**: 点击**删除**按钮（🗑️）从歌曲库中移除歌曲
- **手动添加**: 点击**管理**按钮手动添加歌曲信息

#### 收藏功能
- 点击歌曲旁边的**星标**图标（⭐）将其标记为收藏
- 点击**显示收藏**仅查看您收藏的歌曲
- 点击**显示全部歌曲**返回完整歌曲库

#### 数据管理
- **打开数据文件夹**: 点击查看存储歌曲库数据的位置
- **重置**: 将应用程序恢复到默认状态，使用原始歌曲列表

### 数据存储

应用程序将数据存储在系统的 AppData 文件夹中：
- **Windows**: `%AppData%\MusicPlayer\`
- **macOS**: `~/Library/Application Support/MusicPlayer/`
- **Linux**: `~/.config/MusicPlayer/`

维护两个 JSON 文件：
- `songs.json`: 完整的歌曲库
- `favorites.json`: 收藏的歌曲 ID 列表

### 支持的音频格式

- MP3 (`.mp3`)
- WAV (`.wav`)
- M4A (`.m4a`)
- FLAC (`.flac`)

### 许可证

本项目为开源项目，采用 MIT 许可证。

### 贡献

欢迎贡献！请随时提交 Pull Request。

### 代码仓库

[https://github.com/futurefe414/MusicPlayer](https://github.com/futurefe414/MusicPlayer)
