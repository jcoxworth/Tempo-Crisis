using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class AnimParam
{
    public string paramName;
    public bool isTrue;
    public int intNumber;
    public float floatNumber;
}
public class CutsceneAnimParam : MonoBehaviour
{
    public Animator anim;
    public AnimParam[] animatedParameters;
    private void Start()
    {
        if (!anim)
            anim = GetComponent<Animator>();
    }
    private void Update()
    {
        for (int i = 0; i < animatedParameters.Length; i++)
        {

        }
    }
}
