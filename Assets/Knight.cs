using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class Knight : MonoBehaviour
{

    public float walkSpeed = 3f;
    public enum WalkableDirection { right, left };
    public WalkableDirection WalkDirection;
    TouchingDirections touchingDirections;
    Rigidbody2D rb;
    private Vector2 walkDirectionVector = Vector2.right;
    private bool hasFlipped = false;



    private WalkableDirection _walkDirection
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

   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
       
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
        if (!hasFlipped && touchingDirections.IsOnWall && touchingDirections.IsGrounded)
        {
            FlipDirection();
            hasFlipped = true;
        }
        else if (!touchingDirections.IsOnWall || !touchingDirections.IsGrounded)
        {
            hasFlipped = false; // Reset the flag if conditions are not met
        }
        rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.linearVelocityY);
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

