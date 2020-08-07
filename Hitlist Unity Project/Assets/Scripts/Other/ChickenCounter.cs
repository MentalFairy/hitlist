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
    public FillAnim fillAnim;
    private void Start()
    {
        InvokeRepeating(nameof(CountChickens), 0, repeatInterval);
    }
    private void CountChickens()
    {        
        label.text = HitListMain.Instance.panelManageSoldier.chickenCount.ToString();
        if (fillAnim.animIsRunning == false)
        {
            if (HitListMain.Instance.panelManageSoldier.chickenCount > 0)
                consumeChickenButton.raycastTarget = true;
            else
                consumeChickenButton.raycastTarget = false;
        }
    }
}
