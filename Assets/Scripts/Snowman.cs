using UnityEngine;

using System.Collections;

public class Snowman : Enemy
{
    public int lowHealthValue;
    public float cantPlaceRange;

    public override void Hit(float power, Effect e, Attack attk)
    {
        base.Hit(power, e, attk);

        if (health <= lowHealthValue) {
            healthAnim.Play(false);
        }
    }

    public override void Hit(float power, Effect e)
    {
        Hit(power, e, null);
    }

    public void OnMouseEnter()
    {
        if (!gui.IsDragging()) {
            GameObject towerRange = GameObject.Find("TowerRange");
            towerRange.GetComponent<SpriteRenderer>().color
                = gui.cannotPlaceColor;
            towerRange.transform.localScale
                = new Vector3(cantPlaceRange, cantPlaceRange, 1);
            towerRange.renderer.enabled = true;
            towerRange.transform.position = transform.position;
        }
    }

    public void OnMouseExit()
    {
        if (!gui.IsDragging()) {
            GameObject.Find("TowerRange").renderer.enabled = false;
        }
    }
}
