using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Skrptr
{
    public class SkrptrKeyboardInput : MonoBehaviour
    {
        public KeyCombo upCombo = new KeyCombo (new List<KeyCode>() {  KeyCode.UpArrow}), 
                        downCombo = new KeyCombo(new List<KeyCode>() { KeyCode.DownArrow }),
                        leftCombo = new KeyCombo(new List<KeyCode>() { KeyCode.LeftArrow }),
                        rightcombo = new KeyCombo(new List<KeyCode>() { KeyCode.RightArrow }),
                        backCombo = new KeyCombo(new List<KeyCode>() { KeyCode.Escape }),
                        clickCombo = new KeyCombo(new List<KeyCode>() { KeyCode.Return });

        // Update is called once per frame
        void Update()
        {
            if(upCombo.IsKeyComboDown())
            {
                InputTriggered(SkrptrDirection.Up);
            }
            else if (downCombo.IsKeyComboDown())
            {
                InputTriggered(SkrptrDirection.Down);
            }
            else if (leftCombo.IsKeyComboDown())
            {
                InputTriggered(SkrptrDirection.Left);
            }
            else if (rightcombo.IsKeyComboDown())
            {
                InputTriggered(SkrptrDirection.Right);
            }
            else if (backCombo.IsKeyComboDown())
            {
                InputTriggered(SkrptrDirection.Back);
            }
            else if (clickCombo.IsKeyComboDown())
            {
                InputTriggered(SkrptrDirection.Click);
            }
        }
        private void InputTriggered(SkrptrDirection direction)
        {
            SkrptrMain.inputType = SkrptrInputType.Keyboard;

            if (SkrptrMain.hoveredElem == null)
            {
                if (SkrptrMain.lastHoveredElem != null)
                {
                    SkrptrMain.hoveredElem = SkrptrMain.lastHoveredElem;
                    SkrptrMain.hoveredElem.HoverEnter();
                }
            }
            else
            {
                if (direction != SkrptrDirection.Click && direction != SkrptrDirection.Back)
                {

                    SkrptrKeyboardMapper mapper = SkrptrMain.hoveredElem.GetComponent<SkrptrKeyboardMapper>();
                    GameObject targetNeighbour = null;
                    for (int i = 0; i < mapper.neighbours.Count; i++)
                    {
                        if ((mapper.neighbours[i].direction & direction) == direction)
                        {
                            targetNeighbour = mapper.neighbours[i].target.gameObject;
                        }
                        else
                        {

                        }
                   
                    }
                    SkrptrMain.hoveredElem.HoverExit();
                    SkrptrMain.lastHoveredElem = SkrptrMain.hoveredElem;

                    SkrptrMain.hoveredElem = targetNeighbour.GetComponent<SkrptrElement>();
                    SkrptrMain.hoveredElem.HoverEnter();
                }
            }
        }
    }
}