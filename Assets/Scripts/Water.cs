using UnityEngine;
using System.Collections;

public class Water : Attack
{
    public override void Start()
    {
        effect = new WetEffect(damagePerSecond, 1f, 3f);
    }
}
