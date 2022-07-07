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
        CreateCircle(0, 0, 0, "base", 0);
        CreateCircle(1, -161, 123, "run", 1);
        CreateCircle(2, 157, 116, "heat", 1);
        CreateCircle(3, 110, -151, "eat", 1);
        CreateCircle(4, 291, -207, "drink", 1);

        CreateConnections(0, 1);
        CreateConnections(0, 2);
        CreateConnections(0, 3);
        CreateConnections(3, 4);
        showerCircle.Clicked += ClickOnCircle;
        showerCircle.ClickedActivate += ClickActivate;
        showerCircle.ClickedDeactivate += ClickDeactivate;
        showerCircle.ClickedReset += ClickReset;
        //circleLogic.CheckAllCircles();
    }

    private void CreateCircle(int id, float x, float y, string description, int cost)
    {
        showerCircle.CreateCircle(id, x, y, description);
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
        ChosenIdButton = id;/*
        Debug.Log($"Кнопка {id} нажата");
        Debug.Log($"Проверим как там работает система поиска путей, и она нам выдает {circleLogic.CheckCircleByIdForPossibleConnect(id)}");*/
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
        Debug.Log($"Нажимаем диактевейт");
        if (!circleLogic.CheckCircleByIdForActiveNextCircle(ChosenIdButton))
        {
            showerCircle.ShowButtonActivate();
            showerCircle.DeactivateSkill(ChosenIdButton);
            circleLogic.DeactivateSkill(ChosenIdButton);
        }
    }

    private void ClickReset(object id, EventArgs e)
    {

    }

}
