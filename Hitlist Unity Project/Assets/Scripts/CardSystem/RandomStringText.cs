using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomStringText : SkrptrAction
{
    public Text targetText;
    public string[] stringsToRanndom;
    public override void Execute()
    {
        targetText.text = stringsToRanndom[Random.Range(0, stringsToRanndom.Length - 1)];
    }
}
