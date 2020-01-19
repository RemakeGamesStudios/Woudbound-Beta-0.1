using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{

    [SerializeField] GameObject chargeUp;
    [SerializeField] GameObject slashEffect;
    PlayerController player;
    Animator animator;
    Rigidbody2D rigidbody;
    bool isChargingUp = false;
    float starttime;
    [SerializeField] int direction = -1;
    (int, int)[] directions = { (1, 0), (-1, 0), (0, 1), (0, -1) };

    bool isDashing = false;
    float dashTime;
    [SerializeField] float startDashTime;
    [SerializeField] float dashSpeed;
    [SerializeField] GameObject dashEffect;

    //skills
    [SerializeField] private Skills slash;
    [SerializeField] private Skills spinAttack;
    [SerializeField] private Skills dash;


    void Start()
    {
        chargeUp.SetActive(false);
        player = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }


    void Update()
    {
        ConstantSkills();
        SelectiveSkills();
    }

    private void ConstantSkills()
    {
        Slash();
    }

    private void SelectiveSkills()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (spinAttack.isLearned && spinAttack.isSelected)
            {
                SpinAttack();
            }
            else if (dash.isLearned && dash.isSelected)
            {
                StartCoroutine(DashMove());
            }
        }
    }

    private IEnumerator DashMove()
    {
        player.currentState = PlayerState.cast;
        GameObject dashVFX = Instantiate(dashEffect, transform.position, Quaternion.identity);
        Destroy(dashVFX, 0.5f);
        if (direction == 0)
        {
            rigidbody.velocity = Vector2.right * dashSpeed;
        }
        else if (direction == 1)
        {
            rigidbody.velocity = Vector2.left * dashSpeed;
        } else if (direction == 2)
        {
            rigidbody.velocity = Vector2.up * dashSpeed;
        } else if (direction == 3)
        {
            rigidbody.velocity = Vector2.down * dashSpeed;
        }
        yield return new WaitForSeconds(dashTime);
        rigidbody.velocity = Vector2.zero;
        player.currentState = PlayerState.walk;
    }



    private int GetDirection()  //need to be rewirte
    {
        float currentDirection = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));
        if(currentDirection == Mathf.Abs(Input.GetAxis("Horizontal")))
        {
            if(Input.GetAxis("Horizontal") > 0)
            {
                direction = 0; //right
            }
            else if(Input.GetAxis("Horizontal") < 0)
            {
                direction = 1; //left
            }
        }
        if (currentDirection == Mathf.Abs(Input.GetAxis("Vertical")))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                direction = 2; //up
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                direction = 3; //down
            }
        }
        
        return direction;
    }

    private void SpinAttack()
    {
        
         StartCoroutine(SpinAttackAnimation());
      
    }
    public IEnumerator SpinAttackAnimation()
    {

        animator.SetBool("attackingSpin", true);
        SoundManager.instance.PlaySound("playerAttack");
        player.currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attackingSpin", false);
        yield return new WaitForSeconds(.7f);

        if (player.currentState != PlayerState.interact)
        {
            player.currentState = PlayerState.walk;
        }
    }

    private void Slash()
    {
        GetDirection();
        if (Input.GetButtonDown("Fire1"))
        {
            starttime = Time.time;
        }
        if (Input.GetButton("Fire1"))
        {
            if (Time.time - starttime >= 1f)
            {
                isChargingUp = true;
                chargeUp.SetActive(true);
                player.currentState = PlayerState.cast;
                animator.SetBool("isChargingUp", true);
            }
        }
        if (Input.GetButtonUp("Fire1") && isChargingUp)
        {

            //&&(Input.GetButton("Horizontal")||Input.GetButton("Vertical")
            isChargingUp = false;
            chargeUp.SetActive(false);
            animator.SetBool("isChargingUp", false);

            if (direction == -1)
            {
                player.currentState = PlayerState.walk;
                return;
            }

            StartCoroutine(SlashAnimation());
            StartCoroutine(WindAnimation());
            StartCoroutine(SlashPunishment());
        }
    }

    private IEnumerator SlashAnimation()
    {
        animator.SetFloat("moveX", directions[direction].Item1);
        animator.SetFloat("moveY", directions[direction].Item2);
        animator.SetTrigger("Slash");
        SoundManager.instance.PlaySound("playerAttack");
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator WindAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        slashEffect.transform.GetChild(direction).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        slashEffect.transform.GetChild(direction).gameObject.SetActive(false);
        direction = -1;
    }

    private IEnumerator SlashPunishment()
    {
        yield return new WaitForSeconds(1f);
        player.currentState = PlayerState.walk;
    }

}



