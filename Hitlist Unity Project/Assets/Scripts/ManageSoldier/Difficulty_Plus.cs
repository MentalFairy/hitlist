using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty_Plus : SkrptrAction
{
    public Text difficultyLabel;
    public override void Execute()
    {
        HitListMain.Instance.panelManageSoldier.chickenRegen++;
        difficultyLabel.text = HitListMain.Instance.panelManageSoldier.chickenRegen.ToString();
        HitListMain.Instance.panelManageSoldier.SaveSoldier();
    }
}
