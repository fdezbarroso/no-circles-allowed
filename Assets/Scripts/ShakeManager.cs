using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeManager : MonoBehaviour
{
    public Animator camAnim;

    public void BumpShake()
    {
        camAnim.SetTrigger("Bump");
    }
}
