using System.Threading.Tasks;
using UnityEngine;

namespace Module.App.Scripts.Services
{
    public interface IAvatarCacheService
    {
        Task<Texture2D> GetAvatarAsync(string url);
        void ClearCache();
    }
}