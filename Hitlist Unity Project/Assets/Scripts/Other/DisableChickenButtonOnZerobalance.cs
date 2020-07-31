using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableChickenButtonOnZerobalance : MonoBehaviour
{
    private Image img;
    private void Start()
    {
        img = GetComponent<Image>();
    }
    private void Update()
    {
        if (HitListMain.Instance.panelManageSoldier.balance < HitListMain.Instance.panelManageSoldier.chickenPrice)
            img.raycastTarget = false;
        else
            img.raycastTarget = false;       

    }
}
