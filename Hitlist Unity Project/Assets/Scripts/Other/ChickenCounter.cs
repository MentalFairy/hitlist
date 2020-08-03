using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenCounter : MonoBehaviour
{
    public Text label;
    public Image consumeChickenButton;
    public float repeatInterval = 0.33f;
    private void Start()
    {
        InvokeRepeating(nameof(CountCards), 0, repeatInterval);
    }
    private void CountCards()
    {
        label.text = HitListMain.Instance.panelManageSoldier.chickenCount.ToString();
        if (HitListMain.Instance.panelManageSoldier.chickenCount > 0)
            consumeChickenButton.raycastTarget = true;
        else
            consumeChickenButton.raycastTarget = false;
    }
}
