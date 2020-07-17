using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeChicken : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.panelManageSoldier.ConsumeChicken();
    }
}
