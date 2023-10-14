using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "Configs/Hero")]
    public class HeroConfig : ScriptableObject
    {
        [Header("Stamina")]
        [SerializeField, Range(1, 10)] private float _maxStamina = 5;
        [SerializeField, Range(0, 10)] private float _staminaReducer = 1;

        [Header("Jump")]
        [SerializeField, Range(1f, 5f)] private float _JumpHeight = 1.0f;
        [SerializeField, Range(.1f, 2f)] private float _JumpDuration = .5f;

        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 2.0f;
        [SerializeField] private float _runSpeed = 4.0f;

        [Header("Dash")]
        [SerializeField, Range(5f, 100f)] private float _dashSpeed = 10f;
        [SerializeField, Range(0f, 1f)] private float _dashDuration = 1f;
        [SerializeField, Range(0f, 10f)] private float _dashCooldown = 1f;

        [Header("Attack")]
        [SerializeField, Range(0f, 1f)] private float _attackDuration = 1f;
        [SerializeField, Range(0f, 10f)] private float _attackCooldown = 1f;
        [SerializeField] private LayerMask _hurtLayerMask;

        public float MaxStamina => _maxStamina;
        public float StaminaReducer => _staminaReducer;

        public float JumpHeight => _JumpHeight;
        public float JumpDuration => _JumpDuration;

        public float MoveSpeed => _moveSpeed;
        public float RunSpeed => _runSpeed;

        public float DashSpeed => _dashSpeed;
        public float DashDuration => _dashDuration;
        public float DashCooldown => _dashCooldown;

        public float AttackDuration => _attackDuration;
        public float AttackCooldown => _attackCooldown;
        public LayerMask HurtLayerMask => _hurtLayerMask;
    }
}
