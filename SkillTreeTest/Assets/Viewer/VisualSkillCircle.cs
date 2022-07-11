using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisualSkillCircle : MonoBehaviour, IPointerDownHandler
{
    private int id;
    [SerializeField] private GameObject backgroundActive, backgroundDeactive, choiceImg;
    [SerializeField] private Text textDescription, textId;
    [SerializeField] private CircleShower shower;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        if(id != 0)
        {
            shower.ClickOnCircle(id);
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
