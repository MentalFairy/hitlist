using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateChickenRegenLabel : SkrptrAction
{
    public Text label;
    public override void Execute()
    {
        label.text = HitListMain.Instance.panelManageSoldier.chickenRegen.ToString();
    }
}
