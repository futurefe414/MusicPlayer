using Avalonia.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using Music.Services;
using System;
using Avalonia.Interactivity;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System.Globalization;
using System.IO;

namespace Music
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Song> Songs { get; set; }
        private readonly MusicDataService _dataService;
        private readonly AudioPlayerService _audioPlayer;
        private List<string> _favorites;
        private Song? _currentPlayingSong;
        private bool _showingFavoritesOnly = false;
        private List<Song> _allSongs;

        public MainWindow()
        {
            InitializeComponent();
            _dataService = new MusicDataService();
            _audioPlayer = new AudioPlayerService();
            _favorites = new List<string>();
            Songs = new ObservableCollection<Song>();
            _allSongs = new List<Song>();
            
            InitializeSampleData();
            SetupEventHandlers();
        }

        private async void InitializeSampleData()
        {
            var savedSongs = await _dataService.LoadSongsAsync();
            
            if (savedSongs.Any())
            {
                foreach (var song in savedSongs)
                {
                    Songs.Add(song);
                }
            }
            else
            {
                var defaultSongs = new List<Song>
                {
                  
                    new Song { Id = "01", Name = "小苹果", Artist = "筷子兄弟", Album = "小苹果（新年版）", Example = "你是我的小呀小苹果", FilePath = Path.Combine(AppContext.BaseDirectory, "Assets", "DefaultSongs", "song1.mp3") },
                    new Song { Id = "02", Name = "告白气球", Artist = "周杰伦", Album = "周杰伦的床边故事", Example = "塞纳河畔左岸的咖啡", FilePath = Path.Combine(AppContext.BaseDirectory, "Assets", "DefaultSongs", "song2.mp3")},
                    new Song { Id = "03", Name = "演员", Artist = "薛之谦", Album = "绅士", Example = "简单点说话的方式简单点", FilePath = Path.Combine(AppContext.BaseDirectory, "Assets", "DefaultSongs", "song3.mp3")},
                    new Song { Id = "04", Name = "体面", Artist = "于文文", Album = "体面", Example = "分手应该体面", FilePath = Path.Combine(AppContext.BaseDirectory, "Assets", "DefaultSongs", "song4.mp3") },
                    new Song { Id = "05", Name = "董小姐", Artist = "宋冬野", Album = "安和桥北", Example = "董小姐", FilePath = Path.Combine(AppContext.BaseDirectory, "Assets", "DefaultSongs", "song5.mp3")}
                };
                
                foreach (var song in defaultSongs)
                {
                    Songs.Add(song);
                }
            }

            _favorites = await _dataService.LoadFavoritesAsync();
            foreach (var song in Songs)
            {
                song.IsFavorite = _favorites.Contains(song.Id);
            }

            _allSongs = Songs.ToList();
            SongListBox.ItemsSource = Songs;
            UpdateStatus();
        }

        private void SetupEventHandlers()
        {
            SongListBox.SelectionChanged += (s, e) =>
            {
                if (SongListBox.SelectedItem is Song song)
                {
                    UpdateCurrentSongDisplay(song);
                }
            };

            PlayPauseButton.Click += PlayPauseButton_Click;
            PauseButton.Click += PauseButton_Click;
            StopButton.Click += StopButton_Click;
            PreviousButton.Click += (s, e) => SelectPreviousSong();
            NextButton.Click += (s, e) => SelectNextSong();
            BrowseButton.Click += BrowseButton_Click;
            ManageButton.Click += ManageButton_Click;
            ShowFavoritesButton.Click += ShowFavoritesButton_Click;
            OpenDataFolderButton.Click += OpenDataFolderButton_Click;
            ResetButton.Click += ResetButton_Click;
        }

        private async void PlayPauseButton_Click(object? sender, RoutedEventArgs e)
        {
            if (SongListBox.SelectedItem is Song song)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(song.FilePath))
                    {
                        StatusText.Text = "错误: 该歌曲未指定音频文件";
                        return;
                    }

                    if (_audioPlayer.IsPlaying && _currentPlayingSong == song)
                    {
                        _audioPlayer.Pause();
                        StatusText.Text = "已暂停...";
                    }
                    else if (_audioPlayer.IsPaused && _currentPlayingSong == song)
                    {
                        _audioPlayer.Resume();
                        StatusText.Text = $"正在播放: {song.Name}";
                    }
                    else
                    {
                        _audioPlayer.Play(song.FilePath);
                        _currentPlayingSong = song;
                        StatusText.Text = $"正在播放: {song.Name}";
                    }
                }
                catch (Exception ex)
                {
                    StatusText.Text = $"错误: {ex.Message}";
                }
            }
            else
            {
                StatusText.Text = "请先选择一首歌曲";
            }
        }

        private void PauseButton_Click(object? sender, RoutedEventArgs e)
        {
            if (_audioPlayer.IsPlaying)
            {
                _audioPlayer.Pause();
                StatusText.Text = "已暂停";
            }
        }

        private void StopButton_Click(object? sender, RoutedEventArgs e)
        {
            _audioPlayer.Stop();
            _currentPlayingSong = null;
            StatusText.Text = "已停止";
        }

        private async void BrowseButton_Click(object? sender, RoutedEventArgs e)
        {
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel == null) return;

            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
            {
                Title = "选择音频文件",
                AllowMultiple = true,
                FileTypeFilter = new[]
                {
                    new Avalonia.Platform.Storage.FilePickerFileType("音频文件")
                    {
                        Patterns = new[] { "*.wav", "*.mp3", "*.m4a", "*.flac" }
                    }
                }
            });

            if (files.Any())
            {
                foreach (var file in files)
                {
                    var fileName = System.IO.Path.GetFileNameWithoutExtension(file.Name);
                    var newSong = new Song
                    {
                        Id = (_allSongs.Count + 1).ToString("D2"),
                        Name = fileName,
                        Artist = "未知",
                        Album = "未知",
                        Example = "",
                        FilePath = file.Path.LocalPath
                    };
                    Songs.Add(newSong);
                    _allSongs.Add(newSong);
                }
                
                await SaveAllSongsAsync();
                UpdateStatus();
                StatusText.Text = $"已添加 {files.Count} 首歌曲";
            }
        }

        private async void ManageButton_Click(object? sender, RoutedEventArgs e)
        {
            var manageWindow = new ManageSongWindow();
            await manageWindow.ShowDialog(this);
            
            if (manageWindow.IsSaved && manageWindow.EditingSong != null)
            {
                Songs.Add(manageWindow.EditingSong);
                _allSongs.Add(manageWindow.EditingSong);
                await SaveAllSongsAsync();
                UpdateStatus();
                StatusText.Text = "歌曲添加成功";
            }
        }

        private void ShowFavoritesButton_Click(object? sender, RoutedEventArgs e)
        {
            _showingFavoritesOnly = !_showingFavoritesOnly;
            
            if (_showingFavoritesOnly)
            {
                Songs.Clear();
                foreach (var song in _allSongs.Where(s => s.IsFavorite))
                {
                    Songs.Add(song);
                }
                ShowFavoritesButton.Content = "\u2B50 显示全部歌曲";
                StatusText.Text = $"显示收藏 | 歌曲数: {Songs.Count}";
            }
            else
            {
                Songs.Clear();
                foreach (var song in _allSongs)
                {
                    Songs.Add(song);
                }
                ShowFavoritesButton.Content = "\u2B50 显示收藏";
                UpdateStatus();
            }
        }

        private async void PlaySongButton_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Song song)
            {
                SongListBox.SelectedItem = song;
                await System.Threading.Tasks.Task.Delay(100);
                PlayPauseButton_Click(sender, e);
            }
        }

        private async void ToggleFavoriteButton_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Song song)
            {
                song.IsFavorite = !song.IsFavorite;
                
                if (song.IsFavorite)
                {
                    if (!_favorites.Contains(song.Id))
                    {
                        _favorites.Add(song.Id);
                    }
                }
                else
                {
                    _favorites.Remove(song.Id);
                }
                
                await _dataService.SaveFavoritesAsync(_favorites);
                
                var index = Songs.IndexOf(song);
                Songs.Remove(song);
                Songs.Insert(index, song);
                
                UpdateStatus();
            }
        }

        private async void EditSongButton_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Song song)
            {
                var editWindow = new ManageSongWindow(song);
                await editWindow.ShowDialog(this);
                
                if (editWindow.IsSaved)
                {
                    var index = Songs.IndexOf(song);
                    Songs.Remove(song);
                    Songs.Insert(index, song);
                    
                    await SaveAllSongsAsync();
                    StatusText.Text = "歌曲更新成功";
                }
            }
        }

        private async void DeleteSongButton_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Song song)
            {
                Songs.Remove(song);
                _allSongs.Remove(song);
                _favorites.Remove(song.Id);
                
                await SaveAllSongsAsync();
                await _dataService.SaveFavoritesAsync(_favorites);
                
                UpdateStatus();
                StatusText.Text = $"已删除: {song.Name}";
            }
        }

        private void UpdateCurrentSongDisplay(Song song)
        {
            CurrentSongName.Text = song.Name;
            CurrentArtist.Text = $"歌手: {song.Artist}";
            CurrentAlbum.Text = $"专辑: {song.Album}";
            CurrentExample.Text = $"示例热词: {song.Example}";
        }

        private void UpdateStatus()
        {
            StatusText.Text = $" 全部: {_allSongs.Count} | 收藏: {_favorites.Count}";
        }

        private void SelectPreviousSong()
        {
            if (SongListBox.SelectedIndex > 0)
            {
                SongListBox.SelectedIndex--;
            }
        }

        private void SelectNextSong()
        {
            if (SongListBox.SelectedIndex < Songs.Count - 1)
            {
                SongListBox.SelectedIndex++;
            }
        }

        private async System.Threading.Tasks.Task SaveAllSongsAsync()
        {
            await _dataService.SaveSongsAsync(_allSongs);
        }

        private void OpenDataFolderButton_Click(object? sender, RoutedEventArgs e)
        {
            try
            {
                var dataPath = _dataService.GetDataPath();
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = dataPath,
                    UseShellExecute = true
                });
                StatusText.Text = $"已打开数据文件夹: {dataPath}";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"无法打开文件夹: {ex.Message}";
            }
        }

        private async void ResetButton_Click(object? sender, RoutedEventArgs e)
        {
            var dialog = new ConfirmDialog(
                "Confirm Reset",
                "Are you sure you want to reset to the default song list? This will delete all user-added songs and favorites data."
            );
            
            await dialog.ShowDialog(this);
            
            if (dialog.Result)
            {
                _audioPlayer.Stop();
                _currentPlayingSong = null;
                
                _dataService.DeleteAllData();
                
                Songs.Clear();
                _allSongs.Clear();
                _favorites.Clear();
                _showingFavoritesOnly = false;
                ShowFavoritesButton.Content = "\u2B50 显示收藏";
                
                var defaultSongs = new List<Song>
                {
                    new Song { Id = "01", Name = "小苹果", Artist = "筷子兄弟", Album = "小苹果（新年版）", Example = "你是我的小呀小苹果", FilePath = Path.Combine(AppContext.BaseDirectory, "Assets", "DefaultSongs", "song1.mp3") },
                    new Song { Id = "02", Name = "告白气球", Artist = "周杰伦", Album = "周杰伦的床边故事", Example = "塞纳河畔左岸的咖啡", FilePath = Path.Combine(AppContext.BaseDirectory, "Assets", "DefaultSongs", "song2.mp3")},
                    new Song { Id = "03", Name = "演员", Artist = "薛之谦", Album = "绅士", Example = "简单点说话的方式简单点", FilePath = Path.Combine(AppContext.BaseDirectory, "Assets", "DefaultSongs", "song3.mp3")},
                    new Song { Id = "04", Name = "体面", Artist = "于文文", Album = "体面", Example = "分手应该体面", FilePath = Path.Combine(AppContext.BaseDirectory, "Assets", "DefaultSongs", "song4.mp3") },
                    new Song { Id = "05", Name = "董小姐", Artist = "宋冬野", Album = "安和桥北", Example = "董小姐", FilePath = Path.Combine(AppContext.BaseDirectory, "Assets", "DefaultSongs", "song5.mp3")}
                };
                
                foreach (var song in defaultSongs)
                {
                    Songs.Add(song);
                    _allSongs.Add(song);
                }
                
                await SaveAllSongsAsync();
                
                UpdateStatus();
                StatusText.Text = "已重置到默认歌曲列表";
            }
        }

        protected override void OnClosing(WindowClosingEventArgs e)
        {
            _audioPlayer.Dispose();
            base.OnClosing(e);
        }
    }

    public class Song
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public string Example { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }
    }

    public class FavoriteColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isFavorite)
            {
                return isFavorite ? "#FF5722" : "#9E9E9E";
            }
            return "#9E9E9E";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}