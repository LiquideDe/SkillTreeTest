using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLogic 
{
    private List<List<int>> connections = new List<List<int>>();

    private List<SkillCircle> skillCircles = new List<SkillCircle>();

    private int skillPoints = 2;

    public void CreateSkillCircle(int id, string description, float cost)
    {
        skillCircles.Add(new SkillCircle(id, description, cost));
    }

    public void CreateConnections(int first, int second)
    {
        connections.Add(new List<int>() { first, second });
    }
    public void CheckCircleById(int id)
    {
        CheckActiveCircle(skillCircles[id]);
    }

    public void CheckAllCircles()
    {
        bool answ;
        for (int i = 1; i < skillCircles.Count; i++)
        {
            answ = CheckActiveCircle(skillCircles[i]);
            Debug.Log($"Получили ответ от Circle {i}, ответ {answ}");
            if (!answ && skillCircles[i].IsActive)
            {
                skillCircles[i].DeactivateSkill();
            }
        }
    }

    private bool CheckActiveCircle(SkillCircle circle)
    {
        bool answ = false;
        for (int i = 0; i < connections.Count; i++)
        {
            if (skillCircles[connections[i][1]] == circle)
            {
                if (skillCircles[connections[i][0]] == skillCircles[0] && circle.IsActive)
                {
                    answ = true;
                }
                else if (skillCircles[connections[i][0]].IsActive)
                {
                    if (!circle.IsActive)
                    {
                        Debug.Log($"Circle {circle.Id} Может быть открыт");
                        circle.CanBeActivated();
                    }
                    answ = CheckActiveCircle(skillCircles[connections[i][0]]);
                }
            }
        }
        return answ;
    }

    public void ActivateSkill(int id)
    {
        if (skillPoints >= skillCircles[id].Cost)
        {
            skillCircles[id].ActivateSkill();
        }
    }
}
