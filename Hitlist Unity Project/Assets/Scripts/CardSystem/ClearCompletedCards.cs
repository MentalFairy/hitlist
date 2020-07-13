using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCompletedCards : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.panelCards.ClearCompletedCards();
    }
}
