﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Skrptr
{
    public class SkrptrTrigger : SkrptrAction
    {
        public TriggerTargets[] triggerTargets;

        public override void Execute()
        {
            for (int i = 0; i < triggerTargets.Length; i++)
            {
                foreach (SkrptrEvent item in Enum.GetValues(typeof(SkrptrEvent)))
                {
                    //Debug.Log(triggerTargets[i].targetGO + " " + triggerTargets[i].triggerEvent + " " + item.ToString());
                    if ((triggerTargets[i].triggerEvent & item) == item && item != SkrptrEvent.None)
                    {
                        if (triggerTargets[i].targetGO.GetComponent<SkrptrElement>() != null)
                        {
                            //Debug.LogError("Invoking:" + item.ToString() + " on : " + triggerTargets[i].targetGO.gameObject.name);
                            triggerTargets[i].targetGO.GetComponent<SkrptrElement>().Invoke(item.ToString(), triggerTargets[i].delay);
                            break;
                        }                        
                    }
                }
            }
        }

        [System.Serializable]
        public class TriggerTargets
        {
            public GameObject targetGO;
            public SkrptrEvent triggerEvent;
            public float delay = 0;
        }
    }
}
