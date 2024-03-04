using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG15._NganNguyenStateMachine
{

    public class PlayerLockViewState : PlayerBaseState
    {
        private readonly int moveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int lockViewBlendTreeHash = Animator.StringToHash("Movement_LockView");
        private readonly int lockViewForwardHash = Animator.StringToHash("LockViewForward");
        private readonly int lockViewRightHash = Animator.StringToHash("LockViewRight");
        private const float animatorDampTime = 0.1f;
        private const float crossFadeDuration = 0.3f;
        private float movespeed;
        Vector2 movementInput;
        public PlayerLockViewState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {

        }

        public override void Enter()
        {
            playerStateMachine.Animator.CrossFadeInFixedTime(lockViewBlendTreeHash, crossFadeDuration);
        }
        public override void Tick(float deltaTime)
        {          
            MovementExcute(deltaTime);
            UpdateAnimator(deltaTime);
        }
        public override void Exit()
        {
           
        }
        private void MovementExcute(float deltaTime)
        {
            movementInput = playerStateMachine.InputReader.MovementValue;
            Vector3 movement;
            movespeed = 0.35f;
            if (isMovingToTarget)
            {
                movement = CaculateMovementToTarget(true);
                movement = movement * 0.5f;
                if (Vector3.Distance(playerStateMachine.transform.position, playerStateMachine.Targeter.CurrentTarget.transform.position)
                    < playerStateMachine.Targeter.CurrentTarget.DistanceToinvestigate)
                {
                    playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
                }              
            }
            else
            {
                movement = CaculateMovementToTarget();
                if (playerStateMachine.InputReader.IsRunning)
                {
                    movespeed = 1;
                }               
            }
            Move(movement * playerStateMachine.FreeLockMovementSpeed * movespeed, deltaTime);      
            FaceMovementDirection(movement, deltaTime);
        }
        private void UpdateAnimator(float deltaTime)
        {
           
            if (movementInput == Vector2.zero && !isMovingToTarget)
            {
                playerStateMachine.Animator.SetFloat(lockViewRightHash, 0f, animatorDampTime, deltaTime);
                playerStateMachine.Animator.SetFloat(lockViewForwardHash, 0f, animatorDampTime, deltaTime);
                playerStateMachine.Animator.SetFloat(moveSpeedHash, 0f, animatorDampTime, deltaTime);
                return;
            }
            else
            {
                float moveSpeed;
                if (movementInput.x != 0)
                {
                    moveSpeed = 0.5f;
                }
                else
                {
                    moveSpeed = playerStateMachine.InputReader.IsRunning is true ? 1 : 0.5f;
                }
                playerStateMachine.Animator.SetFloat(moveSpeedHash, moveSpeed, animatorDampTime, deltaTime);
                if(movementInput.x==0)
                {
                    playerStateMachine.Animator.SetFloat(lockViewRightHash, 0f, animatorDampTime, deltaTime);
                }
                else
                {
                    float value = movementInput.x > 0 ? 1 : -1;
                    playerStateMachine.Animator.SetFloat(lockViewRightHash, value, animatorDampTime, deltaTime);
                }
                if (movementInput.y == 0)
                {
                    playerStateMachine.Animator.SetFloat(lockViewForwardHash, 0f, animatorDampTime, deltaTime);
                }
                else
                {
                    float value = movementInput.y > 0 ? 1 : -1;
                    playerStateMachine.Animator.SetFloat(lockViewForwardHash, value, animatorDampTime, deltaTime);
                }                      
            }

        }
    }
}
