using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_RemoveCardSafety : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HitListMain.Instance.panelRemoveCardSafety = this;
    }

}
