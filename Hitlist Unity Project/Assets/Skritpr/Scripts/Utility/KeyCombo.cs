using System.Collections.Generic;
using UnityEngine;
#if ODYN_IS_IMPORTED
using Sirenix.OdinInspector;
#endif
[System.Serializable]
public class KeyCombo
{
    #if ODYN_IS_IMPORTED
    [ListDrawerSettings(ShowIndexLabels = true)]
    #endif
    public List<KeyCode> keys;
    public bool hasBeenPressed;

    public KeyCombo(List<KeyCode> keys)
    {
        this.keys = keys;
    }
    public bool IsKeyComboDown()
    {
        if (keys.Count > 1)
        {
            if (Input.GetKey(keys[0]))
            {
                for (int i = 1; i < keys.Count; i++)
                {
                    if (i != keys.Count - 1)
                    {
                        if (!Input.GetKey(keys[i]))
                        {
                            hasBeenPressed = false;
                            return false;

                        }
                    }
                    //Last Key
                    else
                    {
                        if (!Input.GetKeyDown(keys[i]))
                        {
                            hasBeenPressed = false;
                            return false;
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            hasBeenPressed = true;
            return true;
        }
        // 1 key
        else
        {
            if (Input.GetKeyDown(keys[0]))
                return true;
            else
                return false;
        }
    }
    public bool IsKeyComboHeldDown()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (!Input.GetKey(keys[i]))
                return false;
        }       
        return true;
    }
    public override string ToString()
    {
        string returnString = "KeyCombo: ";
        for (int i = 0; i < keys.Count; i++)
        {
            returnString += keys[i].ToString() + "-";
        }
        returnString = returnString.Remove(returnString.Length - 1);

        return returnString;
    }
}
