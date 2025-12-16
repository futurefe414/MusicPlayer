using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Music.Services
{
    public class MusicDataService
    {
        private readonly string _songsFilePath;
        private readonly string _favoritesFilePath;
        private readonly string _appDataPath;

        public MusicDataService()
        {
            _appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "MusicPlayer");
            
            Directory.CreateDirectory(_appDataPath);
            
            _songsFilePath = Path.Combine(_appDataPath, "songs.json");
            _favoritesFilePath = Path.Combine(_appDataPath, "favorites.json");
        }

        public async Task<List<Song>> LoadSongsAsync()
        {
            try
            {
                if (File.Exists(_songsFilePath))
                {
                    var json = await File.ReadAllTextAsync(_songsFilePath);
                    return JsonSerializer.Deserialize<List<Song>>(json) ?? new List<Song>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading songs: {ex.Message}");
            }
            
            return new List<Song>();
        }

        public async Task SaveSongsAsync(List<Song> songs)
        {
            try
            {
                var json = JsonSerializer.Serialize(songs, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
                await File.WriteAllTextAsync(_songsFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving songs: {ex.Message}");
            }
        }

        public async Task<List<string>> LoadFavoritesAsync()
        {
            try
            {
                if (File.Exists(_favoritesFilePath))
                {
                    var json = await File.ReadAllTextAsync(_favoritesFilePath);
                    return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading favorites: {ex.Message}");
            }
            
            return new List<string>();
        }

        public async Task SaveFavoritesAsync(List<string> favorites)
        {
            try
            {
                var json = JsonSerializer.Serialize(favorites, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
                await File.WriteAllTextAsync(_favoritesFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving favorites: {ex.Message}");
            }
        }

        public void DeleteAllData()
        {
            try
            {
                if (File.Exists(_songsFilePath))
                {
                    File.Delete(_songsFilePath);
                }
                if (File.Exists(_favoritesFilePath))
                {
                    File.Delete(_favoritesFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting data: {ex.Message}");
            }
        }

        public string GetDataPath()
        {
            return _appDataPath;
        }
    }
}
