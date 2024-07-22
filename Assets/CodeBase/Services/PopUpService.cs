using System;
using CodeBase.Configs;
using CodeBase.Services.Interfaces;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Services
{
    public class PopUpService : IPopUpService
    {
        private readonly IUIFactory _uiFactory;

        public event Action OnPopUp;
        
        public PopUpService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            CreateAllPopUps();
        }

        public void Open(PopUpId popUpId) =>
            TryToShowPopUp(popUpId);

        private void CreateAllPopUps() =>
            _uiFactory.CreateAllPopUps();

        private void TryToShowPopUp(PopUpId popUpId)
        {
            bool popUpFound = _uiFactory.CreatedPopUps.TryGetValue(popUpId, out PopUpBase popUp);

            if (!popUpFound)
            {
                Debug.LogError("В UI фабирке не создан тот поп ап, который хочешь открыть");
                return;
            }
            
            popUp.Show();
            OnPopUp?.Invoke();
        }
    }
}