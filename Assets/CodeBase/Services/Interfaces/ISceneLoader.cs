using System;
using CodeBase.Architecture;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services.Interfaces
{
    public interface ISceneLoader
    {
        UniTaskVoid LoadScene(string sceneName, Action onLoaded = null);
        LoadingCurtain LoadingCurtain { get; }
    }
}