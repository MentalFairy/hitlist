﻿using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_MetricsRiskRefresh : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.panelMetricsDay.CountCards();
    }
}
