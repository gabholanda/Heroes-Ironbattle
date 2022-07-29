using UnityEngine;

public class GoblinDyingState : BaseState
{
    private readonly GoblinStateMachine _sm;
    private float changeStateTimer = 0f;
    private float changeStateTimerDur = 3f;

    public GoblinDyingState(GoblinStateMachine stateMachine) : base("Dying", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        EnemyDetector[] detectors = _sm.GetComponentsInChildren<EnemyDetector>();
        for (int i = 0; i < detectors.Length; i++)
        {
            detectors[i].gameObject.SetActive(false);
        }
        _sm.actions.StopRepeat("SeekTarget");
        _sm.GetComponent<Collider2D>().enabled = false;
        _sm.actions.graphics.PlayChildrenAnimations("Dying");
    }


    public override void UpdateLogic()
    {
        changeStateTimer += Time.deltaTime;
        if (changeStateTimer > changeStateTimerDur)
        {
            stateMachine.ChangeState(_sm.deadState);
        }
    }
}
