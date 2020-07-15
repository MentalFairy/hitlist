using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace Skrptr
{
    public class SkrptrAnimResizeToXYZ : SkrptrAnim
    {
        public AnimDataVector3[] animDataV3;
        public override void Execute()
        {
            for (int i = 0; i < animDataV3.Length; i++)
            {            
                if (animDataV3[i].target.GetComponent<RectTransform>() != null)
                {
                    RectTransform rectTf = animDataV3[i].target.GetComponent<RectTransform>();
                    rectTf.DOScale(animDataV3[i].targetV3, animDataV3[i].duration).SetDelay(animDataV3[i].delay);
                }
            }
        }
        public override void Start()
        {
            base.Start();
            animData = new List<AnimData>();
            for (int i = 0; i < animDataV3.Length; i++)
            {
                animData.Add(new AnimData());
                animData[i].delay = animDataV3[i].delay;
                animData[i].duration = animDataV3[i].duration;
                animData[i].target = animDataV3[i].target;
            }
        }
    }
    
}