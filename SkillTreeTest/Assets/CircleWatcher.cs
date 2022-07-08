using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWatcher
{
    private CircleLogic circleLogic;
    private CircleShower showerCircle;

    private int ChosenIdButton;
    public CircleWatcher(CircleShower showerCircle)
    {
        circleLogic = new CircleLogic();
        this.showerCircle = showerCircle;
    }
    
    public void CreateTree()
    {
        /*
        CreateCircle(0, 0, 0, "base", 0);
        CreateCircle(1, -161, 123, "run", 1);
        CreateCircle(2, 157, 116, "heat", 1);
        CreateCircle(3, 110, -151, "eat", 1);
        CreateCircle(4, 291, -207, "drink", 1);

        CreateConnections(0, 1);
        CreateConnections(0, 2);
        CreateConnections(2, 4);
        CreateConnections(0, 3);
        CreateConnections(3, 4);
        */

        CreateCircle(0, 0, 0, "base", 0);
        CreateCircle(1, -64, 79, "Ходьба", 1);
        CreateCircle(2, 58, -47, "Бег", 1);
        CreateCircle(3, 137, -118, "Прыжок", 2);
        CreateCircle(4, -109, -79, "Удар", 1);
        CreateCircle(5, -204, -56, "Пинок", 2);
        CreateCircle(6, -81, -191, "Кивок", 2);
        CreateCircle(7, -212, -181, "Удар с разворота", 3);
        CreateCircle(8, 89, 97, "Зрение", 1);
        CreateCircle(9, 117, 18, "Слух", 1);
        CreateCircle(10, 198, 104, "Эмоции", 2);

        CreateConnections(0,1);
        CreateConnections(0,2);
        CreateConnections(0,4);
        CreateConnections(0,8);
        CreateConnections(0,9);
        CreateConnections(2,3);
        CreateConnections(4,5);
        CreateConnections(4,6);
        CreateConnections(5,7);
        CreateConnections(6,7);
        CreateConnections(8,10);
        CreateConnections(9,10);

        Initial();
        circleLogic.EarnPoints(4);
    }

    private void Initial()
    {
        showerCircle.Clicked += ClickOnCircle;
        showerCircle.ClickedActivate += ClickActivate;
        showerCircle.ClickedDeactivate += ClickDeactivate;
        showerCircle.ClickedReset += ClickReset;
        showerCircle.ClickedEarnPoints += EarnPoints;

        circleLogic.SkillPointsChanged += SkillPointsWasChanged;
    }

    private void CreateCircle(int id, float x, float y, string description, int cost)
    {
        showerCircle.CreateCircle(id, x, y, description, cost);
        circleLogic.CreateSkillCircle(id, description, cost);
    }

    private void CreateConnections(int idFirstPoint, int idSecondPoint)
    {
        showerCircle.CreateConnections(idFirstPoint, idSecondPoint);
        circleLogic.CreateConnections(idFirstPoint, idSecondPoint);
    }

    private void ClickOnCircle(object idCircle, EventArgs e)
    {
        int id = (int)idCircle;
        ChosenIdButton = id;
        if (id > 0)
        {            
            if (circleLogic.IsCircleActiveById(id))
            {
                if (!circleLogic.CheckCircleByIdForActiveNextCircle(id))
                {
                    showerCircle.ShowButtonDeactivate();
                }
                else
                {
                    showerCircle.ShowNoButton();
                }
            }
            else if (circleLogic.CheckCircleByIdForPossibleConnect(id))
            {
                showerCircle.ShowButtonActivate();
            }
            else
            {
                showerCircle.ShowNoButton();
            }
        }
    }

    private void ClickActivate(object id, EventArgs e)
    {
        if (circleLogic.ActivateSkill(ChosenIdButton))
        {
            showerCircle.ShowButtonDeactivate();
            showerCircle.ActivateSkill(ChosenIdButton);
        }
    }

    private void ClickDeactivate(object id, EventArgs e)
    {
        if (!circleLogic.CheckCircleByIdForActiveNextCircle(ChosenIdButton))
        {
            showerCircle.ShowButtonActivate();
            showerCircle.DeactivateSkill(ChosenIdButton);
            circleLogic.DeactivateSkill(ChosenIdButton);
        }
    }

    private void ClickReset(object id, EventArgs e)
    {
        circleLogic.ResetSkills();
    }

    private void EarnPoints(object id, EventArgs e)
    {
        circleLogic.EarnPoints(1);
    }

    private void SkillPointsWasChanged(object skillpoints, EventArgs e)
    {
        showerCircle.SetAmountSkillPoints((int)skillpoints);
    }
}
