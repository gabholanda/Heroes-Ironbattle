using UnityEngine;

public class StandardIdleState : BaseState
{
    private GoblinStateMachine _sm;

    private float patrollingTime;
    private float intervalToGoPatrol;

    public StandardIdleState(GoblinStateMachine stateMachine) : base("Idle", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        _sm.actions.graphics.PlayChildrenAnimations("Idle");
        patrollingTime = 0f;
        intervalToGoPatrol = Random.Range(2f, 3f);
    }

    public override void UpdateLogic()
    {
        patrollingTime += Time.deltaTime;
        if (patrollingTime > intervalToGoPatrol)
        {
            _sm.ChangeState(_sm.patrollingState);
        }
    }
}
