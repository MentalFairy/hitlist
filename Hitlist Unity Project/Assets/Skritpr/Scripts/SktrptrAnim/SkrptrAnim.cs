using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
          // Load data into animsData
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
