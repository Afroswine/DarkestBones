using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Hero : Character
{
    public Sprite Portrait => _portrait;
    
    [Header("Hero")]
    [SerializeField] Sprite _portrait;
}
