using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace SG15._NganNguyenStateMachine
{
    public class PlayerFreelookState : PlayerBaseState
    {
        private bool shouldFade;
        private readonly int freeLookSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int freeLookBlendTreeHash = Animator.StringToHash("Movement_freeView");
        private readonly int cameraLockBlendTreeHash = Animator.StringToHash("Movement_LockView");
        private const float animatorDampTime = 0.1f;
        private const float crossFadeDuration = 0.1f;
        public PlayerFreelookState(PlayerStateMachine playerStateMachine, bool shouldFade = true) : base(playerStateMachine)
        {
            this.shouldFade = shouldFade;
        }

        public override void Enter()
        {
            //playerStateMachine.InputReader.TargetEvent += MoveToTarget;
            playerStateMachine.InputReader.EquipLightEvent += EquipLightHandle;
            //playerStateMachine.InputReader.CancelTargetEvent += OnCancel;
            /*playerStateMachine.InputReader.JumpEvent += OnJump;*/
            playerStateMachine.Animator.SetFloat(freeLookSpeedHash, 0);          
                if (shouldFade)
                {
                    playerStateMachine.Animator.CrossFadeInFixedTime(freeLookBlendTreeHash, crossFadeDuration);
                }
                else
                {
                    playerStateMachine.Animator.Play(freeLookBlendTreeHash);
                }        
        }

        public override void Tick(float deltaTime)
        {
            MovementExcute(deltaTime);
        }

        private void MovementExcute(float deltaTime)
        {
            float movespeed;
            Vector3 movement;
            if (isMovingToTarget)
            {
                movement = CaculateMovementToTarget(true);
                movement = movement * 0.5f;
                if (Vector3.Distance(playerStateMachine.transform.position, playerStateMachine.Targeter.CurrentTarget.transform.position)
                    < playerStateMachine.Targeter.CurrentTarget.DistanceToinvestigate)
                {
                    playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
                }
                movespeed = 0.5f;
            }
            else
            {
                movement = CaculateMovementToTarget();
                if (playerStateMachine.InputReader.IsRunning)
                {
                    movespeed = 1;
                }
                else
                    movespeed = 0.5f;
            }
            Move(movement * playerStateMachine.FreeLockMovementSpeed * movespeed, deltaTime);

            if (playerStateMachine.InputReader.MovementValue == Vector2.zero && !isMovingToTarget)
            {
                playerStateMachine.Animator.SetFloat(freeLookSpeedHash, 0, animatorDampTime, deltaTime);
                return;
            }
            playerStateMachine.Animator.SetFloat(freeLookSpeedHash, 1 * movespeed, animatorDampTime, deltaTime);
            FaceMovementDirection(movement, deltaTime);
        }

        public override void Exit()
        {
            //playerStateMachine.InputReader.TargetEvent -= MoveToTarget;
            playerStateMachine.InputReader.EquipLightEvent += EquipLightHandle;
            //playerStateMachine.InputReader.CancelTargetEvent -= OnCancel;
        }

        
    }
}