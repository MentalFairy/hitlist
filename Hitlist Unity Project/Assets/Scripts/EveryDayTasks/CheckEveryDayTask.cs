using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEveryDayTask : SkrptrAction
{
    public override void Execute()
    {
        GetComponentInParent<EveryDayTask>().Check();
    }
}
