using System;
using CodeBase.Architecture;
using CodeBase.Services.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services
{
    public class SceneLoader : ISceneLoader
    {
        private LoadingCurtain _loadingCurtain;
        
        public SceneLoader(LoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
        }

        public async UniTaskVoid LoadScene(string sceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                return;
            }

            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);
            _loadingCurtain.Show();
        
            await loadSceneAsync.ToUniTask();
        
            _loadingCurtain.Hide().Forget();
            onLoaded?.Invoke();
        }
    }
}