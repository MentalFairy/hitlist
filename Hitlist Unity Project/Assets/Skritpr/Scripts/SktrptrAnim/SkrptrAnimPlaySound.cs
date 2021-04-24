using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkrptrAnimPlaySound : SkrptrAnim
{
    public List<AnimData> animDataAudio;
    public override void Execute()
    {
        if (Time.realtimeSinceStartup > 6)
        {
            for (int i = 0; i < animDataAudio.Count; i++)
            {
                if (animDataAudio[i].target.GetComponent<AudioSource>() != null)
                {
                    animDataAudio[i].target.GetComponent<AudioSource>().PlayDelayed(animDataAudio[i].delay);
                    //Debug.Log("Playing: " + gameObject.name);
                }
            }
        }
    }
}
