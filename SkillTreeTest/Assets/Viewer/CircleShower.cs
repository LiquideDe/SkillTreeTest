using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleShower : MonoBehaviour
{
    List<RectTransform> circles = new List<RectTransform>();
    List<RectTransform> connections = new List<RectTransform>();
    [SerializeField] private RectTransform circleTransform;
    [SerializeField] private Transform circlesContainer, connectionsContainer;
    [SerializeField] private GameObject buttonActivate, buttonDeactivate;
    [SerializeField] private Text textSkillPoints;
    public event EventHandler Clicked;
    public event EventHandler ClickedActivate;
    public event EventHandler ClickedDeactivate;
    public event EventHandler ClickedReset;
    public event EventHandler ClickedEarnPoints;

    public void CreateCircle(int id, float x, float y, string description, int cost)
    {
        circles.Add(Instantiate(circleTransform));
        int i = circles.Count - 1;
        var circle = circles[i].GetComponent<VisualSkillCircle>();
        circle.SetId(id);
        circle.SetDescription($"{description} - {cost} points");
        circles[i].SetParent(circlesContainer);
        circles[i].anchoredPosition = new Vector2(x, y);
        circles[i].gameObject.SetActive(true);
        if(id == 0)
        {
            circle.Activate();
        }
        
    }

    public void CreateConnections(int idFirstPoint, int idSecondPoint)
    {
        DrawConnection(circles[idFirstPoint].anchoredPosition, circles[idSecondPoint].anchoredPosition);
    }

    private void DrawConnection(Vector2 firstPoint, Vector2 secondPoint)
    {
        GameObject gameObject = new GameObject("skillConnection", typeof(Image));
        gameObject.transform.SetParent(connectionsContainer, false);
        gameObject.GetComponent<Image>().color = new Color(0, .7f, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (secondPoint - firstPoint).normalized;
        float distance = Vector2.Distance(firstPoint, secondPoint);
        //rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = new Vector2(distance, 3f);

        rectTransform.anchoredPosition = firstPoint + dir * distance * .5f;
        
        Vector3 rot = Quaternion.LookRotation(secondPoint - firstPoint).normalized.eulerAngles;

        
        if(secondPoint.x < 0)
        {
            rectTransform.localEulerAngles = new Vector3(0, 0, rot.x);
        }
        else
        {
            rectTransform.localEulerAngles = new Vector3(0, 0, rot.x * -1);
        }

        connections.Add(rectTransform);
    }

    public void ClickOnCircle(int id)
    {
        Clicked(id, EventArgs.Empty);
        for(int i = 1; i < circles.Count; i++)
        {
            if(i != id)
            {
                circles[i].GetComponent<VisualSkillCircle>().HideDescription();
            }
        }
    }

    public void ClickActivateButton()
    {
        ClickedActivate(0, EventArgs.Empty);
    }

    public void ClickDeactivateButton()
    {
        ClickedDeactivate(1, EventArgs.Empty);
    }

    public void ClickReset()
    {
        ClickedReset(2, EventArgs.Empty);
        for(int i = 1; i < circles.Count; i++)
        {
            DeactivateSkill(i);
        }
        ShowNoButton();
        ClickOnCircle(0);
    }

    public void ShowButtonActivate()
    {
        buttonActivate.SetActive(true);
        buttonDeactivate.SetActive(false);
    } 

    public void ShowButtonDeactivate()
    {
        buttonActivate.SetActive(false);
        buttonDeactivate.SetActive(true);
    }

    public void ShowNoButton()
    {
        buttonActivate.SetActive(false);
        buttonDeactivate.SetActive(false);
    }

    public void ActivateSkill(int id)
    {
        circles[id].GetComponent<VisualSkillCircle>().Activate();
    }

    public void DeactivateSkill(int id)
    {
        circles[id].GetComponent<VisualSkillCircle>().DeActivate();
    }

    public void SetAmountSkillPoints(int amount)
    {
        textSkillPoints.text = $"Количество очков {amount}";
    }

    public void EarnPoints()
    {
        ClickedEarnPoints(3, EventArgs.Empty);
    }
}
