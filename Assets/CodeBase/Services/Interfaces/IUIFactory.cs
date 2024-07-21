using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.UI;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services.Interfaces
{
    public interface IUIFactory
    {
        Dictionary<PopUpId, PopUpBase> CreatedPopUps { get; }
        UniTask CreateUIRoot();
        void CreateAllPopUps();
    }
}