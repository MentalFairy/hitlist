using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEveryDayTask : SkrptrAction
{
    public override void Execute()
    {
        GameObject.Destroy(GetComponentInParent<EveryDayTask>().gameObject);
    }
}
