using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Skrptr
{
    public class SkrptrKeyboardMapper : MonoBehaviour
    {
        public List<SkrptrNeighbour> neighbours;

        private void Start()
        {
            foreach (var item in neighbours)
            {
                Debug.Log(item.direction.ToString() + " " + (int)item.direction + " ");
            }
            foreach (SkrptrDirection direction in Enum.GetValues(typeof(SkrptrDirection)))
            {
                if (direction != SkrptrDirection.None)
                {
                    int foundCount = 0;
                    for (int i = 0; i < neighbours.Count; i++)
                    {
                        if ((neighbours[i].direction & direction) == direction)
                        {
                            foundCount++;
                            if (foundCount >= 2)
                            {
                                neighbours[i].direction = neighbours[i].direction ^ direction;
                                Debug.LogWarning("Element: " + gameObject.name + " has been found having two different targetting neighoburs for direction : " + direction.ToString() + " on index: " + i + ". Removing duplicate.");
                            }
                        }
                    }
                }
            }
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                foreach (var item in neighbours)
                {
                    Debug.Log(item.direction.ToString() + " " + (int)item.direction + " ");
                }
            }
        }
    }
    [System.Serializable]
    public class SkrptrNeighbour
    {
        [EnumToggle]
        public SkrptrDirection direction;
        public GameObject target;

        public SkrptrNeighbour(SkrptrDirection direction, GameObject target)
        {
            this.direction = direction;
            this.target = target;
        }
    }
}