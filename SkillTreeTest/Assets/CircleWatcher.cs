using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWatcher
{
    private CircleLogic circleLogic;
    private CircleShower showerCircle;

    public CircleWatcher(CircleShower showerCircle)
    {
        circleLogic = new CircleLogic();
        this.showerCircle = showerCircle;
    }
    
    public void CreateTree()
    {
        CreateCircle(0, -358, 0, "base", 0);
        CreateCircle(1, -193, 0, "run", 1);
        CreateCircle(2, -80, 0, "heat", 2);
        CreateCircle(3, 80, 0, "eat", 3);

        CreateConnections(0, 1);
        CreateConnections(1, 2);
        CreateConnections(2, 3);
    }

    private void CreateCircle(int id, float x, float y, string description, float cost)
    {
        showerCircle.CreateCircle(id, x, y, "description");
        circleLogic.CreateSkillCircle(id, description, cost);
    }

    private void CreateConnections(int idFirstPoint, int idSecondPoint)
    {
        showerCircle.CreateConnections(idFirstPoint, idFirstPoint);
        circleLogic.CreateConnections(idSecondPoint, idSecondPoint);
    }

}
