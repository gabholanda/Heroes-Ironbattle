using UnityEngine;

public class SlimePatrollingState : BaseState
{
    private SlimeStateMachine _sm;

    private float checkRepathing;
    private float checkRepathingTimer = 0.3f;
    private float upSpeed = 1f;
    private float downSpeed = 1.6f;

    public SlimePatrollingState(SlimeStateMachine stateMachine) : base("Patrol", stateMachine)
    {
        _sm = stateMachine;
    }
    public override void Enter()
    {
        _sm.UpdatePath();
    }

    public override void UpdateLogic()
    {
        _sm.UpdatePath();
    }

    public override void UpdatePhysics()
    {
        _sm.SetMovement(_sm.stats.moveSpeed);
        if (_sm.goUp)
            _sm.GoUp(upSpeed);
        else
            _sm.GoDown(downSpeed);

        CanRepath();
    }

    public override void Exit()
    {
        _sm.ResetWayPoint();
    }


    private void CanRepath()
    {
        if (_sm.rb.velocity.x == 0 & _sm.rb.velocity.y == 0)
        {
            checkRepathing += Time.deltaTime;
            if (checkRepathing > checkRepathingTimer)
            {
                Vector2 standardPath = _sm.rb.position + Random.insideUnitCircle * 8f;
                checkRepathing = 0f;
                _sm.ResetWayPoint();
                _sm.StartNewPath(standardPath);
            }
        }
    }

}
