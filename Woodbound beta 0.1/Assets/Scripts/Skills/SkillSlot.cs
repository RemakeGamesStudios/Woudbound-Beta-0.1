using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SkillSlot : MonoBehaviour
{
    [Header("UI Stuff to change")]
    [SerializeField] private Image skillImage;
    [SerializeField] private Sprite [] skillSelectionImages;
    public Image skillSelectionImage;


    [Header("Variables from the skill")]
    public Skills thisSkill;
    public SkillManager thisManager;

    SkillSlot[] skillISlots;

    private void Start()
    {
        skillISlots = FindObjectsOfType<SkillSlot>();
    }

    public void Setup(Skills newSkill, SkillManager newManager)
    {
        thisSkill = newSkill;
        thisManager = newManager;
        if (thisSkill)
        {
            skillImage.sprite = thisSkill.skillImage;
        }
    }

    public void ClickedOn()
    {
        Debug.Log("You clicked an item!");
        Debug.Log("The item Description is " + thisSkill.skillDescription);
        if (thisSkill)
        {
            if (!thisSkill.isSelected&&thisSkill.isSelective)
            {
                for(int i = 0; i < skillISlots.Length; i++)
                {
                    skillISlots[i].thisSkill.isSelected = false;
                    if (skillISlots[i]) skillISlots[i].skillSelectionImage.sprite = skillSelectionImages[0];

                }
                thisSkill.isSelected = true;
                skillSelectionImage.sprite = skillSelectionImages[1];
            }
            thisManager.SetupDescriptionAndButton(thisSkill.skillDescription, thisSkill);
        }
    }

    public Sprite[] SelectionIcons()
    {
        return skillSelectionImages;
    }

}
