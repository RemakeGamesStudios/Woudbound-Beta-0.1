using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle,
    shield,
    cast
}

public enum PlayerHealthState
{
    normal,
    poisoned,
    iced,
    fired
}

public class PlayerController : MonoBehaviour
{

    public PlayerState currentState;
    public PlayerHealthState currentHealthState;
    public string currentRoom; // tracks current room
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public Signal playerHit;
    public Signal reduceMagic;
    bool hasBeenSlowed = false;
    float playerRegularSpeed = 0;
    [SerializeField] GameObject iceEffect;

    [Header("IFrame Stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;

    [Header("Projectile Stuff")]
    public GameObject projectile;
    public Item bow;
    public bool readyToShield = false;

    //Previous moving state
    private bool prevMovingState = false;

    // Use this for initialization
    void Start()
    {
        //reserting playerHealth in the ScriptableObject
        currentHealth.RuntimeValue = currentHealth.initialValue; // 6
        currentState = PlayerState.walk;
        currentHealthState = PlayerHealthState.normal;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAction();
        PlayerStatus();
    }

    private void PlayerStatus()
    {

        if (currentHealthState != PlayerHealthState.iced)
        {
            iceEffect.SetActive(false);
            hasBeenSlowed = false;
            if (playerRegularSpeed == 0) playerRegularSpeed = speed;
            else speed = playerRegularSpeed;
        }
        if (currentHealthState == PlayerHealthState.poisoned)
        {
            currentHealth.RuntimeValue -= 0.1f * Time.deltaTime;
        }
        else if(currentHealthState == PlayerHealthState.iced)
        {
            if (!hasBeenSlowed)
            {
                speed *= 0.2f;
                iceEffect.SetActive(true);
                hasBeenSlowed = true;
            }
        }else if(currentHealthState == PlayerHealthState.fired)
        {
            currentHealth.RuntimeValue -= 0.25f * Time.deltaTime;
        }
        playerHealthSignal.Raise();

        if(currentHealthState != PlayerHealthState.iced)
        {
            speed = playerRegularSpeed;
        }

        if (currentHealth.RuntimeValue <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void PlayerAction()
    {
        // Is the player in an interaction
        if (currentState == PlayerState.interact || currentState == PlayerState.cast)
        {
            return;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        // Is the player attacking
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack
           && currentState != PlayerState.stagger && currentState != PlayerState.shield)
        {
            StartCoroutine(AttackCo());
        }
        else if (Input.GetButtonDown("Second Weapon") && currentState != PlayerState.attack
           && currentState != PlayerState.stagger)
        {
            //if (playerInventory.CheckForItem(bow))
            StartCoroutine(SecondAttackCo());
        }
        else if (readyToShield && currentState != PlayerState.attack
           && currentState != PlayerState.stagger && currentState != PlayerState.shield)
        {
            StartCoroutine(Shielding());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    public IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        SoundManager.instance.PlaySound("playerAttack");
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.5f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator SecondAttackCo()
    {
        //animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        MakeArrow();
        //animator.SetBool("attacking", false);
        
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }

    }

    private IEnumerator Shielding()
    {
        animator.SetBool("Shielding", true);
        currentState = PlayerState.shield;
        yield return null;
        animator.SetBool("Shielding", false);
        readyToShield = false;
        yield return new WaitForSeconds(0.25f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }


    private void MakeArrow()
    {
        if (playerInventory.currentMagic > 0)
        {
            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ChooseArrowDirection());
            playerInventory.ReduceMagic(arrow.magicCost);
            reduceMagic.Raise();
        }
    }

    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }


    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receive item", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receive item", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
            if (prevMovingState == false)
            {
                SoundManager.instance.PlaySound("playerWalk");
                prevMovingState = true;
            }
        }
        else
        {
            animator.SetBool("moving", false);
            prevMovingState = false;
            SoundManager.instance.PlaySound("playerStopWalking");
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }

    public void Knock(float knockTime, float damage)
    {
        Debug.Log(currentHealth.RuntimeValue + " " + damage);
        currentHealth.RuntimeValue -= damage;// / 100; // for trailer
        //Debug.Log()
        playerHealthSignal.Raise();
        //currentHealth.RuntimeValue = 6;
        if (currentHealth.RuntimeValue > 0)
        {
            playerHit.Raise();
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        Debug.Log(knockTime);
        if (myRigidbody != null)
        {
            Debug.Log(knockTime);
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
}
