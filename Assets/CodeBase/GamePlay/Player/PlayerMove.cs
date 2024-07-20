using CodeBase.Configs;
using CodeBase.Services.Interfaces;
using CodeBase.Utility;
using UnityEngine;
using Zenject;

namespace CodeBase.GamePlay.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private PlayerConfig _playerConfig;
        private IInputService _inputService;
        private bool _isInitialized;
        private Camera _camera;
        private MovementRestriction _movementRestriction;

        [Inject]
        private async void Construct(IStaticData staticData, IInputService inputService)
        {
            _inputService = inputService;
            _camera = Camera.main;
            
            _playerConfig = await staticData.GetPlayerStaticData();
            WorldConfig worldConfig = await staticData.GetWorldStaticData();

            _movementRestriction = new MovementRestriction(_camera, worldConfig.FinishLineYPosition);
            
            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized)
                return;
            
            if (_inputService.Axis.magnitude <= Constants.Epsilon)
                return;

            Vector3 movementVector = _camera.transform.TransformDirection(_inputService.Axis);
            movementVector.Normalize();

            transform.position += movementVector * _playerConfig.PlayerMovementSpeed * Time.deltaTime;
            transform.position = _movementRestriction.ClampPosition(transform.position);
        }
    }
}