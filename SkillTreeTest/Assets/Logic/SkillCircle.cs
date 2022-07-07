using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCircle 
{
    private bool isBase, isActive;
    private int cost;
    private string description;
    private int id;

    public bool IsBase { get { return isBase; } }
    public bool IsActive { get { return isActive; } }
    public int Cost { get { return cost; } }
    public int Id { get { return id; } }

    public SkillCircle(int id, string description, int cost)
    {
        if(id == 0)
        {
            isBase = true;
            isActive = true;
        }
        else
        {
            isBase = false;
            isActive = false;
        }
        this.id = id;        
        this.cost = cost;
        this.description = description;
    }

    public SkillCircle()
    {
        isBase = true;
        isActive = true;
    }

    public void ActivateSkill()
    {
        isActive = true;
    }

    public void DeactivateSkill()
    {
        isActive = false;
    }
}
