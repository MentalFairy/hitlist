using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterCardListByProject : SkrptrAction
{
    public string projectName;
    private void Start()
    {
        projectName = GetComponent<Project>().inputField.text;
    }
    public override void Execute()
    {
        HitListMain.Instance.currentProject = projectName;
        HitListMain.Instance.panelCards.FilterCards();
    }
}
