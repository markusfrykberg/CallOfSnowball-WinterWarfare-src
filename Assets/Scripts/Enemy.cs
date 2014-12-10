using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class Enemy : MonoBehaviour
{
    public bool deathNotice = false;
    public AudioClip death_sound;
    public int health;
    public float speed;
    public int reward;
    public int damage;
    public Vector3 velocity;
    public Vector3 direction;
    public SpriteAnimation healthAnim;
    public SpriteAnimation alwaysAnim;
    public float deathDelay = 2;
    public bool immune = false;
    protected Game game;
    protected Gui gui;
    protected Effect effect;
    protected float newSpeed = 1;
    protected SpriteRenderer spriteR;
    protected float lastDot;
    protected float deathTime;

    public virtual void Start()
    {
        game = GameObject.Find("Game").GetComponent<Game>();
        gui = GameObject.Find("Gui").GetComponent<Gui>();
        spriteR = GetComponent<SpriteRenderer>();
        direction = Vector3.down;
        lastDot = Time.time;
        if (alwaysAnim != null) {
            alwaysAnim.Play(true);
        }
        if (game.levelNumber == 2)
            health = (int)(health * 1.2);
    }

    public virtual void Update()
    {
        newSpeed = speed;
        if (effect != null) {
            newSpeed = speed * effect.speed;
            if (Time.time - lastDot >= 1f) {
                Hit(effect.damagePerSecond, null);
                lastDot = Time.time;
            }

            effect.duration -= Time.deltaTime;
            if (effect.duration <= 0f) {
                effect = null;
            }
        }

        if (velocity.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (velocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        if (deathNotice) {
            spriteR.color = new Color(1, 1, 1,
                (deathTime - Time.time) / deathDelay + 1);
        }

        if (alwaysAnim != null)
            spriteR.sprite = alwaysAnim.GetSprite();
        if (healthAnim != null)
            spriteR.sprite = healthAnim.GetSprite();

        velocity = direction * newSpeed;
        transform.position += velocity * Time.deltaTime;

        renderer.sortingOrder = (int)(-transform.position.y * 100);
    }

    public virtual void Die()
    {
        health = 0;
        game.AddMischief(reward);
        Destroy(gameObject);
    }

    public void PlaySound()
    {
        audio.PlayOneShot(death_sound, 0.7F);
    }

    public virtual void Hit(float power, Effect e, Attack attk)
    {
        if (effect == null) {
            effect = e;
        } else if (e != null && !immune) {
            effect = effect.Combine(e);
        }

        health -= (int)power;
        if (health <= 0 && deathNotice == false) {
            PlaySound();
            direction = Vector3.zero;
            deathNotice = true;
            deathTime = Time.time;
            if (alwaysAnim != null)
                alwaysAnim.Pause();
            Invoke("Die", deathDelay);
          }

        if (attk != null) {
            audio.PlayOneShot(attk.hitclip);
        }
    }

    public virtual void Hit(float power, Effect e)
    {
        Hit(power, e, null);
    }

    public void OnDestroy()
    {
        if (game != null)
            game.EnemyDestroyed();
    }

    public virtual Vector3 GetVelocity()
    {
        return velocity;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "TurnWest"){
            direction = Vector3.left;
        }
        if(other.gameObject.tag == "TurnSouth"){
            direction = Vector3.down;
        }
        if(other.gameObject.tag == "TurnEast"){
            direction = Vector3.right;
        }
        if(other.gameObject.tag == "TurnNorth"){
            direction = Vector3.up;
        }
    }

    public Effect GetEffect()
    {
        return effect;
    }
}
