
using UnityEngine;

public abstract class State
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();
    protected float GetNormalizedTime(Animator Animator,string tag)
    {
        AnimatorStateInfo currentInfo = Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = Animator.GetNextAnimatorStateInfo(0);
        if (Animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else
            if (!Animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
            return 0f;
    }

}
