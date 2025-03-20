using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class Knight : MonoBehaviour
{

    public float walkSpeed = 3f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    Damageable damageable;
    public enum WalkableDirection { right, left };
    Animator animator;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.right)
                { walkDirectionVector = Vector2.right; }

                else if (value == WalkableDirection.left)

                { walkDirectionVector = Vector2.left; }
            }
            _walkDirection = value;
        }

    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public float AttackCooldown { 
        
        get 
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }

        private set 
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value,0));
        } }

    TouchingDirections touchingDirections;
    Rigidbody2D rb;
    public Vector2 walkDirectionVector = Vector2.right;



    private WalkableDirection _walkDirection;
    private float walkRateSpeed = 0.06f;


    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (AttackCooldown > 0)
        { AttackCooldown -= Time.deltaTime; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    public void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.right)
        {
            WalkDirection = WalkableDirection.left;
        }
        else if (WalkDirection == WalkableDirection.left)
        {
            WalkDirection = WalkableDirection.right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right and left");
        }
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsOnWall && touchingDirections.IsGrounded)
        {
            FlipDirection();

        }
        if (!damageable.LockVelocity)
        {
            if (CanMove)
                rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.linearVelocityY);
            else
                rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocityX, 0, walkRateSpeed), rb.linearVelocityY);


        }

    }

 

    public void onHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocityY + knockback.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        { FlipDirection(); }
    }
}

