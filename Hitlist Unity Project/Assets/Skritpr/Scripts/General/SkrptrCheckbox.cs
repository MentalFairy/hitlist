using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Skrptr
{
    public class SkrptrCheckbox : SkrptrElement
    {
        public bool isChecked = false;
        public override void Click()
        {
            base.Click();
            if (isChecked)
                Uncheck();
            else
                Check();
        }
        public override void Check()
        {
            isChecked = true;
            base.Check();
        }
        public override void Uncheck()
        {
            isChecked = false;
            base.Uncheck();
        }
    }
}