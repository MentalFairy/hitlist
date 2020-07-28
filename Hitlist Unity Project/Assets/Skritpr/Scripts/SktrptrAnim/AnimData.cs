using UnityEngine;
namespace Skrptr
{
    [System.Serializable]
    public class AnimData
    {
        public float duration, delay;
        public GameObject target;
    }
    [System.Serializable]
    public class AnimDataColor : AnimData
    {
        public Color targetColor;     
    }
    [System.Serializable]
    public class AnimDataVector3 : AnimData
    {
        public Vector3 targetV3;
    }
    [System.Serializable]
    public class AnimDataSlideOutside : AnimData
    {        
        public SlideDirection slideDirection = SlideDirection.Up;
    }
    [System.Serializable]
    public class AnimDataGO : AnimData
    {
        public GameObject toGO;
    }
    [System.Serializable]
    public class AnimDataRotate : AnimDataVector3
    {
        public RotateType rotateType;
    }
}
