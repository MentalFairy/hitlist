using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerDelta_Plus : SkrptrAction
{
    public InputField value;
    public override void Execute()
    {
        value.text = (int.Parse(value.text) + 1).ToString();
        HitListMain.Instance.panelCustomerDelta.CheckValue();
    }
}
