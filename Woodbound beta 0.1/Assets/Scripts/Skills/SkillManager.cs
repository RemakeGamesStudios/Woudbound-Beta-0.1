using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillManager : MonoBehaviour
{
    [Header("Inventory Information")]
    public PlayerSelectiveSkill playerSelectiveSkills;
    public PlayerConstantSkill playerConstantSkills;

    [SerializeField] private GameObject blankSkillSlot;
    [SerializeField] private GameObject selectiveSkillPanel;
    [SerializeField] private GameObject constantSkillPanel;

    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;

    public Skills currentSkill;

    void OnEnable()
    {
        ClearSkillSlots();
        MakeSelectiveSkillSlots();
        MakeConstantSkillSlots();
        Debug.Log("made inventory");
        SetTextAndButton("", false);
    }

    public void SetTextAndButton(string description, bool buttonActive)
    {
        // Fill the description and show up the use button if available
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    void MakeSelectiveSkillSlots()
    {
        if (playerSelectiveSkills)
        {
            for (int i = 0; i < playerSelectiveSkills.mySelectiveSkills.Count; i++)
            {
                GameObject temp =
                    Instantiate(blankSkillSlot,
                    selectiveSkillPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(selectiveSkillPanel.transform);
                SkillSlot newSlot = temp.GetComponent<SkillSlot>();
                newSlot.Setup(playerSelectiveSkills.mySelectiveSkills[i], this);
                if (newSlot)
                {
                    if(!newSlot.thisSkill.isSelected)
                    {
                        newSlot.skillSelectionImage.sprite = newSlot.SelectionIcons()[0];
                    }
                    else if (newSlot.thisSkill.isSelected)
                    {
                        newSlot.skillSelectionImage.sprite = newSlot.SelectionIcons()[1];
                    }
                }
                newSlot.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    void MakeConstantSkillSlots()
    {
        if (playerConstantSkills)
        {
            for (int i = 0; i < playerConstantSkills.myConstantSkills.Count; i++)
            {
                GameObject temp =
                    Instantiate(blankSkillSlot,
                    constantSkillPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(constantSkillPanel.transform);
                SkillSlot newSlot = temp.GetComponent<SkillSlot>(); 
                if (newSlot)
                {
                    newSlot.skillSelectionImage.gameObject.SetActive(false);
                    newSlot.Setup(playerConstantSkills.myConstantSkills[i], this);
                }
                newSlot.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void ClearSkillSlots()
    {
        for (int i = 0; i < selectiveSkillPanel.transform.childCount; i++)
        {
            Destroy(selectiveSkillPanel.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < constantSkillPanel.transform.childCount; i++)
        {
            Destroy(constantSkillPanel.transform.GetChild(i).gameObject);
        }
    }

    public void SetupDescriptionAndButton(string newDescriptionString,
     Skills newSkill)
    {
        currentSkill = newSkill;
        descriptionText.text = newDescriptionString;
    }

}
