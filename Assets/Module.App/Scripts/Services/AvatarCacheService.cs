using Module.Common.Scripts;

namespace Module.App.Scripts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Networking;

    public class AvatarCacheService : IAvatarCacheService
    {
        private readonly Dictionary<string, Texture2D> _cache = new();

        private readonly string _cacheFolder = GlobalConstants.Resources.AVATAR_CACHE_FOLDER_PATH;

        /// <summary>
        /// Gets the Avatar by url. If the avatar is not cached in memory -> tries to load from disk. If not cached in disk -> loads by Web request
        /// </summary>
        /// <param name="url">Url of the avatar</param>
        /// <returns></returns>
        public async Task<Texture2D> GetAvatarAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            if (_cache.TryGetValue(url, out var cachedTexture)) return cachedTexture;

            var diskTexture = await LoadFromDiskAsync(url);
            if (diskTexture != null)
            {
                _cache[url] = diskTexture;
                return diskTexture;
            }

            var downloadedTexture = await DownloadTextureAsync(url);
            if (downloadedTexture != null)
            {
                _cache[url] = downloadedTexture;
                await SaveToDiskAsync(url, downloadedTexture);
            }

            return downloadedTexture;
        }

        private async Task<Texture2D> DownloadTextureAsync(string url)
        {
            using var www = UnityWebRequestTexture.GetTexture(url);
            var asyncOp = www.SendWebRequest();

            while (!asyncOp.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Avatar download failed: {url} | {www.error}");
                return null;
            }

            var texture = DownloadHandlerTexture.GetContent(www);
            texture.name = System.IO.Path.GetFileName(url);
            return texture;
        }

        private async Task<Texture2D> LoadFromDiskAsync(string url)
        {
            var filePath = GetCacheFilePath(url);
            if (!System.IO.File.Exists(filePath))
                return null;

            try
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                if (texture.LoadImage(bytes))
                {
                    texture.name = System.IO.Path.GetFileName(url);
                    return texture;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load cached avatar: {url} | {ex.Message}");
            }

            return null;
        }

        private async Task SaveToDiskAsync(string url, Texture2D texture)
        {
            try
            {
                var filePath = GetCacheFilePath(url);
                var directory = System.IO.Path.GetDirectoryName(filePath);
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }

                var pngData = texture.EncodeToPNG();
                await System.IO.File.WriteAllBytesAsync(filePath, pngData);
                Debug.Log($"Saved avatar cache to {filePath}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save avatar cache: {url} | {ex.Message}");
            }
        }

        private string GetCacheFilePath(string url)
        {
            var fileName = HashUrl(url) + ".png";
            return System.IO.Path.Combine(Application.persistentDataPath, _cacheFolder, fileName);
        }

        private string HashUrl(string url)
        {
            using var sha1 = System.Security.Cryptography.SHA1.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(url);
            var hash = sha1.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public void ClearCache()
        {
            _cache.Clear();

            var cacheDir = System.IO.Path.Combine(Application.persistentDataPath, _cacheFolder);
            if (System.IO.Directory.Exists(cacheDir))
                System.IO.Directory.Delete(cacheDir, true);
        }
    }
}