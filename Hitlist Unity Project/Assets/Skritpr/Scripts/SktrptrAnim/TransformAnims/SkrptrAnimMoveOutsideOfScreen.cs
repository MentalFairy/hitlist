using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Skrptr
{
    public class SkrptrAnimMoveOutsideOfScreen : SkrptrAnim
    {
        public AnimDataSlideOutside[] animDataSlide;
        public override void Execute()
        {
            for (int i = 0; i < animDataSlide.Length; i++)
            {            
                if (animDataSlide[i].target.GetComponent<RectTransform>() != null)
                {
                    RectTransform rectTf = animDataSlide[i].target.GetComponent<RectTransform>();
                    Vector2 targetPosition;
                    switch (animDataSlide[i].slideDirection)
                    {
                        case SlideDirection.Left:
                            targetPosition = new Vector2(-rectTf.rect.size.x / 2, rectTf.position.y);
                            break;
                        case SlideDirection.Right:
                            targetPosition = new Vector2(Screen.width + rectTf.rect.size.x /2 , rectTf.position.y);
                            break;
                        case SlideDirection.Up: targetPosition = new Vector2(rectTf.position.x, Screen.height + rectTf.rect.size.y / 2);
                            break;
                        case SlideDirection.Down:
                            targetPosition =  new Vector2(rectTf.position.x, -rectTf.rect.size.y /2);
                            break;
                        default:
                            targetPosition = Vector2.zero;
                            break;
                    }
                    DOTween.defaultEaseType = ease;
                    animDataSlide[i].target.GetComponent<RectTransform>().DOMove(targetPosition, animDataSlide[i].duration).SetEase(ease).SetDelay(animDataSlide[i].delay);
                }
                else
                {
                    Debug.LogWarning("Traget : " + animDataSlide[i].target.name+ " has no RectTransform attached. " +
                        "Animation from: " + gameObject.name + "will not run.");
                }
            }
        }
        public override void Start()
        {
            base.Start();
            animData = new List<AnimData>();
            for (int i = 0; i < animDataSlide.Length; i++)
            {
                animData.Add(new AnimData());
                animData[i].delay = animDataSlide[i].delay;
                animData[i].duration = animDataSlide[i].duration;
                animData[i].target = animDataSlide[i].target;
            }
        }
    }
}
