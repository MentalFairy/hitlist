using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skrptr_EnableGO : SkrptrAnim
{
    public AnimData[] enableAnimData;
    public override void Execute()
    {
        StartCoroutine(nameof(DisableGO));
    }
    public IEnumerator DisableGO()
    {
        bool foundInactiveGo;
        float timePassed = 0;
        do
        {
            timePassed += Time.deltaTime;
            foreach (var item in enableAnimData)
            {
                if (item.delay < timePassed)
                    item.target.SetActive(true);
            }
            foundInactiveGo = false;
            foreach (var item in enableAnimData)
            {
                if (!item.target.activeSelf)
                    foundInactiveGo = true;
            }
            yield return null;

        } while (foundInactiveGo);
    }
}
