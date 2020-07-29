using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCard : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.panelCards.DeleteCard();
    }
}
