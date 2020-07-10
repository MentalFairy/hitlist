using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skrptr
{
    public abstract class SkrptrAction : MonoBehaviour
    {
        public SkrptrEvent skrptrEvent;
        public abstract void Execute();
        protected bool isInit = false;
        public virtual void OnValidate()
        {
            if (!isInit)
            {
                if (GetComponent<SkrptrElement>() == null)
                {
                    SkrptrElement elem = gameObject.AddComponent<SkrptrElement>();
                    #if UNITY_EDITOR
                    UnityEditorInternal.ComponentUtility.MoveComponentUp(elem);
                    #endif
                }
                isInit = true;
            }
        }
    }
  
}