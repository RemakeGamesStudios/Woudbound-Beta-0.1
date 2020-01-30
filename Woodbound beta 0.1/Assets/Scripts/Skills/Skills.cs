using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Skill", menuName = "Player Skills/Skills")]
public class Skills : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public Sprite skillImage;
    public UnityEvent thisEvent;
    public bool isSelective;
    public bool isSelected;
    public bool isLearned;

    public void Use()
    {
        Debug.Log("Using Item");
        thisEvent.Invoke();
    }
}

