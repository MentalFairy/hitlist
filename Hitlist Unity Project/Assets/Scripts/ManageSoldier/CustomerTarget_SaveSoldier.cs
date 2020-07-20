using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerTarget_SaveSoldier : SkrptrAction
{
    public InputField value;
    public override void Execute()
    {
        HitListMain.Instance.panelManageSoldier.customerTarget = int.Parse(value.text);
        HitListMain.Instance.panelManageSoldier.SaveSoldier();
        HitListMain.Instance.panelManageSoldier.InitCustomerData();
    }
}
