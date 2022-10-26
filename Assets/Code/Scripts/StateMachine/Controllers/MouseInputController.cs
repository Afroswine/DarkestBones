using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseInputController : MonoBehaviour
{
    public event Action PressedButton = delegate { };

    public void ButtonPress()
    {
        PressedButton?.Invoke();
    }
}
