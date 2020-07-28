using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearIfNoProjects : MonoBehaviour
{
    public Image imgTarget;
    private void Update()
    {
        if (HitListMain.Instance.panelProjectSelection.projects.Count > 0)
            imgTarget.color = new Color(imgTarget.color.r, imgTarget.color.g, imgTarget.color.b, 1);
        else
            imgTarget.color = new Color(imgTarget.color.r, imgTarget.color.g, imgTarget.color.b, 0);
    }
}
