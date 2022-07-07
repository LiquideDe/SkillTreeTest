using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisualSkillCircle : MonoBehaviour, IPointerDownHandler
{
    private int id;
    [SerializeField] private GameObject backgroundActive, backgroundDeactive;
    [SerializeField] private Text textDescription, textId;
    public event EventHandler Clicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        Clicked(id, EventArgs.Empty);
    }

    public void Activate()
    {
        backgroundActive.SetActive(true);
        backgroundDeactive.SetActive(false);
    }

    public void DeActivate()
    {
        backgroundActive.SetActive(false);
        backgroundDeactive.SetActive(true);
    }

    public void SetId(int id)
    {
        this.id = id;
        textId.text = $"{id}";
    }

    public void SetDescription(string text)
    {
        textDescription.text = text;
    }
}
