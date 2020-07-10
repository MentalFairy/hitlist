using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Skrptr
{
    public class SkrptrRadioButton : SkrptrCheckbox
    {
        public override void Click()
        {
            ExecuteActions(SkrptrEvent.Click);
            if(!isChecked)
            {
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    if(transform.parent.GetChild(i).GetComponent<SkrptrRadioButton>() != null)
                    {
                        SkrptrRadioButton auxRadioButton = transform.parent.GetChild(i).GetComponent<SkrptrRadioButton>();
                        if (auxRadioButton.isChecked)
                        {
                            auxRadioButton.Uncheck();
                            break;
                        }
                    }
                }
                Check();
            }
        }
    }
}
