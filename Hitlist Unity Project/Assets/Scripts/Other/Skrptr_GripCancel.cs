using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skrptr_GripCancel : SkrptrAction
{
    public override void Execute()
    {
        if(GetComponent<Skrptr_GripHold>() != null)
        {
#if UNITY_EDITOR
            Debug.Log("HELLO");
            GetComponent<Skrptr_GripHold>().isGripped = false;
#endif
        }
    }
}
