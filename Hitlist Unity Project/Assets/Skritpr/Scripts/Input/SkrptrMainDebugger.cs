using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skrptr
{
    public class SkrptrMainDebugger : MonoBehaviour
    {
        public SkrptrElement selectedElem, hoveredElem, lastHoveredElem;
        public SkrptrInputType inputType;
        // Update is called once per frame
        void Update()
        {
            selectedElem = SkrptrMain.selectedElem;
            hoveredElem = SkrptrMain.hoveredElem;
            lastHoveredElem = SkrptrMain.lastHoveredElem;
            inputType = SkrptrMain.inputType;
        }
    }
}
