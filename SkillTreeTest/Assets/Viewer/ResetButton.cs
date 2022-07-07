using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : ClassForButton
{
    protected override void DoButtonTask()
    {
        shower.ClickReset();
    }
}
