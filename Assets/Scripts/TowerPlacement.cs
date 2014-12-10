using UnityEngine;
using System.Collections;

public class TowerPlacement : MonoBehaviour
{
    public float minTowerDistance;
    public float minStaticDistance;
    private Gui gui;
    private Game game;

    public void Start()
    {
        gui = GameObject.Find("Gui").GetComponent<Gui>();
        game = GameObject.Find("Game").GetComponent<Game>();
    }

    public void OnMouseDown()
    {
        Tower t = gui.StopDrag();
        if (t != null) {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            if (CanPlace(pos) && game.AddMischief(-t.cost)) {
                Instantiate(t, pos, Quaternion.identity);
            } else {
                gui.StartDrag(t);
            }
        }
    }

    public bool CanPlace(Vector3 pos)
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject t in towers) {
            if (Vector3.Distance(pos, t.transform.position) < minTowerDistance) {
                return false;
            }
        }
        GameObject[] statics = GameObject.FindGameObjectsWithTag("Static");
        foreach (GameObject s in statics) {
            if (Vector3.Distance(pos, s.transform.position)
                < s.GetComponent<Snowman>().cantPlaceRange) {
                return false;
            }
        }
        return true;
    }

    public void OnMouseOver()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        gui.SetCanPlace(gui.IsDragging()
            && CanPlace(mousePos));
    }

    public void OnMouseExit()
    {
        gui.SetCanPlace(false);
    }
}
