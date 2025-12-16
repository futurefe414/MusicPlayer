# 音乐播放器系统

[English](README.md) | [中文](README_CN.md)

一个使用 Avalonia UI 和 .NET 8 构建的简洁优雅的跨平台音乐播放器。

![音乐播放器系统](screenshot.png)

## :sparkles: 功能特性

- :musical_note: **音频播放** - 支持多种音频格式（MP3、WAV、M4A、FLAC）
- :star: **收藏管理** - 标记您喜欢的歌曲以便快速访问
- :memo: **歌曲库** - 浏览、添加、编辑和删除歌曲
- :floppy_disk: **数据持久化** - 所有歌曲和收藏自动保存
- :art: **现代化界面** - 简洁直观的 Fluent 设计风格
- :file_folder: **文件浏览器** - 从计算机导入音频文件
- :arrows_counterclockwise: **重置功能** - 随时恢复到默认歌曲列表

## :computer: 系统要求

- **操作系统**: Windows 10 或更高版本（64 位）
- **框架**: .NET 8.0 运行时（自包含版本已内置）
- **内存**: 最少 512 MB RAM
- **存储空间**: 100 MB 可用空间

## :package: 安装方法

### 方式一：下载发布版本（推荐）

1. 从 [Releases](https://github.com/futurefe414/MusicPlayer/releases) 页面下载最新版本
2. 将 ZIP 文件解压到您喜欢的位置
3. 运行 `Music.exe` 启动应用程序

### 方式二：从源码构建

**前置要求：**
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 或 JetBrains Rider（可选）

**步骤：**

```bash
# 克隆仓库
git clone https://github.com/futurefe414/MusicPlayer.git
cd MusicPlayer/Music

# 还原依赖项
dotnet restore

# 构建项目
dotnet build -c Release

# 运行应用程序
dotnet run
```

## :rocket: 发布打包

创建独立可执行文件：

```bash
# 导航到项目目录
cd Music

# 发布 Windows x64 版本
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true

# 输出文件位于: bin\Release\net8.0\win-x64\publish\
```

## :book: 使用说明

### 播放音乐

1. **选择歌曲** - 在歌曲列表中点击一首歌
2. **点击播放按钮**（▶️）或使用操作列中的播放按钮
3. 使用控制按钮：
   - ⏮️ **上一首** - 播放上一首歌曲
   - ▶️ **播放** - 播放或恢复播放
   - ⏸️ **暂停** - 暂停当前歌曲
   - ⏹️ **停止** - 停止播放
   - ⏭️ **下一首** - 播放下一首歌曲

### 管理曲库

#### 添加歌曲

**方法一：浏览文件**
1. 点击 **:file_folder: Browse Audio Files**
2. 选择一个或多个音频文件
3. 文件将自动添加到您的曲库

**方法二：手动输入**
1. 点击 **:heavy_plus_sign: Add New Song**
2. 填写歌曲详细信息：
   - 歌曲名称
   - 歌手
   - 专辑
   - 示例歌词（可选）
   - 文件路径
3. 点击 **保存**

#### 编辑歌曲

1. 点击操作列中的 **:pencil2: 编辑** 按钮
2. 修改歌曲信息
3. 点击 **保存** 应用更改

#### 删除歌曲

1. 点击操作列中的 **:x: 删除** 按钮
2. 歌曲将从您的曲库中移除

### 收藏功能

- 点击操作列中的 **:heart: 心形图标** 来添加/移除收藏
- 点击 **:star: Show Favorites** 筛选并查看收藏的歌曲
- 再次点击显示所有歌曲

### 数据管理

- **:file_folder: Open Data Folder** - 打开数据存储文件夹
  - 位置：`%AppData%\MusicPlayer\`
  - 包含：`songs.json` 和 `favorites.json`

- **:arrows_counterclockwise: Reset to Default** - 恢复到默认歌曲列表
  - :warning: 警告：这将删除所有用户添加的歌曲和收藏

## :wrench: 技术栈

- **框架**: [.NET 8.0](https://dotnet.microsoft.com/)
- **UI 框架**: [Avalonia UI 11.3.6](https://avaloniaui.net/)
- **音频引擎**: [NAudio 2.2.1](https://github.com/naudio/NAudio)
- **数据序列化**: System.Text.Json
- **架构模式**: MVVM 模式与代码后置

## :file_folder: 项目结构

```
Music/
├── Assets/                      # 应用程序资源
│   ├── DefaultSongs/           # 默认音乐文件
│   └── logo.ico                # 应用程序图标
├── Services/
│   ├── AudioPlayerService.cs   # 音频播放逻辑
│   └── MusicDataService.cs     # 数据持久化逻辑
├── MainWindow.axaml            # 主窗口界面
├── MainWindow.axaml.cs         # 主窗口逻辑
├── ManageSongWindow.axaml      # 歌曲管理对话框
├── ManageSongWindow.axaml.cs   # 歌曲管理逻辑
├── ConfirmDialog.axaml         # 确认对话框界面
├── ConfirmDialog.axaml.cs      # 确认对话框逻辑
├── App.axaml                   # 应用程序样式
├── App.axaml.cs                # 应用程序入口点
└── Music.csproj                # 项目配置文件
```

## :gear: 配置说明

### 默认歌曲

应用程序预置了 5 首中文歌曲。您可以通过以下方式修改：

1. 编辑 `MainWindow.axaml.cs` 中的默认歌曲（第 ~52 行）
2. 替换 `Assets/DefaultSongs/` 中的 MP3 文件
3. 重新构建项目

### 数据存储

用户数据存储在：
```
Windows: C:\Users\[用户名]\AppData\Roaming\MusicPlayer\
```

文件：
- `songs.json` - 您的歌曲库
- `favorites.json` - 您的收藏列表

## :handshake: 贡献

欢迎贡献！请随时提交 Pull Request。

1. Fork 本项目
2. 创建您的功能分支 (`git checkout -b feature/AmazingFeature`)
3. 提交您的更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 开启一个 Pull Request

## :memo: 许可证

本项目采用 MIT 许可证 - 详见 [LICENSE](LICENSE) 文件。

## :bug: 已知问题

- NAudio 仅支持 Windows 平台进行音频播放
- 首次启动可能稍慢，因为需要解压文件

## :email: 联系方式

- **作者**: Your Name
- **邮箱**: your.email@example.com
- **GitHub**: [@futurefe414](https://github.com/futurefe414)

## :pray: 致谢

- [Avalonia UI](https://avaloniaui.net/) - 跨平台 UI 框架
- [NAudio](https://github.com/naudio/NAudio) - 音频播放库
- 所有帮助改进本项目的贡献者

---

:star: 如果您觉得这个项目有用，请考虑给它一个星标！star~~
