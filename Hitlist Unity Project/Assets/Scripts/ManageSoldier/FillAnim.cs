using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAnim : SkrptrAnim
{
    public Image img;
    public Text label;
    private float timePassed = 0;
    public override void Execute()
    {
        StartCoroutine(nameof(FillImage));
    }
    private IEnumerator FillImage()
    {
        img.fillAmount = 0;
        timePassed = 0;
        while (timePassed < 3.5f)
        {
            img.fillAmount += 0.03f / 3.5f;
            timePassed += 0.03f;
            label.text = timePassed.ToString("0.0");
            yield return new WaitForSecondsRealtime(0.03f);
        }
    }
}
