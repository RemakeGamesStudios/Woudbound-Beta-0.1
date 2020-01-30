using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Skills", menuName = "Player Skills/Player Constant Skills")]
public class PlayerConstantSkill : PlayerSkills
{
    public List<Skills> myConstantSkills = new List<Skills>();

    public void AddSkill(Skills skill)
    {
        myConstantSkills.Add(skill);
        skill.isLearned = true;
    }
}
