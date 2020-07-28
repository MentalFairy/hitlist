using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Skrptr
{
    public class SkrptrAnimMoveToXY : SkrptrAnim
    {
        public AnimDataVector3[] animDataVector3;
        public override void Execute()
        {
            for (int i = 0; i < animDataVector3.Length; i++)
            {
                if (animDataVector3[i].target.GetComponent<RectTransform>() != null)
                {
                    RectTransform rectTf = animDataVector3[i].target.GetComponent<RectTransform>();
                    rectTf.DOAnchorPos(animDataVector3[i].targetV3, animDataVector3[i].duration).SetDelay(animDataVector3[i].delay);
                }
            }
        }
        public override void Start()
        {
            base.Start();
            animData = new List<AnimData>();
            for (int i = 0; i < animDataVector3.Length; i++)
            {
                animData.Add(new AnimData());
                animData[i].delay = animDataVector3[i].delay;
                animData[i].duration = animDataVector3[i].duration;
                animData[i].target = animDataVector3[i].target;
            }
        }
    }
}