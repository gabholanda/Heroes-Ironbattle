using System;
using UnityEngine;

public class SlimeDyingState : BaseState
{
    private SlimeStateMachine _sm;
    private float changeStateTimer = 0f;
    private float changeStateTimerDur = 0.3f;

    public SlimeDyingState(SlimeStateMachine stateMachine) : base("Dying", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        EnemyDetector[] detectors = _sm.GetComponentsInChildren<EnemyDetector>();
        for (int i = 0; i < detectors.Length; i++)
        {
            detectors[i].gameObject.SetActive(false);
        }
        _sm.StopRepeat("SeekTarget");
        _sm.GetComponent<Collider2D>().enabled = false;
        _sm.anim.Play("Dying");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        changeStateTimer += Time.deltaTime;
        if (changeStateTimer > changeStateTimerDur)
        {
            stateMachine.ChangeState(_sm.deadState);
        }
    }
}