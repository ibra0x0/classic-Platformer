using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{

    public UnityEvent<int , Vector2> damageableHit;
    
    Animator animator;


    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    [SerializeField]
    private float invincibilityTime = 0.25f;
    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get 
        {
            return _maxHealth;
        }

        set
        {
            _maxHealth = value;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    


    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
        set { animator.SetBool(AnimationStrings.lockVelocity, value); }
    }

    public int Health { 
        get 
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <=0) { 
                
                IsAlive = false; }
        }}



    private float timeSinceHit = 0;
   

    public bool IsAlive {

        get
        {
            return _isAlive;
        }

        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive is set to : " +value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible) {

            if (timeSinceHit > invincibilityTime) {
                // remove incincibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }


    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible) {

            Health -= damage;
            isInvincible = true;


            // notify other subscribed components that the damageable was hit to handle the knockback
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            return true;
        }
        // unable to hit
        return false;
    }

}
