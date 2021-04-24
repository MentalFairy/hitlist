using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_MetricsYearRefresh : SkrptrAction
{
    public override void Execute()
    {
        HitListMain.Instance.panelMetricsYear.RefreshValues();
    }
}
