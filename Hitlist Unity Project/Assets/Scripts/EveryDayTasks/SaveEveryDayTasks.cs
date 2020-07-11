using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skrptr;
public class SaveEveryDayTasks : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.panelEveryDayTasks.SaveTasks();
    }
}
