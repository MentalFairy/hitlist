using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Project_SetProjectToBeRemoved : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.projectToBeRemoved = GetComponentInParent<Project>();
        HitListMain.Instance.panelRemoveProjectSafety.GetComponent<SkrptrElement>().Unlock();
    }
}
