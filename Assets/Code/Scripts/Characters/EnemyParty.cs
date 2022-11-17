using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParty : Party
{
    // Set origin and spacing
    private void Awake()
    {
        _origin = new Vector3(XPOS, YPOS, ZPOS);
        _spacing = SPACING;
    }
}
