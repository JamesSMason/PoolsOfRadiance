using PoR.Character.Customisation.Skills;
using System.Collections.Generic;
using UnityEngine;

public class CharClass : MonoBehaviour
{
    // TODO: This will be replaced by a player options choice in character creation
    [SerializeField] List<Skill> skillProficiencies;
    
    public List<Skill> GetSkillProficiencies()
    {
        return skillProficiencies;
    }
}