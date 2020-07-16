using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterCardListByProject : SkrptrAction
{
    public string projectName;
    public override void Execute()
    {
        projectName = GetComponent<Project>().inputField.text;
        HitListMain.Instance.currentProject = projectName;
        HitListMain.Instance.panelCards.FilterCards();
    }
}
