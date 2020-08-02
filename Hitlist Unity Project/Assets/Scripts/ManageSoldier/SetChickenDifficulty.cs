using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetChickenDifficulty : SkrptrAction
{
    public Text label;
    public override void Execute()
    {
        HitListMain.Instance.panelManageSoldier.chickenRegen = int.Parse(label.text);
    }
}
