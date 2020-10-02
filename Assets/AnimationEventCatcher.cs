using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCatcher : MonoBehaviour
{
    public void DidLightMeleeAttack()
    {
        Debug.Log("DidLightMeleeAttack");
    }

    public void DidHeavyMeleeAttack()
    {
        Debug.Log("DidHeavyMeleeAttack");
    }
}
