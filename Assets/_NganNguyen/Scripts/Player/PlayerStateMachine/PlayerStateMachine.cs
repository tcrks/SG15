
using Unity.VisualScripting;
using UnityEngine;
using SG15._NganNguyenInput;
using SG15._NganNguyenCore;
using SG15._NganNguyenPlayerAnimationEvent;
namespace SG15._NganNguyenStateMachine
{
    public class PlayerStateMachine : StateMachine
    {
        [field: SerializeField] public InputsReader InputReader { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Targeter Targeter { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
        [field: SerializeField] public PlayerAnimationEvent PlayerAnimationEvent { get; private set; }
        [field: SerializeField] public float RotationSmoothDamping { get; private set; }
        [field: SerializeField] public float FreeLockMovementSpeed { get; private set; }
        public Transform MainCameraTransform { get; private set; }
        public bool IsIntereacting { get; private set; } = false;
        public bool willLockToCamera { get; private set; } = false;
        private void Awake()
        {
            Targeter = transform.GetComponentInChildren<Targeter>();
            PlayerAnimationEvent = GetComponentInChildren<PlayerAnimationEvent>();
            InputReader = transform.AddComponent<InputsReader>();
            MainCameraTransform = Camera.main.transform;
            CharacterController = GetComponent<CharacterController>();
        }
        private void Start()
        {
            Animator = transform.GetComponentInChildren<Animator>();
            SwitchState(new PlayerFreelookState(this));
            ForceReceiver = GetComponent<ForceReceiver>();
        }
        public void SetIntereacting(bool value)
        {
            IsIntereacting = value;
        }
        public void SetWillLockToCameraView(bool willLockToCamera)
        {
            if (willLockToCamera)
            {
                SwitchState(new PlayerLockViewState(this));             
            }
            else
            {
                SwitchState(new PlayerFreelookState(this));
            }
            this.willLockToCamera = willLockToCamera;
        }
    }
}

