using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Skrptr
{
    public class SkrptrMouseInput : StandaloneInputModule
    {
        private void Update()
        {
           

            if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) && Input.touchCount != 0)
            {
                SkrptrMain.inputType = SkrptrInputType.Mouse;
                Debug.LogError("Setting to mouse");
            }

            if (SkrptrMain.inputType == SkrptrInputType.Mouse)
            {
                PointerEventData pointerData = GetLastPointerEventData(-1);
                if (pointerData == null)
                    return;

                if (pointerData.pointerCurrentRaycast.isValid)
                {
                    if (pointerData.pointerCurrentRaycast.gameObject.GetComponent<SkrptrElement>() != null)
                    {
                        if (SkrptrMain.hoveredElem != null)
                        {
                            if (pointerData.pointerCurrentRaycast.gameObject.GetComponent<SkrptrElement>() != SkrptrMain.hoveredElem)
                            {
                                SkrptrMain.hoveredElem.HoverExit();
                                SkrptrMain.lastHoveredElem = SkrptrMain.hoveredElem;
                                SkrptrMain.hoveredElem = null;
                            }
                        }
                        if (SkrptrMain.hoveredElem == null)
                        {
                            SkrptrMain.hoveredElem = pointerData.pointerCurrentRaycast.gameObject.GetComponent<SkrptrElement>();
                            SkrptrMain.hoveredElem.HoverEnter();
                        }

                        //First input
                        if (Input.GetMouseButtonDown(0))
                        {
                            //Debug.Log("DOWN ENTERED");
                            //if (currentElement != null)
                            //    currentElement.OnDeselect();

                            SkrptrMain.selectedElem = pointerData.pointerCurrentRaycast.gameObject.GetComponent<SkrptrElement>();
                            SkrptrMain.selectedElem.Select();
                        }
                        // while holding mouse button down
                        if (Input.GetMouseButton(0))
                        {
                            // Debug.Log("MOUSED PRessed");
                            if (SkrptrMain.selectedElem != null)
                            {
                                if (SkrptrMain.hoveredElem != SkrptrMain.selectedElem)
                                {
                                    //if (mouseManager.lastSelectedElement != null)
                                    //    mouseManager.lastSelectedElement.OnDeselect();
                                    SkrptrMain.selectedElem.Deselect();
                                    SkrptrMain.selectedElem = null;
                                }
                            }
                        }
                        // Relase / Click
                        if (Input.GetMouseButtonUp(0))
                        {
                            //  Debug.Log("MOUSE RELEASED");
                            if (SkrptrMain.selectedElem != null)
                            {
                                //if (mouseManager.lastSelectedElement != null && mouseManager.lastSelectedElement != SkrptrMain.selectedElem)
                                //{
                                //    mouseManager.lastSelectedElement.OnDeselect();
                                //}
                                SkrptrMain.selectedElem.Click();
                                //mouseManager.lastSelectedElement = SkrptrMain.selectedElem;
                                //                mouseManager.HandleInput(DirectionEventTrigger.Into);
                                SkrptrMain.selectedElem = null;
                            }
                        }
                    }
                    else
                    {
                        if (SkrptrMain.hoveredElem != null)
                        {
                            SkrptrMain.hoveredElem.HoverExit();
                            SkrptrMain.lastHoveredElem = SkrptrMain.hoveredElem;
                            SkrptrMain.hoveredElem = null;
                        }
                    }
                }
                //Invalid Raycast
                else
                {
                    if (SkrptrMain.hoveredElem != null)
                    {
                        SkrptrMain.hoveredElem.HoverExit();
                        SkrptrMain.lastHoveredElem = SkrptrMain.hoveredElem;
                        SkrptrMain.hoveredElem = null;
                    }
                    if (SkrptrMain.selectedElem != null)
                    {
                        SkrptrMain.selectedElem.HoverExit();
                        SkrptrMain.selectedElem = null;
                    }
                }
            }
        }

    }
}

