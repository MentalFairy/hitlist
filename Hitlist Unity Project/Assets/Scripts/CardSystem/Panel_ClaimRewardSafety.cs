﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_ClaimRewardSafety : MonoBehaviour
{
    private void Start()
    {
        HitListMain.Instance.panelClaimRewardSafety = this;
    }
}
