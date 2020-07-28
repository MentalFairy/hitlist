using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skrptr;
public class SkrptrAction_EnableDisableGO : SkrptrAction
{
    public List<GameObject> enableGOs, disableGOs;

    public override void Execute()
    {
        foreach (var item in enableGOs)
        {
            item.SetActive(true);
        }
        foreach (var item in disableGOs)
        {
            item.SetActive(false);
        }
    }
}
