using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_SetToClaimReward : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.cardToClaimReward = GetComponentInParent<Card>();
        HitListMain.Instance.panelClaimRewardSafety.GetComponent<SkrptrElement>().Unlock();
    }
}
