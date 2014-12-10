using UnityEngine;
using System.Collections;

public class ColdEffect : Effect
{
    public ColdEffect(int dps, float spd, float dur)
    {
        damagePerSecond = dps;
        speed = spd;
        duration = dur;
    }

    public override Effect Combine(Effect e)
    {
        if (e.GetType() == typeof(WetEffect)) {
            return new SlowEffect(0, 0.5f, 5f);
        } else if (e.GetType() == typeof(ColdEffect)) {
            return new ColdEffect(damagePerSecond, 1, 5f);
        }
        return this;
    }
}
