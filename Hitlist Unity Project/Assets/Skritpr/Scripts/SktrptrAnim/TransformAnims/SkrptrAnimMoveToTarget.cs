﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Skrptr
{
    public class SkrptrAnimMoveToTarget : SkrptrAnim
    {
        public AnimDataGO[] animDataGO;
        public override void Execute()
        {
            DOTween.defaultEaseType = ease;
            for (int i = 0; i < animDataGO.Length; i++)
            {            
                if(animDataGO[i].target.GetComponent<RectTransform>() !=null)
                {
                    RectTransform rectTf = animDataGO[i].target.GetComponent<RectTransform>();
                    rectTf.DOMove(animDataGO[i].toGO.transform.position, animDataGO[i].duration).SetDelay(animDataGO[i].delay);
                }
            }
        }
        public override void Start()
        {
            base.Start();
            animData = new List<AnimData>();
            for (int i = 0; i < animDataGO.Length; i++)
            {
                animData.Add(new AnimData());
                animData[i].delay = animDataGO[i].delay;
                animData[i].duration = animDataGO[i].duration;
                animData[i].target = animDataGO[i].target;
            }
        }        
    }
}