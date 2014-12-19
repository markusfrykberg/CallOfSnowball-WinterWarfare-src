using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    public GUIStyle buttonStyle;
    public GUIStyle textStyle;
    public string firstLevelName;
    public GUIStyle startStyle;
    public GUIStyle creditsStyle;
    public GUIStyle quitStyle;
    public GUIStyle exitStyle;
    public Sprite bg1, bg2;
    private int sHeight;
    private int sWidth;
    private float screenf;
    private bool credits = false;
    private SpriteRenderer background;

    public void Start()
    {
        background = GameObject.Find("Background").GetComponent<SpriteRenderer>();
    }

    public void OnGUI()
    {
        sHeight = 600;
        sWidth = sHeight * Screen.width / Screen.height;
        screenf = Screen.height / (float)sHeight;
        GUIUtility.ScaleAroundPivot(new Vector2(screenf, screenf),
                                    Vector2.zero);

        if (credits) {
            Credits();
        } else {
            MainMenu();
        }
    }

    private void MainMenu()
    {
        background.sprite = bg1;
        float buttonWidth = 270 * 0.6f;
        float buttonHeight = 120 * 0.6f;
        GUI.BeginGroup(new Rect(sWidth / 2 - buttonWidth / 2, 300,
                                buttonWidth, buttonHeight * 4));

        if (GUI.Button(new Rect(0, 0, buttonWidth, buttonHeight),
                                "", startStyle)) {
			Application.LoadLevel("WorldMap");
        }
		if (GUI.Button(new Rect(0, buttonHeight, buttonWidth, buttonHeight),
		               "", startStyle)) {
			PlayerPrefs.SetInt("LevelTracker",0);
			Application.LoadLevel("WorldMap");
		}

        if (GUI.Button(new Rect(0, buttonHeight*2, buttonWidth, buttonHeight),
                                "", creditsStyle)) {
            credits = true;
        }

        if (GUI.Button(new Rect(0, buttonHeight * 3, buttonWidth, buttonHeight),
                                "", quitStyle)) {
            Application.Quit();
        }

        GUI.EndGroup();
    }

    private void Credits()
    {
        background.sprite = bg2;
        float buttonWidth = 280 * 0.6f;
        float buttonHeight = 100 * 0.6f;
        float width = 800;
        GUI.BeginGroup(new Rect(sWidth / 2 - width / 2, 250,
                                width, 500));
        GUI.Label(new Rect(0, 0, width, 20),
                  "Patricia \"SpriteMaster\" Möllerström \n" +
                  "Advanced assistant Audiomaster Manager Oliver Engström \n" +
                  "Daniel \"PörkeMaster\" Egerström \n" +
                  "Robert \"MasterProgrammer\" Alm \n" +
                  "\n" +
                  "All sound assets belong to their respective owners under the\n" +
                  "creative commons license\n\n" +
                  "Background music created by Kevin MacLeod @incompetech.com",
                  textStyle);
        if (GUI.Button(new Rect(0, 270, buttonWidth, buttonHeight),
                        "", exitStyle)) {
            credits = false;
        }
        GUI.EndGroup();
    }
}
