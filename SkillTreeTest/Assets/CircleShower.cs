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

    public void CreateCircle(int id, float x, float y, string description)
    {
        circles.Add(Instantiate(circleTransform));
        int i = circles.Count - 1;
        var circle = circles[i].GetComponent<VisualSkillCircle>();
        circle.SetId(id);
        circle.SetDescription(description);
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
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(connectionsContainer, false);
        gameObject.GetComponent<Image>().color = new Color(0, .7f, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (secondPoint - firstPoint).normalized;
        float distance = Vector2.Distance(firstPoint, secondPoint);
        //rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = new Vector2(distance, 3f);

        rectTransform.anchoredPosition = firstPoint + dir * distance * .5f;
        Vector3 rot = Quaternion.LookRotation(secondPoint - firstPoint).eulerAngles;
        rectTransform.localEulerAngles = new Vector3(0, 0, rot.x);
        connections.Add(rectTransform);
    }
}
