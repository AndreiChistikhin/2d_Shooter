using CodeBase.Services.Interfaces;
using UnityEngine;

namespace CodeBase.Services
{
    public class InputService : IInputService
    {
        public Vector2 Axis => _playerInput.Player.Move.ReadValue<Vector2>();

        private PlayerControl _playerInput;
        
        public InputService()
        {
            _playerInput = new PlayerControl();
            _playerInput.Enable();
        }
    }
}