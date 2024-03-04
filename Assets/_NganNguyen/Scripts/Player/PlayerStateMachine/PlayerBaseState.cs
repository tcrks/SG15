using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG15._NganNguyenStateMachine;

namespace SG15._NganNguyenStateMachine
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerStateMachine playerStateMachine;
        protected bool isMovingToTarget = false;
        public PlayerBaseState(PlayerStateMachine playerStateMachine)
        {
            this.playerStateMachine = playerStateMachine;
        }
        protected void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
        }
        protected void Move(Vector3 motion, float deltaTime)
        {
            playerStateMachine.CharacterController.Move((playerStateMachine.ForceReceiver.Movement + motion) * deltaTime);
        }
        protected void FaceTarget()
        {
            if (playerStateMachine.Targeter.CurrentTarget == null) { return; }
            Vector3 lookPos = playerStateMachine.Targeter.CurrentTarget.transform.position -
                playerStateMachine.transform.position;
            lookPos.y = 0f;
            playerStateMachine.transform.GetChild(0).GetChild(0).rotation = Quaternion.LookRotation(lookPos);
        }
        protected void ReturnToLocomotion()
        {
            if (playerStateMachine.Targeter.CurrentTarget != null)
            {
                playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
            }
            else
            {
                playerStateMachine.SwitchState(new PlayerFreelookState(playerStateMachine));
            }
        }
        protected void EquipLightHandle()
        {
            if (playerStateMachine.InputReader.isEquipedLight)
            {
                EquipLight();
            }
            else
                UnequipLight();
        }
        private void EquipLight()
        {
            //playerStateMachine.Animator.SetLayerWeight(playerStateMachine.Animator.GetLayerIndex("EquipedLight"), 1);
            playerStateMachine.Animator.SetBool("EquipedLight", true);
        }
        private void UnequipLight()
        {
            //playerStateMachine.Animator.SetLayerWeight(playerStateMachine.Animator.GetLayerIndex("EquipedLight"), 0);
            playerStateMachine.Animator.SetBool("EquipedLight", false);
        }

        protected Vector3 CaculateMovementToTarget(bool isMovingToTarget = false)
        {
            Vector3 foward = playerStateMachine.MainCameraTransform.forward;
            Vector3 right = playerStateMachine.MainCameraTransform.right;

            foward.y = 0f;
            right.y = 0f;

            foward.Normalize();
            right.Normalize();
            if (isMovingToTarget)
            {
                Vector3 targetDir = playerStateMachine.Targeter.CurrentTarget.transform.position - playerStateMachine.transform.position;
                targetDir.y = 0f;
                return targetDir;
            }
            return foward * playerStateMachine.InputReader.MovementValue.y
                 + right * playerStateMachine.InputReader.MovementValue.x;

        }
        protected void FaceMovementDirection(Vector3 movement, float deltaTime)
        {
            if (playerStateMachine.willLockToCamera) { return; }
            playerStateMachine.transform.GetChild(0).GetChild(0).rotation = Quaternion.Lerp(
                playerStateMachine.transform.GetChild(0).GetChild(0).rotation,
                Quaternion.LookRotation(movement),
                deltaTime * playerStateMachine.RotationSmoothDamping);
        }

        protected void MoveToTarget()
        {
            if (playerStateMachine.IsIntereacting) return;
            if (!playerStateMachine.Targeter.SelectTarget())
            {
                playerStateMachine.InputReader.SetIsTargeting(false);
                return;
            }
            isMovingToTarget = !isMovingToTarget;
        }
        protected void Ontarget()
        {
            playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
        }
    }
}