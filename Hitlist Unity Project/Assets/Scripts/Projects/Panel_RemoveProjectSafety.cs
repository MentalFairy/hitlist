using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_RemoveProjectSafety : MonoBehaviour
{
    private void Awake()
    {
        HitListMain.Instance.panelRemoveProjectSafety = this;
    }
}
