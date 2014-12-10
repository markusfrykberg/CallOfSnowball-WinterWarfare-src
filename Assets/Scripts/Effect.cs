using UnityEngine;
using System.Collections;

public class Effect
{
    public int damagePerSecond;
    public float speed;
    public float duration;

    public Effect(int dps, float spd, float dur)
    {
    }

    public Effect()
    {
    }

    public virtual Effect Combine(Effect e)
    {
        return this;
    }
}
