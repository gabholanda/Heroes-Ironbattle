using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour, IMovable
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private CharacterStats stats;
    public CharacterAnimator CharAnim { get; set; }
    private Vector3 movVector;

    //TODO: Change it to the ability system and just pass a handler
    [Header("Dash Info")]
    public float dashCoeficient;
    public float dashDuration;
    public float dashTimer;
    public float dashCooldown;
    public float dashCooldownTimer;
    public bool isDashing;
    public bool canDash;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTimer = 0;
        canDash = true;
        dashDuration = 0.1f;
        dashCooldown = 1f;
        dashCoeficient = 3f;
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
        Flip();
    }

    public void SetVector(Vector2 v2)
    {
        if (!isDashing)
            movVector = new Vector3(v2.x, v2.y) * stats.moveSpeed;
        if (IsMoving())
        {
            CharAnim.SetAnimation("Walk", true, true);
        }
        else
        {
            CharAnim.SetAnimation("Idle", true, true);
        }
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
