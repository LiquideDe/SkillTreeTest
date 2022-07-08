using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CircleLogic 
{
    private List<List<int>> connections = new List<List<int>>();

    private List<SkillCircle> skillCircles = new List<SkillCircle>();

    private int skillPoints;

    public event EventHandler SkillPointsChanged;

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
                    if(HowMuchActiveRoute(skillCircles[connections[i][1]]) == 1)
                    {
                        return true;                        
                    }
                    else
                    {
                        return !CheckForActivePreviousCircleWithRestriction(skillCircles[connections[i][1]], circle);
                    }
                }
            }
        }
        return answ;
    }

    private int HowMuchActiveRoute(SkillCircle circle)
    {
        int amount = 0;
        for (int i = 0; i < connections.Count; i++)
        {
            if (skillCircles[connections[i][0]] == circle && skillCircles[connections[i][1]].IsActive)
            {
                amount++; 
            }
            else if(skillCircles[connections[i][1]] == circle && skillCircles[connections[i][0]].IsActive)
            {
                amount++;
            }
        }

        return amount;
    }

    private bool CheckForActivePreviousCircleWithRestriction(SkillCircle circle, SkillCircle restriction, SkillCircle previousCircle = null )
    {
        for (int i = connections.Count - 1; i >= 0; i--)
        {
            if (skillCircles[connections[i][0]] == circle && skillCircles[connections[i][1]] != restriction && skillCircles[connections[i][1]].IsActive)
            {
                Debug.Log($"Нашли более низкий круг {skillCircles[connections[i][1]].Id}, идем дальше");
                return CheckForActivePreviousCircleWithRestriction(skillCircles[connections[i][1]], restriction, circle);
            }           
        }

        Debug.Log($"Идем на глубину");
        return CheckActiveCircleFromDownToUp(circle, restriction, previousCircle);
    }

    private bool CheckActiveCircleFromDownToUp(SkillCircle circle, SkillCircle restriction, SkillCircle previousCircle)
    {
        bool answ = false;
        for (int i = connections.Count - 1; i >= 0; i--)
        {
            if (skillCircles[connections[i][1]] == circle && skillCircles[connections[i][0]] != restriction && skillCircles[connections[i][0]].IsActive && skillCircles[connections[i][0]] != previousCircle)
            {
                if (skillCircles[connections[i][0]] == skillCircles[0])
                {
                    Debug.Log($"Мы нашли базу true");
                    return true;
                }
                else
                {
                    answ = CheckActiveCircleFromDownToUp(skillCircles[connections[i][0]], restriction, circle);
                }
            }
        }

        Debug.Log($"Получили для точки {circle.Id} ответ {answ}");
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
            SkillPointsChanged(skillPoints, EventArgs.Empty);
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
        if(skillCircles[id].IsActive)
        {
            skillPoints += skillCircles[id].Cost;
            SkillPointsChanged(skillPoints, EventArgs.Empty);
            skillCircles[id].DeactivateSkill();
        }        
    }

    public void EarnPoints(int amount)
    {
        skillPoints += amount;
        SkillPointsChanged(skillPoints, EventArgs.Empty);
    }

    public void ResetSkills()
    {
        for(int i = 1; i < skillCircles.Count; i++)
        {
            DeactivateSkill(i);
        }
    }
}
