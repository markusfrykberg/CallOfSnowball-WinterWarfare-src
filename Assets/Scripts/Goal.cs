using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    public AudioClip EndBell;

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy") {
            GameObject.Find("Game").GetComponent<Game>()
                .AddHealth(-coll.gameObject.GetComponent<Enemy>().damage);
            audio.PlayOneShot(EndBell);
            Destroy(coll.gameObject);
        }
    }
}
