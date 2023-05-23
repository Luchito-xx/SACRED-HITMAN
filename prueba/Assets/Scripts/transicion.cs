using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transicion : StateMachineBehaviour
{
    private Animator animator;
    
    private void Muerte()
    {
        animator.SetTrigger("Muerte");
    }
}
