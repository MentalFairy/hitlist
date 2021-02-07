using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSoldier : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.panelManageSoldier.SaveSoldier();
    }
}
