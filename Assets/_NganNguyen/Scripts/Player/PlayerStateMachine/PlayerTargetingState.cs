using UnityEngine;
using SG15.ObjectFunction;

namespace SG15._NganNguyenStateMachine
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private readonly int hightTargetHash = Animator.StringToHash("Touch");
        private readonly int lowTargetHash = Animator.StringToHash("Pickup");
        private readonly float crossFadeDuration = 0.3f;
        private float lowStateDuration = 6f;
        private float hightStateDuration = 15;
        private float currentStateDuration;
        public PlayerTargetingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            playerStateMachine.PlayerAnimationEvent.ObjectInvestigate += itemFunctionHandle;
            playerStateMachine.SetIntereacting(true);
            ObjectsIntereactionType intereactionType = playerStateMachine.Targeter.CurrentTarget.GetComponent<IObjsFunction>().GetObjectsIntereactionTypes();
            if (intereactionType == ObjectsIntereactionType.LowState)
            {
                playerStateMachine.Animator.CrossFadeInFixedTime(lowTargetHash, crossFadeDuration);
                currentStateDuration = lowStateDuration;
            }
            else
            if (intereactionType == ObjectsIntereactionType.HightState)
            {
                playerStateMachine.Animator.CrossFadeInFixedTime(hightTargetHash, crossFadeDuration);
                currentStateDuration = hightStateDuration;
            }
        }
        public override void Tick(float deltaTime)
        {
            currentStateDuration -= deltaTime;
            if (currentStateDuration < 0)
            {
                playerStateMachine.SetIntereacting(false);
                playerStateMachine.Targeter.Cancel();
                playerStateMachine.SwitchState(new PlayerFreelookState(playerStateMachine));
                return;
            }
            FaceTarget();
        }

        public override void Exit()
        {
            playerStateMachine.PlayerAnimationEvent.ObjectInvestigate -= itemFunctionHandle;
        }
        private void itemFunctionHandle()
        {
            playerStateMachine.Targeter.CurrentTarget.GetComponent<IObjsFunction>().PobUpFunction();
        }
    }
}