using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerTarget_Minus : SkrptrAction
{
    public InputField value;
    public override void Execute()
    {
        HitListMain.Instance.panelManageSoldier.customerTarget--;
        value.text = HitListMain.Instance.panelManageSoldier.customerTarget.ToString();
        HitListMain.Instance.panelManageSoldier.SaveSoldier();
    }
}
