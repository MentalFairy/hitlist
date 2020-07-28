using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddProject : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.panelProjectSelection.AddProject();
        HitListMain.Instance.panelProjectSelection.SaveProjects();
    }
}
