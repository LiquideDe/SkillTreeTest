using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ClassForButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject background, backgroundPush;
    [SerializeField] protected CircleShower shower;

    public void OnPointerDown(PointerEventData eventData)
    {
        background.SetActive(false);
        backgroundPush.SetActive(true);
        Debug.Log($"Нажали");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        background.SetActive(true);
        backgroundPush.SetActive(false);
        DoButtonTask();
    }

    protected abstract void DoButtonTask(); 
}
