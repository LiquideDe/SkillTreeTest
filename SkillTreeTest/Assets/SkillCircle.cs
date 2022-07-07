using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCircle 
{
    private bool isBase, isContactWithBase, isActive;
    private float cost;
    private string description;
    private int id;

    public bool IsBase { get { return isBase; } }
    public bool IsActive { get { return isActive; } }
    public bool IsContactWithBase { get { return isContactWithBase; } }
    public float Cost { get { return cost; } }
    public int Id { get { return id; } }

    public SkillCircle(int id, string description, float cost)
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
        isContactWithBase = true;
    }

    public void DeactivateSkill()
    {
        isActive = false;
        isContactWithBase = false;
    }

    public void CanBeActivated()
    {
        isContactWithBase = true;
    }

    public void CanNotBeActivated()
    {
        isContactWithBase = false;
    }
}
