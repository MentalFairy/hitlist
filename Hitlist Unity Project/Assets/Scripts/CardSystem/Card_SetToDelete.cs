using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_SetToDelete : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.cardToBeDeleted = GetComponentInParent<Card>();
        HitListMain.Instance.panelRemoveCardSafety.GetComponent<SkrptrElement>().Unlock();
    }
}
