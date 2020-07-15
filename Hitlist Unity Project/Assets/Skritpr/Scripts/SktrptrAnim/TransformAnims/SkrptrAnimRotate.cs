using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace Skrptr
{
    public class SkrptrAnimRotate : SkrptrAnim
    {      
        public AnimDataRotate[] animDataRotate;
        public override void Execute()
        {
            for (int i = 0; i < animDataRotate.Length; i++)
            {
                if (animDataRotate[i].target.GetComponent<RectTransform>() != null)
                {
                    RectTransform rectTf = animDataRotate[i].target.GetComponent<RectTransform>();
                    if (animDataRotate[i].rotateType == RotateType.Absolute)
                    {
                        rectTf.DORotate(animDataRotate[i].targetV3, animDataRotate[i].duration, RotateMode.FastBeyond360).SetDelay(animDataRotate[i].delay);
                    }
                    else
                    {
                        Vector3 rotateTo = rectTf.eulerAngles + animDataRotate[i].targetV3;
                        rectTf.DORotate(rotateTo, animDataRotate[i].duration, RotateMode.FastBeyond360).SetDelay(animDataRotate[i].delay);
                    }
                }
            }
        }
        public override void Start()
        {
            base.Start();
            animData = new List<AnimData>();
            for (int i = 0; i < animDataRotate.Length; i++)
            {
                animData.Add(new AnimData());
                animData[i].delay = animDataRotate[i].delay;
                animData[i].duration = animDataRotate[i].duration;
                animData[i].target = animDataRotate[i].target;
            }
        }
    }
}