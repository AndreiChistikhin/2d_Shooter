using System;
using CodeBase.Configs;

namespace CodeBase.Services.Interfaces
{
    public interface IPopUpService
    {
        void Open(PopUpId popUpId);
        event Action OnPopUp;
    }
}