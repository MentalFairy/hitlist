using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Down : SkrptrAction
{
    public override void Execute()
    {
        GetComponentInParent<Card>().DownPress();
    }
}
