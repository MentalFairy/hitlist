using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skrptr_GripHold : SkrptrAction
{
    public bool isGripped = false;
    public RectTransform target;
    public Vector3 mousePos;
    public Vector2 widthHeightOfTarget;
    public float yEndValue;
    public float offset = -100;
    private void Start()
    {
        widthHeightOfTarget = target.rect.size;
    }
    public override void Execute()
    {
        isGripped = true;
    }
    private void Update()
    {
        mousePos = Input.mousePosition;

        yEndValue = Input.mousePosition.y - widthHeightOfTarget.y + offset;
        if (isGripped)
        {
            if (yEndValue > 0 && yEndValue < 400)
                target.offsetMax = new Vector2(target.offsetMax.x, yEndValue);
            #if !UNITY_EDITOR
            if (Input.touchCount == 0)
                isGripped = false;            
            #endif
        }
     
    }
}
