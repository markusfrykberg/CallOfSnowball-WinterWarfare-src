using UnityEngine;
using System.Collections;

public class WaterTower : Tower
{
    protected override GameObject FindEnemy()
    {
        GameObject bestEnemy = null
                 , otherEnemy = null
                 , worstEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies) {
            Effect eff = e.GetComponent<Enemy>().GetEffect();
            if (Vector3.Distance(e.transform.position,
                                 transform.position) >= range
                || e.GetComponent<Enemy>().health <= 0) {
            } else if (eff == null) {
                otherEnemy = e;
            } else if (eff.GetType() == typeof(ColdEffect)) {
                bestEnemy = e;
                break;
            } else {
                worstEnemy = e;
            }
        }

        if (bestEnemy != null)
            return bestEnemy;
        else if (otherEnemy != null)
            return otherEnemy;
        else if (worstEnemy != null)
            return worstEnemy;
        else
            return FindClosest("Static");
    }
}
