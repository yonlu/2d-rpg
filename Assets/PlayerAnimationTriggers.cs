using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
     private PlayerContext context => GetComponentInParent<Player>().context;

    public void AnimationTrigger()
    {
        context.AnimationFinishTrigger();
    }
}
