﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMFrameRate : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
