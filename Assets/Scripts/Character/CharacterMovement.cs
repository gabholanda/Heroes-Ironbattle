using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, IMovable
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private CharacterStats stats;
    private Animator animator;
    private ChildAnimationPlayer childAnimation;
    private string currentAnim;
    private Vector3 movVector;

    [Header("Dash Info")]
    public float dashCoeficient;
    public float dashDuration;
    public float dashTimer;
    public float dashCooldown;
    public float dashCooldownTimer;
    public bool isDashing;
    public bool canDash;
    // Start is called before the first frame update

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        childAnimation = gameObject.GetComponentInChildren<ChildAnimationPlayer>();
        dashTimer = 0;
        canDash = true;
    }

    private void Update()
    {
        if (isDashing)
        {
            rb.MovePosition(transform.position + movVector * Time.deltaTime * dashCoeficient);
        }
        else
        {
            rb.MovePosition(transform.position + movVector * Time.deltaTime);
        }
    }

    public void SetVector(Vector2 v2)
    {
        if (!isDashing)
            movVector = new Vector3(v2.x, v2.y) * stats.moveSpeed;
        if (IsMoving())
        {
            currentAnim = "Walk";
        }
        else
        {
            currentAnim = "Idle";
        }
        animator.Play(currentAnim);
        childAnimation.PlayAnimation(currentAnim);
    }

    public void Dash()
    {
        if (IsMoving() && canDash)
        {
            isDashing = true;
            canDash = false;
            StartCoroutine(StartDashing());
        }
    }

    public bool IsMoving()
    {
        return Mathf.Abs(movVector.x) > 0 || Mathf.Abs(movVector.y) > 0;
    }

    public bool IsDashDurationOver()
    {
        return dashDuration < dashTimer;
    }
    public void SetStats(CharacterStats _stats)
    {
        stats = _stats;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
    }

    public IEnumerator StartDashing()
    {
        while (isDashing)
        {
            yield return new WaitForSeconds(0.1f);
            dashTimer += 0.1f;
            if (IsDashDurationOver())
            {
                isDashing = false;
                dashTimer = 0f;
                //movVector = new Vector2(0, 0);
                StartCoroutine(StartDashCooldown());
            }
        }
    }

    public IEnumerator StartDashCooldown()
    {
        while (!canDash)
        {
            yield return new WaitForSeconds(0.1f);
            dashCooldownTimer += 0.1f;
            if (dashCooldownTimer > dashCooldown)
            {
                canDash = true;
                dashCooldownTimer = 0f;
            }
        }
    }
}
