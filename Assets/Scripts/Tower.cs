using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
    public Attack ammo;
    public float damageMult = 1;
    public float range;
    public int cost;
    public float ammoSpeed;
    public float maxCooldown;
    public int level;
    public string towerName;
    public Tower nextUpgrade;
    public int upgradeCost;
    public SpriteAnimation fireAnim;
    public string strFrate, strRange, strDamage, strUpgrades;
    protected float cooldown;
    protected GameObject target;
    protected Game game;
    protected Gui gui;
    protected SpriteRenderer spriteR;
    protected GameObject towerRange;

    public virtual void Start()
    {
        renderer.sortingOrder = (int)(-transform.position.y * 100);
        target = null;
        game = GameObject.Find("Game").GetComponent<Game>();
        gui = GameObject.Find("Gui").GetComponent<Gui>();
        spriteR = GetComponent<SpriteRenderer>();
        towerRange = GameObject.Find("TowerRange");
        cooldown = maxCooldown;
        if (fireAnim != null) {
            fireAnim.GoTo(-1);
        }
    }

    public virtual void Update()
    {
        if (cooldown > 0f) {
            cooldown -= Time.deltaTime;
        }

        if (target != null
            && Vector3.Distance(target.transform.position,
                                transform.position) > range) {
            target = null;
        }

        if (fireAnim != null) {
            spriteR.sprite = fireAnim.GetSprite();
        }

        target = FindEnemy();

        if (target != null) {
            Vector3 enemyPos = target.transform.position;
            if ((enemyPos - transform.position).x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if ((enemyPos - transform.position).x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            Vector3 enemyVel
                = target.GetComponent<Enemy>().GetVelocity();
            float enemyDist
                = Vector3.Distance(enemyPos, transform.position);
            Fire(enemyPos + enemyVel * enemyDist / ammoSpeed);
        }
    }

    public virtual void Fire(Vector3 targetPosition)
    {
        if (cooldown > 0f)
            return;

        if (!game.WavesOver() && !game.IsWaveRunning())
            return;

        if (fireAnim != null) {
            fireAnim.GoTo(0);
            fireAnim.Play(false);
        }

        Attack snowball = (Attack)Instantiate(ammo, transform.position,
                                              Quaternion.identity);

        snowball.rigidbody2D.velocity
            = (targetPosition - transform.position).normalized
            * ammoSpeed;
        snowball.hitDamage = (int)(snowball.hitDamage * damageMult);
        snowball.splashDamage = (int)(snowball.splashDamage * damageMult);
        snowball.damagePerSecond = (int)(snowball.damagePerSecond * damageMult);
        snowball.SetTtl(Vector3.Distance(targetPosition,
                                         transform.position)
                        / ammoSpeed * 1.2f);
        cooldown = maxCooldown;
    }

    protected virtual GameObject FindEnemy()
    {
        if (target != null && target.tag != "Static"
            && target.GetComponent<Enemy>().health > 0)
            return target;

        GameObject closestEnemy = FindClosest("Enemy");

        if (closestEnemy == null) {
            closestEnemy = FindClosest("Static");
        }

        return closestEnemy;
    }

    protected virtual GameObject FindClosest(string tag)
    {
        float closestDist = range + 1f;
        GameObject closest = null;
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject o in objects) {
            float dist = Vector3.Distance(o.transform.position,
                                          transform.position);
            if (dist < range && dist < closestDist
                && o.GetComponent<Enemy>().health > 0) {
                closestDist = dist;
                closest = o;
            }
        }
        return closest;
    }

    public void OnMouseDown()
    {
		if (gui.towerMenu == null)
		{
        	gui.TowerMenu(this);
		}
    }

    public void OnMouseEnter()
    {
        if (!gui.IsDragging()) {
            towerRange.GetComponent<SpriteRenderer>().color = gui.canPlaceColor;
            towerRange.transform.localScale
                = new Vector3(range, range, 1);
            towerRange.renderer.enabled = true;
            towerRange.transform.position = transform.position;
        }
    }

    public void OnMouseExit()
    {
        if (!gui.IsDragging()) {
            towerRange.renderer.enabled = false;
        }
    }
}
