using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System.Collections.Generic;
using System.Linq;

namespace Music
{
    public partial class ManageSongWindow : Window
    {
        public Song? EditingSong { get; private set; }
        public bool IsSaved { get; private set; }

        public ManageSongWindow()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        public ManageSongWindow(Song song) : this()
        {
            EditingSong = song;
            LoadSongData(song);
        }

        private void SetupEventHandlers()
        {
            SaveButton.Click += SaveButton_Click;
            CancelButton.Click += CancelButton_Click;
            BrowseFileButton.Click += BrowseFileButton_Click;
        }

        private void LoadSongData(Song song)
        {
            IdTextBox.Text = song.Id;
            NameTextBox.Text = song.Name;
            ArtistTextBox.Text = song.Artist;
            AlbumTextBox.Text = song.Album;
            ExampleTextBox.Text = song.Example;
            FilePathTextBox.Text = song.FilePath;
        }

        private async void BrowseFileButton_Click(object? sender, RoutedEventArgs e)
        {
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel == null) return;

            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select Audio File",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("Audio Files")
                    {
                        Patterns = new[] { "*.wav", "*.mp3", "*.m4a", "*.flac" }
                    },
                    new FilePickerFileType("All Files")
                    {
                        Patterns = new[] { "*.*" }
                    }
                }
            });

            if (files.Any())
            {
                FilePathTextBox.Text = files[0].Path.LocalPath;
            }
        }

        private void SaveButton_Click(object? sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IdTextBox.Text) ||
                string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                return;
            }

            if (EditingSong == null)
            {
                EditingSong = new Song();
            }

            EditingSong.Id = IdTextBox.Text;
            EditingSong.Name = NameTextBox.Text;
            EditingSong.Artist = ArtistTextBox.Text ?? string.Empty;
            EditingSong.Album = AlbumTextBox.Text ?? string.Empty;
            EditingSong.Example = ExampleTextBox.Text ?? string.Empty;
            EditingSong.FilePath = FilePathTextBox.Text ?? string.Empty;

            IsSaved = true;
            Close();
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            IsSaved = false;
            Close();
        }
    }
}
