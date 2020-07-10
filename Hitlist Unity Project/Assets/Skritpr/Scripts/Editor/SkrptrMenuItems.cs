using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace Skrptr
{
    public static class SkrptrMenuItems
    {
        [MenuItem("Skrptr/Extra/Auto-Anchor %h")]
        private static void AutoAnchor()
        {
            RectTransform t = Selection.activeTransform as RectTransform;
            RectTransform pt = Selection.activeTransform.parent as RectTransform;

            if (t == null || pt == null) return;

            if (pt.rect.width != 0 && pt.rect.height != 0)
            {
                Vector2 newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
                    t.anchorMin.y + t.offsetMin.y / pt.rect.height);
                Vector2 newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
                    t.anchorMax.y + t.offsetMax.y / pt.rect.height);

                t.anchorMin = newAnchorsMin;
                t.anchorMax = newAnchorsMax;
                t.offsetMin = t.offsetMax = new Vector2(0, 0);
            }
        }
        [MenuItem("Skrptr/Neighbours/Neighbour horizontally")]
        private static void SetNeighboursHorizontally()
        {
            Transform[] tfs = Selection.transforms.OrderBy(tf => tf.gameObject.name).ToArray();
            SkrptrKeyboardMapper[] mappers = new SkrptrKeyboardMapper[tfs.Length];
            for (int i = 0; i < tfs.Length; i++)
            {
                if (tfs[i].GetComponent<SkrptrKeyboardMapper>() == null)
                {
                    tfs[i].gameObject.AddComponent<SkrptrKeyboardMapper>();
                    mappers[i] = tfs[i].GetComponent<SkrptrKeyboardMapper>();
                }
                else
                {
                    mappers[i] = tfs[i].GetComponent<SkrptrKeyboardMapper>();
                }
                if (mappers[i].neighbours == null)
                    mappers[i].neighbours = new List<SkrptrNeighbour>();
            }

            for (int i = 0; i < mappers.Length; i++)
            {
                Debug.Log(mappers[i].gameObject.name);
            }
            mappers[0].neighbours.Add(new SkrptrNeighbour(SkrptrDirection.Right, mappers[1].gameObject));
            mappers[mappers.Length - 1].neighbours.Add(new SkrptrNeighbour(SkrptrDirection.Left, mappers[mappers.Length - 2].gameObject));
            for (int i = 1; i < mappers.Length-1; i++)
            {
                mappers[i].neighbours.Add(new SkrptrNeighbour(SkrptrDirection.Right, mappers[i + 1].gameObject));
                mappers[i].neighbours.Add(new SkrptrNeighbour(SkrptrDirection.Left, mappers[i - 1].gameObject));
            }
        }

        [MenuItem("Skrptr/Neighbours/Neighbour Vertically ")]
        private static void SetNeighboursVertically()
        {
            Transform[] tfs = Selection.transforms.OrderBy(tf => tf.gameObject.name).ToArray();
            SkrptrKeyboardMapper[] mappers = new SkrptrKeyboardMapper[tfs.Length];
            for (int i = 0; i < tfs.Length; i++)
            {
                if (tfs[i].GetComponent<SkrptrKeyboardMapper>() == null)
                {
                    tfs[i].gameObject.AddComponent<SkrptrKeyboardMapper>();
                    mappers[i] = tfs[i].GetComponent<SkrptrKeyboardMapper>();
                }
                else
                {
                    mappers[i] = tfs[i].GetComponent<SkrptrKeyboardMapper>();
                }
                if (mappers[i].neighbours == null)
                    mappers[i].neighbours = new List<SkrptrNeighbour>();
            }

            for (int i = 0; i < mappers.Length; i++)
            {
                Debug.Log(mappers[i].gameObject.name);
            }
            mappers[0].neighbours.Add(new SkrptrNeighbour(SkrptrDirection.Down, mappers[1].gameObject));
            mappers[mappers.Length - 1].neighbours.Add(new SkrptrNeighbour(SkrptrDirection.Up, mappers[mappers.Length - 2].gameObject));
            for (int i = 1; i < mappers.Length - 1; i++)
            {
                mappers[i].neighbours.Add(new SkrptrNeighbour(SkrptrDirection.Down, mappers[i + 1].gameObject));
                mappers[i].neighbours.Add(new SkrptrNeighbour(SkrptrDirection.Up, mappers[i - 1].gameObject));
            }
        }
    }
}