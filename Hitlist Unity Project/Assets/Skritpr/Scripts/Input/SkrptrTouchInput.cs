using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Skrptr
{

    public class SkrptrTouchInput : MonoBehaviour
    {
        public GraphicRaycaster[] graphicRaycasters;
        public List<RaycastResult> raycastResults;
        public List<GameObject> hitSkrptrElements;
        public TouchPhase phase;
        public Touch touch;
        public float longPressDelay = .5f;
        public void LongPress()
        {
            SkrptrMain.hoveredElem.LongPress();
            SkrptrMain.hoveredElem = null;
        }
        protected  void Start()
        {            
            touch = new Touch();
            hitSkrptrElements = new List<GameObject>();
            raycastResults = new List<RaycastResult>();
            graphicRaycasters = FindObjectsOfType<GraphicRaycaster>();
        }

        public void Update()
        {
            if(Input.touchCount > 0)
            {
                SkrptrMain.inputType = SkrptrInputType.Touch;
            }

            if (SkrptrMain.inputType == SkrptrInputType.Touch)
            {
                hitSkrptrElements.Clear();
                raycastResults.Clear();
                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);
                    PointerEventData ped;
                    //Get touch and all items hit by raycasts from all canvases.                

                    ped = new PointerEventData(null);
                    ped.position = touch.position;

                    foreach (GraphicRaycaster raycaster in graphicRaycasters)
                    {
                        raycaster.Raycast(ped, raycastResults);
                    }
                    string resultHitGO = "";
                    foreach (RaycastResult result in raycastResults)
                    {
                        resultHitGO += result.gameObject.name + " | ";
                    }
                    if (raycastResults.Count > 0)
                    {
                        for (int i = 0; i < raycastResults.Count; i++)
                        {
                            if (raycastResults[i].gameObject.GetComponent<SkrptrElement>() != null && !hitSkrptrElements.Contains(raycastResults[i].gameObject))
                            {
                                hitSkrptrElements.Add(raycastResults[i].gameObject);
                            }
                        }
                    }


                    //TOUCH input management
                    //Fetch first touch and hit element
                    if (touch.phase == TouchPhase.Began)
                    {
                        phase = TouchPhase.Began;
                        if (hitSkrptrElements.Count > 0)
                        {
                            if (hitSkrptrElements[0].GetComponent<SkrptrElement>() != null)
                            {
                                Debug.Log("Touch Began / hover enter on: " + hitSkrptrElements[0].gameObject.name);
                                if (SkrptrMain.hoveredElem != null)
                                    SkrptrMain.hoveredElem.HoverExit();

                                if (SkrptrMain.hoveredElem != hitSkrptrElements[0].GetComponent<SkrptrElement>())
                                {
                                    SkrptrMain.hoveredElem = hitSkrptrElements[0].GetComponent<SkrptrElement>();
                                    SkrptrMain.hoveredElem.HoverEnter();
                                    Invoke(nameof(LongPress), longPressDelay);
                                }
                            }
                        }
                    }
                    //Reset if finger moved outside of original element
                    if (touch.phase == TouchPhase.Moved || (touch.phase == TouchPhase.Stationary))
                    {
                        if (touch.phase == TouchPhase.Moved)
                            phase = TouchPhase.Moved;
                        else if (touch.phase == TouchPhase.Stationary)
                            phase = TouchPhase.Stationary;
                        if (SkrptrMain.hoveredElem != null)
                        {
                            if (hitSkrptrElements.Count > 0)
                            {
                                if (hitSkrptrElements[0].GetComponent<SkrptrElement>() == null ||
                                    hitSkrptrElements[0].GetComponent<SkrptrElement>() != SkrptrMain.hoveredElem)
                                {
                                    Debug.LogError(" Touch Outside of selection");
                                    if (SkrptrMain.lastHoveredElem != null)
                                        SkrptrMain.lastHoveredElem.HoverExit();
                                    SkrptrMain.hoveredElem.HoverExit();
                                    SkrptrMain.hoveredElem = null;
                                    CancelInvoke(nameof(LongPress));
                                }
                            }
                            else
                            {
                                if (SkrptrMain.lastHoveredElem != null)
                                    SkrptrMain.lastHoveredElem.HoverExit();
                                SkrptrMain.hoveredElem.HoverExit();
                                SkrptrMain.hoveredElem = null;
                                CancelInvoke(nameof(LongPress));
                            }
                        }
                    }
                    // End the touch phase, click it.
                    if (touch.phase == TouchPhase.Ended)
                    {
                        if (SkrptrMain.hoveredElem != null)
                        {
                            // Debug.Log("Touch Ended");
                            if (SkrptrMain.lastHoveredElem != null)
                            {
                                SkrptrMain.lastHoveredElem.HoverExit();
                            }
                            SkrptrMain.hoveredElem.HoverExit();
                            SkrptrMain.hoveredElem.Click();
                            SkrptrMain.lastHoveredElem = SkrptrMain.hoveredElem;
                            SkrptrMain.hoveredElem = null;
                            CancelInvoke(nameof(LongPress));
                        }
                    }

                }
                else
                {
                    phase = touch.phase;
                }
            }
        }

    }
}