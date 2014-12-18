using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    public AudioClip hitclip;
    public int hitDamage;
    public int damagePerSecond;
    public int splashDamage;
    public float splashRadius;
    public Effect effect;
    public GameObject explosion;
    protected Vector3 target;
    protected float timeToLive;

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
        renderer.sortingOrder = (int)(-transform.position.y * 100);
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0f) {
            Destroy(gameObject);
        }
    }

    public virtual void Explode()
    {
        if (explosion != null) {
            GameObject explobj = (GameObject)
                Instantiate(explosion, transform.position,
                            Quaternion.identity);
            explobj.transform.Translate(Vector3.up * 0.3f);
            Destroy(explobj, explobj.GetComponent<ParticleSystem>()
                    .duration);
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies) {
            float dist = Vector3.Distance(e.transform.position,
                                          transform.position);
            if (dist < splashRadius) {
                e.GetComponent<Enemy>().Hit(splashDamage, effect);
            }
        }

        GameObject[] statics = GameObject.FindGameObjectsWithTag("Static");
        foreach (GameObject e in statics) {
            float dist = Vector3.Distance(e.transform.position,
                                          transform.position);
            if (dist < splashRadius) {
                e.GetComponent<Enemy>().Hit(splashDamage, effect);
            }
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Static") {
            coll.gameObject.GetComponent<Enemy>().Hit(hitDamage, effect, this);
			Explode();
			Destroy(gameObject);
        }
		//if(coll.gameObject.tag != "BackGround"){
//       		Explode();
//       	 	Destroy(gameObject);
		//}
    }


    public void SetTtl(float ttl)
    {
        timeToLive = ttl;
    }
}
