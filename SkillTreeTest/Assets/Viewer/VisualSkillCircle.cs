using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void ClickOnCircle(int idCircle);
public class VisualSkillCircle : MonoBehaviour, IPointerDownHandler
{
    private int id;
    [SerializeField] private GameObject backgroundActive, backgroundDeactive, choiceImg;
    [SerializeField] private Text textDescription, textId;
    
    ClickOnCircle clickOnCircle;

    public void RegistredDelegate(ClickOnCircle clickOnCircle)
    {
        this.clickOnCircle = clickOnCircle;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(id != 0)
        {
            clickOnCircle?.Invoke(id);
            textDescription.gameObject.SetActive(true);
            choiceImg.SetActive(true);
        }        
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
        if(id == 0)
        {
            textId.text = $"База";
        }
        else
        {
            textId.text = $"{id}";
        }
        
    }

    public void SetDescription(string text)
    {
        textDescription.text = text;
    }

    public void HideDescription()
    {
        textDescription.gameObject.SetActive(false);
        choiceImg.SetActive(false);
    }
}
