using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour, IMovable
{
    public DashHandler dashHandler;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private CharacterStats stats;
    public CharacterAnimator CharAnim { get; set; }
    private Vector3 movVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (dashHandler.isDashing)
        {
            rb.MovePosition(Time.deltaTime * dashHandler.GetAbilityData().scalingCoeficient * movVector + transform.position);
        }
        else
        {
            rb.MovePosition(transform.position + movVector * Time.deltaTime);
        }
        Flip();
    }

    public void SetVector(Vector2 v2)
    {
        //if (!dashHandler.isDashing)
        movVector = new Vector3(v2.x, v2.y) * stats.combatStats.MoveSpeed;
        if (IsMoving())
        {
            CharAnim.SetAnimation("Walk", true, true, false, false);
        }
        else
        {
            CharAnim.SetAnimation("Idle", true, true, false, false);
        }
    }

    public CharacterMovement SetStats(CharacterStats _stats)
    {
        stats = _stats;
        return this;
    }

    public CharacterMovement SetAnimator(CharacterAnimator _charAnim)
    {
        CharAnim = _charAnim;
        return this;
    }

    public CharacterMovement SetDashHandler(DashHandler _dashHandler)
    {
        dashHandler = _dashHandler;
        return this;
    }

    public CharacterMovement InitializeDashHandler(GameObject castingPoint, Vector3 characterPosition)
    {
        dashHandler.Initialize(castingPoint, characterPosition);
        return this;
    }


    public void Dash()
    {
        if (IsMoving() && !dashHandler.isCoolingDown)
        {
            dashHandler.Execute(gameObject, gameObject.transform.position);
        }
    }

    public bool IsMoving()
    {
        return Mathf.Abs(movVector.x) > 0 || Mathf.Abs(movVector.y) > 0;
    }

    public void Flip()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 10f;
        Vector3 positionToWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        if (positionToWorld.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    public bool IsFlipped()
    {
        return transform.localScale.x == -1;
    }
}
