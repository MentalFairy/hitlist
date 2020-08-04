using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Skrptr
{
    public class SkrptrAnimRecolor : SkrptrAnim
    {
        public AnimDataColor[] animDataColor;
        public override void Execute()
        {
            for (int i = 0; i < animDataColor.Length; i++)
            {
                if (animDataColor[i].target.GetComponent<Image>() != null)
                {
                    animDataColor[i].target.GetComponent<Image>().DOColor(animDataColor[i].targetColor, animDataColor[i].duration).SetDelay(animDataColor[i].delay);
                }
                else if (animDataColor[i].target.GetComponent<Text>() != null)
                {
                    animDataColor[i].target.GetComponent<Text>().DOColor(animDataColor[i].targetColor, animDataColor[i].duration).SetDelay(animDataColor[i].delay);
                }
                else if (animDataColor[i].target.GetComponent<RawImage>() != null)
                {
                    animDataColor[i].target.GetComponent<RawImage>().DOColor(animDataColor[i].targetColor, animDataColor[i].duration).SetDelay(animDataColor[i].delay);
                }
            }
        }
        public override void Start()
        {
            base.Start();
            animData = new List<AnimData>();
            for (int i = 0; i < animDataColor.Length; i++)
            {
                animData.Add(new AnimData());
                animData[i].delay = animDataColor[i].delay;
                animData[i].duration = animDataColor[i].duration;
                animData[i].target = animDataColor[i].target;
            }
        }
    }
    
    
}