using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLogic 
{
    private List<List<int>> connections = new List<List<int>>();

    private List<SkillCircle> skillCircles = new List<SkillCircle>();

    private int skillPoints = 2;

    public int SkillPoints { get { return skillPoints; } }

    public void CreateSkillCircle(int id, string description, int cost)
    {
        skillCircles.Add(new SkillCircle(id, description, cost));
    }

    public void CreateConnections(int first, int second)
    {
        connections.Add(new List<int>() { first, second });
    }
    public bool CheckCircleByIdForPossibleConnect(int id)
    {
        return CheckCircleForPossibleConnect(skillCircles[id]);
    }

    public bool CheckCircleByIdForActiveNextCircle(int id)
    {
        return CheckForActiveNextCircle(skillCircles[id]);
    }

    /*
    public void CheckAllCircles()
    {
        bool answ;
        for (int i = 1; i < skillCircles.Count; i++)
        {
            answ = CheckCircleForPossibleConnect(skillCircles[i]);

            if(!answ && !skillCircles[i].IsActive && skillCircles[i].IsContactWithBase)
            {
                skillCircles[i].CanNotBeActivated();
            }
            else if (answ)
            {
                skillCircles[i].CanBeActivated();
            }
        }
    }
    */
    private bool CheckCircleForPossibleConnect(SkillCircle circle)
    {
        bool answ = false;
        for (int i = 0; i < connections.Count; i++)
        {
            if (skillCircles[connections[i][1]] == circle)
            {
                if (skillCircles[connections[i][0]] == skillCircles[0])
                {
                    answ = true;
                }
                else if (skillCircles[connections[i][0]].IsActive)
                {
                    answ = CheckCircleForPossibleConnect(skillCircles[connections[i][0]]);
                }
            }
        }
        return answ;
    }

    private bool CheckForActiveNextCircle(SkillCircle circle)
    {
        bool answ = false;
        for(int i = connections.Count - 1; i >= 0; i--)
        {
            if (skillCircles[connections[i][0]] == circle)
            {
                if (skillCircles[connections[i][1]].IsActive)
                {
                    return true;
                }
            }
        }
        return answ;
    }

    public bool IsCircleActiveById(int id)
    {
        return skillCircles[id].IsActive;
    }

    public bool ActivateSkill(int id)
    {
        if (skillPoints >= skillCircles[id].Cost)
        {
            skillPoints -= skillCircles[id].Cost;
            skillCircles[id].ActivateSkill();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeactivateSkill(int id)
    {
        skillPoints += skillCircles[id].Cost;
    }
}
