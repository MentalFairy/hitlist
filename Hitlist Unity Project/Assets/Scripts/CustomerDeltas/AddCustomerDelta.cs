using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCustomerDelta : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.panelCustomerDelta.AddDelta();
    }
}
