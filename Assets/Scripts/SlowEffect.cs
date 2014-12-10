using UnityEngine;
using System.Collections;

public class SlowEffect : Effect
{
    public SlowEffect(int dps, float spd, float dur)
    {
        damagePerSecond = dps;
        speed = spd;
        duration = dur;
    }

    public SlowEffect()
    {
        damagePerSecond = 0;
        speed = 0.5f;
        duration = 0f;
    }

    public override Effect Combine(Effect e)
    {
        return this;
    }
}
