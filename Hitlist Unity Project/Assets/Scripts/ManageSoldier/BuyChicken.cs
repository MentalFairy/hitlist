using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skrptr;
public class BuyChicken : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.panelManageSoldier.BuyChicken();       
    }
}
