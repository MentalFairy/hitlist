using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

namespace Skrptr
{
    public class SkrptrAnim : MonoBehaviour
    {
        [EnumFlags]
        public SkrptrEvent skrptrEvent;
        [SerializeField]
        protected Ease ease = Ease.OutQuad;
        protected List<AnimData> animData;
        private bool isInit = false;

        private float longestAnim = -1;

        //protected void AddSelfToTargets()
        //{
        //    GameObject[] gos = new GameObject[targets.Length + 1];
        //    for (int i = 0; i < targets.Length; i++)
        //    {
        //        gos[i] = targets[i];
        //    }
        //    gos[gos.Length - 1] = gameObject;
        //    targets = gos;
        //}
        public virtual void Start()
        {
            if (skrptrEvent == SkrptrEvent.Loop)
            {
                Debug.Log("Found loop anim on " + gameObject.name);
                StartCoroutine(nameof(LoopAnimations), longestAnim);               
            }
        }
        protected IEnumerator LoopAnimations(float loopInterval)
        {            
            if (longestAnim == -1)
            {
                yield return null;
                //Debug.Log("Executing " + longestAnim + " " + animData.Count);
                longestAnim = 0;
                foreach (var item in animData)
                {
                    if (item.delay + item.duration > longestAnim)
                        longestAnim = item.delay + item.duration;
                }
            }

            InvokeRepeating(nameof(Execute), 0, longestAnim);
        }
        public virtual void Execute()
        {
        }
        //public virtual void OnValidate()
        //{
        //    if (!isInit)
        //    {
        //        if (GetComponent<SkrptrElement>() == null)
        //        {
        //            SkrptrElement elem = gameObject.AddComponent<SkrptrElement>();
        //            UnityEditorInternal.ComponentUtility.MoveComponentUp(elem);
        //        }
        //        targets = new GameObject[1] { gameObject };
        //        isInit = true;
        //    }
        //}
    }


}
