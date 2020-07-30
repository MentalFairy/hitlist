using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditTextOnLongPress : SkrptrAction
{
    public InputField inputfield;
    public override void Execute()
    {
        Debug.Log("Long Press");
        inputfield.Select();
        inputfield.ActivateInputField();
    }
}
