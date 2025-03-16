using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{

    public UnityEvent<int , Vector2> damageableHit;
    [SerializeField]
    private int _maxHealth = 100;
    Animator animator;

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
    [SerializeField]
    private int _health = 100;

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

    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;

    

    private float timeSinceHit = 0;
    private float invincibilityTime = 0.25f;

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
            damageableHit?.Invoke(damage, knockback);
            return true;
        }
        // unable to hit
        return false;
    }

}
