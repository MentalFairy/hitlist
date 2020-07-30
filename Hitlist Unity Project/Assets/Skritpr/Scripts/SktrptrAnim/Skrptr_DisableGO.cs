using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skrptr_DisableGO : SkrptrAnim
{
    public AnimData[] disableAnimData;
    public override void Execute()
    {
        StartCoroutine(nameof(DisableGO));
    }
    public IEnumerator DisableGO()
    {
        bool foundActiveGO;
        float timePassed = 0;
        do
        {
            timePassed += Time.deltaTime;
            foreach (var item in disableAnimData)
            {
                if (item.delay < timePassed)
                    item.target.SetActive(false);
            }
            foundActiveGO = false;
            foreach (var item in disableAnimData)
            {
                if (item.target.activeSelf)
                    foundActiveGO = true;
            }
            yield return null;

        } while (foundActiveGO);
    }
}
