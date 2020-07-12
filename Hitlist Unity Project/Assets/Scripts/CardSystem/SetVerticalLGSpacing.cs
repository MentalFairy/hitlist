using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVerticalLGSpacing : SkrptrAction
{
    public VerticalLayoutGroup vlg;
    public int value = 0;
    public override void Execute()
    {
        vlg.padding.top = value;
    }
}
