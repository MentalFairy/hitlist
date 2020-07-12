using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCurrentProject : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.currentProject = GetComponent<Project>().inputField.text;
    }
}
