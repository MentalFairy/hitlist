using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAcceptedRejectedFilter : SkrptrAction
{
    public CardStatus setStatusTo;
    public override void Execute()
    {
        HitListMain.Instance.currentCardStatusFilter = setStatusTo;
        HitListMain.Instance.panelCards.FilterCards();
    }
}
