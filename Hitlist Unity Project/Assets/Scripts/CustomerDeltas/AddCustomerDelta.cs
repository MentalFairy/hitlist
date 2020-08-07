using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCustomerDelta : SkrptrAction
{
    public AudioSource audioSource;
    public InputField inputField;
    public override void Execute()
    {
        if (int.Parse(inputField.text) > 0)
            audioSource.Play();
        HitListMain.Instance.panelCustomerDelta.AddDelta();    
    }
}
