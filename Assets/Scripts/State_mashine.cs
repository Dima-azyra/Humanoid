using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_mashine : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Control>().check_fall();
    }
}
