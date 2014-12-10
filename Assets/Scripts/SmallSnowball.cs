using UnityEngine;
using System.Collections;

public class SmallSnowball : Attack
{
    public override void Start()
    {
        effect = new ColdEffect(damagePerSecond, 1f, 3f);
    }
}
