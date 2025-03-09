using UnityEngine;

public class TouchingDirections : MonoBehaviour
{

    CapsuleCollider2D touchingCol;
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    
    [SerializeField]
    private bool _isGrounded;

    public bool IsGrounded { get {
            return _isGrounded;
        } private set {

            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        } }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

    }
   


    // Update is called once per frame
    void FixedUpdate()
    {
       IsGrounded =  touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }
}
