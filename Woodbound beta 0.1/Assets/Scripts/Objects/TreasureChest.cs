using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreasureChest : Interactable {

    [Header("Contents")]
    //item
    public Item contents;
    public Inventory playerInventory;
    //skill orb
    public Skills skill;
    public PlayerSelectiveSkill playerSelectiveSkill;
    public PlayerConstantSkill playerConstantSkill;

    public bool isOpen;
    public BoolValue storedOpen;

    [Header("Signals and Dialog")]
    public Signal raiseItem;
    public GameObject dialogBox;
    public TextMeshPro dialogText;

    [Header("Animation")]
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        isOpen = false;
        if(isOpen)
        {
            anim.SetBool("opened", false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("attack") && playerInRange)
        {
            if(!isOpen)
            {
                // Open the chest
                OpenChest();
            }else
            {
                // Chest is already open
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        // Dialog window on
        dialogBox.SetActive(true);
        if (skill) CollectSkillOrbs();
        else CollectItems();
        isOpen = true;
        anim.SetBool("opened", true);
        storedOpen.RuntimeValue = isOpen;
    }

    private void CollectItems()
    {
        // dialog text = contents text
        dialogText.text = contents.itemDescription;
        // add contents to the inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        // Raise the signal to the player to animate
        raiseItem.Raise();
        // raise the context clue
        context.Raise();
        // set the chest to opened
    }

    private void CollectSkillOrbs()
    {
        dialogText.text = skill.skillDescription;
        if (skill.isSelective) playerSelectiveSkill.AddSkill(skill);
        else playerConstantSkill.AddSkill(skill);
    }

    public void ChestAlreadyOpen()
    {
        // Dialog off
        dialogBox.SetActive(false);
        // raise the signal to the player to stop animating
        raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // whether player is in range
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}
