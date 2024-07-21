using System;
using UnityEngine;

namespace CodeBase.GamePlay.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerObserver : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _circleCollider2D;

        public CircleCollider2D CircleCollider2D => _circleCollider2D;
        
        public event Action<Collider2D> OnTriggerEnter;
        public event Action<Collider2D> OnTriggerExit;
        
        private void OnTriggerEnter2D(Collider2D col) =>
            OnTriggerEnter?.Invoke(col);

        private void OnTriggerExit2D(Collider2D col) =>
            OnTriggerExit?.Invoke(col);
    }
}