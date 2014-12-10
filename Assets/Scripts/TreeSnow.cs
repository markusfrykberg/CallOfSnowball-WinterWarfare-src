using UnityEngine;

using System.Collections;

public class TreeSnow : Enemy
{
    public int lowHealthValue;

    public override void Hit(float power, Effect e)
    {
        if (effect == null) {
            effect = e;
        } else if (e != null) {
            effect = effect.Combine(e);
        }

        health -= (int)power;
        if (health <= 0) {
            Die();
        }

        if (health <= lowHealthValue) {
            healthAnim.Play(false);
        }
    }
}
