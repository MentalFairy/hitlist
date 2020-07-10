using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Skrptr
{
    public class SkrptrElement : MonoBehaviour
    {
        private SkrptrAnim[] anims;
        private SkrptrAction[] actions;
        private SkrptrKeyboardMapper keyboardMapper;
        [HideInInspector]
        public bool isLocked = false;
        public SkrptrEvent runInitEvents;
        private void Awake()
        {
            actions = GetComponents<SkrptrAction>();
            anims = GetComponents<SkrptrAnim>();
            if(GetComponent<SkrptrKeyboardMapper>() !=null)
            {
                keyboardMapper = GetComponent<SkrptrKeyboardMapper>();
            }
        }
        public virtual void Start()
        {
            foreach (SkrptrEvent skrptrEvent in Enum.GetValues(typeof(SkrptrEvent)))
            {
                if (skrptrEvent != SkrptrEvent.None)
                {
                    if ((runInitEvents & skrptrEvent) == skrptrEvent)
                    {
                        Debug.Log("Running " + skrptrEvent + " on " + gameObject.name);
                        Invoke(skrptrEvent.ToString(), 0);
                    }
                }
            }
        }

        public virtual void Select()
        {
            //Debug.Log("Selecting: " + gameObject.name);
            ExecuteAnims(SkrptrEvent.Select);
        }
        public virtual void Deselect()
        {
            //Debug.Log("Deselect: " + gameObject.name);
            ExecuteAnims(SkrptrEvent.Deselect);
        }
        public virtual void Enable()
        {
            gameObject.SetActive(true);
            ExecuteAnims(SkrptrEvent.Enable);            
        }
        public virtual void Disable()
        {
            ExecuteAnims(SkrptrEvent.Disable);
            Invoke(nameof(DisableMe),GetLongestAnimAction(SkrptrEvent.Disable));
        }
        public virtual void Hide()
        {
            //Debug.Log("Hide: " + gameObject.name);
            ExecuteAnims(SkrptrEvent.Hide);
        }
        public virtual void Show()
        {
            //Debug.Log("Show: " + gameObject.name);
            ExecuteAnims(SkrptrEvent.Show);
        }
        public virtual void Lock()
        {
            DisableRaycastTarget();
            isLocked = true;
            ExecuteAnims(SkrptrEvent.Lock);
            
        }
        public virtual void Unlock()
        {
            //Debug.Log("Unlock: " + gameObject.name);
            ExecuteAnims(SkrptrEvent.Unlock);
            Invoke(nameof(EnableRaycastTarget), GetLongestAnimAction(SkrptrEvent.Unlock));
        }
        public virtual void Click()
        {
            //Debug.Log("Click: " + gameObject.name);
            ExecuteAnims(SkrptrEvent.Click);
        }
        public virtual void HoverEnter()
        {
            //Debug.Log("HoverEnter: " + gameObject.name);
            ExecuteAnims(SkrptrEvent.HoverEnter);
        }
        public virtual void HoverExit()
        {
            //Debug.Log("HoverExit: " + gameObject.name);
            ExecuteAnims(SkrptrEvent.HoverExit);
        }
        public virtual void Check()
        {
            //Debug.Log("Check: " + gameObject.name);
            ExecuteAnims(SkrptrEvent.Check);
        }
        public virtual void Uncheck()
        {
            //Debug.Log("Uncheck: " + gameObject.name);
            ExecuteAnims(SkrptrEvent.Uncheck);
        }
        protected void ExecuteAnims(SkrptrEvent currentSkrptrEvent)
        {
            foreach (var action in anims)
            {
                if ((action.skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    action.Execute();
                }
            }
            ExecuteActions(currentSkrptrEvent);
        }
        protected void ExecuteActions(SkrptrEvent currentSkrptrEvent)
        {
            foreach (var action in actions)
            {
                if ((action.skrptrEvent & currentSkrptrEvent) == currentSkrptrEvent)
                {
                    action.Execute();
                }
            }
        }

        private float GetLongestAnimAction(SkrptrEvent skrptrEvent)
        {
            float maxDuration = 0;
            foreach (var anim in anims)
            {
                if ((anim.skrptrEvent & skrptrEvent) == skrptrEvent)
                {
                    //if (anim.TotalAnimationTime > maxDuration)
                    //    maxDuration = anim.TotalAnimationTime;
                }
            }
            return maxDuration;
        }
        private void DisableMe()
        {           
            gameObject.SetActive(false);
        }
        private void DisableRaycastTarget()
        {
            if (GetComponent<Image>() != null)
            {
                GetComponent<Image>().raycastTarget = false;
            }
            if (GetComponent<Text>() != null)
            {
                GetComponent<Text>().raycastTarget = false;
            }
        }
        private void EnableRaycastTarget()
        {
            if (GetComponent<Image>() != null)
            {
                GetComponent<Image>().raycastTarget = true;
            }
            if (GetComponent<Text>() != null)
            {
                GetComponent<Text>().raycastTarget = true;
            }
            isLocked = false;
        }
    }
}