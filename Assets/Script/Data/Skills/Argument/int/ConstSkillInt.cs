using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstSkillInt : ISkillInt
{
    [SerializeField] private int number;
    public int SkillInt(CardFacade facade)
    {
        return number;
    }
    public string Text()
    {
        return number.ToString();
    }
    public string SkillName()
    {
        return number.ToString();
    }

}
