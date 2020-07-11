using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skrptr;
public class AddEveryDayTask : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.panelEveryDayTasks.AddTask();
    }
}
