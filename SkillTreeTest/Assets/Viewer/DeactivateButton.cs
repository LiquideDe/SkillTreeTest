using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateButton : ClassForButton
{
    protected override void DoButtonTask()
    {
        Debug.Log($"������ ����������");
        shower.ClickDeactivateButton();
    }
}
