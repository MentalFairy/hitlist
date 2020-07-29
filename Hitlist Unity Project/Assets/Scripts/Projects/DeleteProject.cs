using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteProject : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.panelProjectSelection.DeleteProject();
        HitListMain.Instance.currentProject = "";
    }
}
