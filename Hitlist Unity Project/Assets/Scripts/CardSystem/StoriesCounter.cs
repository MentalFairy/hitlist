using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoriesCounter : MonoBehaviour
{
    public Text label;
    public Transform contentParent;
    public float repeatInterval = 0.33f;
    private void Start()
    {
        InvokeRepeating(nameof(CountCards), 0, repeatInterval);
    }
    private void CountCards()
    {
        label.text = contentParent.GetComponentsInChildren<Card>().Length.ToString();
    }
}
