using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Skills", menuName = "Player Skills/Player Selective Skills")]
public class PlayerSelectiveSkill : PlayerSkills
{
    public List<Skills> mySelectiveSkills = new List<Skills>();

    public void AddSkill(Skills skill)
    {
        mySelectiveSkills.Add(skill);
        skill.isLearned = true;
    }
}
