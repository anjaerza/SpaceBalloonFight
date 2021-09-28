using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAnimation : MonoBehaviour
{
    Animator _animFirstAgua;
    private void OnEnable()
    {
        _animFirstAgua = gameObject.GetComponent<Animator>();
        _animFirstAgua.SetBool("FirstWaterAnimation", true);
    }
}
