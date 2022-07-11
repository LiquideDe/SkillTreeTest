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
            //Ищем ребра в которых участвует наша вершина, если в этом ребре есть база или любая активна вершина, значит нашу вершину тоже можно Изучить.
            if (skillCircles[connections[i][1]] == circle)
            {
                if (skillCircles[connections[i][0]] == skillCircles[0])
                {
                    answ = true;
                }
                else if (skillCircles[connections[i][0]].IsActive)
                {
                    //answ = CheckCircleForPossibleConnect(skillCircles[connections[i][0]]);
                    answ = true;
                }
            }
            
        }

        if (!answ)
        {
            //Если ничего не нашли, делаем еще один проход, но в обратном направлении
            answ = CheckCircleForPossibleConnectReverse(circle);
        }
        
        return answ;
    }

    private bool CheckCircleForPossibleConnectReverse(SkillCircle circle)
    {
        bool answ = false;
        for (int i = connections.Count - 1; i >= 0; i--)
        {
            if(skillCircles[connections[i][0]] == skillCircles[0] && skillCircles[connections[i][1]] == circle)
            {
                return true;
            }

            else if (skillCircles[connections[i][0]] == circle && skillCircles[connections[i][1]].IsActive)
            {
                //answ = CheckCircleForPossibleConnectReverse(skillCircles[connections[i][1]]);
                return true;
            }
        }

        return answ;
    }

    private bool CheckForActiveNextCircle(SkillCircle circle)
    {
        //Итог:
        //true - Соседняя Активная вершина с 1 путем до базы
        //false - Соседняя вершина или не активна или имеет другой путь до базы
        bool answ = false;
        int k = 0;
        for(int i = connections.Count - 1; i >= 0; i--)
        {
            //Нашли нашу вершину в списке ребер
            if (skillCircles[connections[i][0]] == circle)
            {
                k++;
                //Если вторая вершина в этом ребре активна, то
                if (skillCircles[connections[i][1]].IsActive)
                {
                    if (HowManyActiveRoutes(skillCircles[connections[i][1]]) == 1)
                    {
                        //Если у соседней вершины один путь, значит он пролегает только через нашу вершину
                        return true;
                    }

                    else if (FindPreviousCircle(circle))
                    {
                        //Если больше, значит ищем ребра в которых наша вершина вторая
                        return !CheckForActivePreviousCircleWithRestriction(skillCircles[connections[i][1]], circle);
                    }

                    else
                    {
                        answ = !FindPreviousCircle(circle);
                    }
                }
            }
        }

        //Если счетчик цикла остался ноль, и не сработало ни одного из условий, значит наша точка самая последняя и надо искать с самого низа где в ребре наша вершина вторая
        if (k==0)
        {
            for (int i = connections.Count - 1; i >= 0; i--)
            {
                if(skillCircles[connections[i][1]] == circle && skillCircles[connections[i][0]].IsActive)
                {
                    answ = !CheckForActivePreviousCircleWithRestriction(skillCircles[connections[i][0]], circle);
                    break;
                }
            }
        }

        return answ;
    }

    private int HowManyActiveRoutes(SkillCircle circle)
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

    private bool FindPreviousCircle(SkillCircle circle)
    {
        bool answ = false;
        for(int i = 0; i < connections.Count; i++)
        {
            if (skillCircles[connections[i][1]] == circle && skillCircles[connections[i][0]].IsActive)
            {
                //Если наша вершина 2 в ребре, а первая вершина в этой паре активна, то проверяем первую вершину дальше на доступ к базе
                answ = CheckForActivePreviousCircleWithRestriction(skillCircles[connections[i][0]], circle);               
                break;
            }
            else if(skillCircles[connections[i][1]] == circle && !skillCircles[connections[i][0]].IsActive)
            {
                //Если предыдущая вершина отключена, это означает только одно - наша вершина последняя и ее можно деактивировать.
                answ = true;
                break;
            }
        }

        return answ;
    }

    private bool CheckForActivePreviousCircleWithRestriction(SkillCircle circle, SkillCircle restriction, SkillCircle previousCircle = null )
    {
        //true - у кнопки есть прямой доступ к базе несмотря на ограниченные вершины
        bool answ = false;
        for (int i = connections.Count - 1; i >= 0; i--)
        {
            //Если первая вершина в паре это база, значит мы дошли до базы с ограничениями, а значит Истина
            if (circle == skillCircles[0])
            {
                return true;
            }

            if (skillCircles[connections[i][0]] == circle && skillCircles[connections[i][1]] != restriction && skillCircles[connections[i][1]].IsActive)
            {
                //Если мы нашли соседню вершину, которая не является исключающей, и она активна, то проверяем ее дальше.
                answ = answ || CheckForActivePreviousCircleWithRestriction(skillCircles[connections[i][1]], restriction, circle);
                if(HowManyActiveRoutes(circle) < 3)
                {
                    //Если у вершина 1 или 2 пути, то значит мы уже рассмотрели все пути и можно завершать. 
                    return answ;
                }                
            }            
        }

        //Если мы смогли дойти до сюда, значит мы не смогли найти соседние ребра где наша вершина стоит выше, следовательно делаем все то же самое, но "с низа". 
        answ = CheckActiveCircleFromDownToUp(circle, restriction, previousCircle);
        return answ;
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
                    return true;
                }
                else
                {
                    answ = CheckActiveCircleFromDownToUp(skillCircles[connections[i][0]], restriction, circle);
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
