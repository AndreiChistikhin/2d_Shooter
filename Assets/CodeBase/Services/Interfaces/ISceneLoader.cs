using System;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services.Interfaces
{
    public interface ISceneLoader
    {
        UniTaskVoid LoadScene(string sceneName, Action onLoaded = null);
    }
}