using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActivateButton : ClassForButton
{
    protected override void DoButtonTask()
    {
        shower.ClickActivateButton();
    }
}
