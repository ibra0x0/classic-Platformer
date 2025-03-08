using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    Vector2 moveInput;
    Animator animator;
    Rigidbody2D rb;

    public bool _isFacingright = true;
   
    [SerializeField]
    private bool _isMoving = false;
    [SerializeField]
    private bool _isRunning = false;

    public float currentMoveSpeed { get
        {
            if (IsMoving)
            {
                if (IsRunning)
                {
                    return runSpeed;
                }

                else { return walkSpeed; }
            }

            else { return 0; }
        } }
    public bool IsMoving { get {
            return _isMoving;
        }

        private set {

            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);


        }
    }

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }

        private set
        {

            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool IsFacingRight
    {
        get { return _isFacingright; }
        private set
        {
            if (_isFacingright != value)
            {
                // Flip the local scale to make the player face the opposite direction}
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingright = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * currentMoveSpeed, rb.linearVelocityY);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            //Face the right
            IsFacingRight = true;
        }
        else if (moveInput.x<0 && IsFacingRight){

            // Face the left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
}
