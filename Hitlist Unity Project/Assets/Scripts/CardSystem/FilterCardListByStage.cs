﻿using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterCardListByStage : SkrptrAction
{
    public CardStage filter;
    public override void Execute()
    {
        HitListMain.Instance.currentStage = filter;
        HitListMain.Instance.panelCards.FilterCards();       
    }
}
