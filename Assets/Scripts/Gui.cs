using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour
{	
    public Tower regularTower;
    public Texture2D regularTowerTexture;
    public Tower rapidTower;
    public Texture2D rapidTowerTexture;
    public Tower waterTower;
    public Texture2D waterTowerTexture;
    public Tower catapultTower;
    public Texture2D catapultTowerTexture;
    public Texture2D defeatTexture, victoryTexture, pauseTexture;
	public Texture2D pauseTextTexture;
    public GUIStyle buttonStyle;
    public GUIStyle buttonDisbStyle;
    public GUIStyle startWaveStyle;
    public GUIStyle towerButtonStyle;
    public GUIStyle textStyle;
    public GUIStyle textCenterStyle;
    public GUIStyle mischiefStyle;
    public GUIStyle boxStyle;
    public GUIStyle noBgBoxStyle;
    public GUIStyle towerInfoStyle;
    public GUIStyle towerMenuTitleStyle;
    public GUIStyle startWaveInfoStyle;
    public Texture2D audioOnTexture, audioOffTexture;
    public Texture2D waveTexture;
    public Texture2D gameOverTexture;
    public Texture2D victoryTextTexture;
    public GameObject cursorSprite;
    public Color canPlaceColor;
    public Color cannotPlaceColor;
    public Texture2D[] healthMeters;
    public string startMenuName;
    public Color disbColor;
    public Color enblColor;
    private int sWidth;
    private int sHeight;
    private float screenf;
    private Tower dragging;
    private Texture2D cursorTexture;
    private Game game;
    private GameObject towerRange;
    private Tower towerMenu;
    private bool canPlace;
    private bool winWindow = false;
    private bool loseWindow = false;
	private bool pauseWindow=false;
    public void Start()
    {
        game = GameObject.Find("Game").GetComponent<Game>();
        towerRange = GameObject.Find("TowerRange");
        towerMenu = null;

    }

	public void Update(){


		// Hotkeys for builder
		if (Input.GetButtonDown("Regular") && game.mischief >= regularTower.cost)
		   StartDrag(regularTower);
		if (Input.GetButtonDown("Water")&& game.mischief >= waterTower.cost)
			StartDrag(waterTower);
		if (Input.GetButtonDown("Rapid")&& game.mischief >= rapidTower.cost)
			StartDrag(rapidTower);
		if (Input.GetButtonDown("Catapult")&& game.mischief >= catapultTower.cost)
			StartDrag(catapultTower);

		// hotkey for upgradeTower
		if (Input.GetButtonDown ("Upgrade") && towerMenu)
						UpgradeTower ();
		// hotkey for sellTower
		if (Input.GetButtonDown ("Sell") && towerMenu)
						SellTower ();
		//hotkey for close
		if (Input.GetButtonDown ("Close") && towerMenu)
						towerMenu = null;

		// Pause meny
		if (Input.GetButtonDown ("Escape"))
						pauseWindow = !pauseWindow;


		}

    public void StartDrag(Tower tower)
    {
        if (dragging != null && dragging.GetType() == tower.GetType()) {
            StopDrag();
            return;
        }

        towerRange.renderer.enabled = true;
        towerRange.transform.localScale
            = new Vector3(tower.range, tower.range, 1);

        dragging = tower;
        cursorSprite.GetComponent<SpriteRenderer>().sprite
            = tower.GetComponent<SpriteRenderer>().sprite;
    }

    public Tower StopDrag()
    {
        Tower t = dragging;
        towerRange.renderer.enabled = false;
        dragging = null;
        cursorSprite.GetComponent<SpriteRenderer>().sprite = null;
        return t;
    }

    public void OnGUI()
    {

		if (pauseWindow) {

						DrawPauseWindow ();
						Time.timeScale = 0;
				} 
		else
						Time.timeScale = 1;
        sHeight = 600;
        sWidth = sHeight * Screen.width / Screen.height;
        screenf = Screen.height / (float)sHeight;
        GUIUtility.ScaleAroundPivot(new Vector2(screenf, screenf),
                                    Vector2.zero);

        Rect rect = new Rect(10, 10, 60, 60);
        if (rect.Contains(Event.current.mousePosition)) {
            GUI.backgroundColor = new Color(1, 1, 1, 0.8f);
            GUI.Box(new Rect(rect.x + 60, rect.y + 10, 150, 35),
                    "Start wave", startWaveInfoStyle);
            GUI.backgroundColor = new Color(1, 1, 1, 1);
        }
        if (game.IsWaveRunning())
            GUI.color = disbColor;
        else
            GUI.color = enblColor;
        if (GUI.Button(rect, waveTexture, startWaveStyle)) {
            game.NewWave();
        }
        GUI.color = enblColor;

        TowerButton(10, 70, regularTower, regularTowerTexture);
        TowerButton(10, 130, waterTower, waterTowerTexture);
        TowerButton(10, 190, rapidTower, rapidTowerTexture);
        TowerButton(10, 250, catapultTower, catapultTowerTexture);

        Texture2D audioTexture;
        if (game.IsAudioOn()) {
            audioTexture = audioOnTexture;
        } else {
            audioTexture = audioOffTexture;
        }
        if (GUI.Button(new Rect(sWidth - 70, 10, 60, 60),
                       audioTexture, textStyle)) {
            game.ToggleAudio();
        }

        GUI.Label(new Rect(20, sHeight - 125, 210, 80),
                  game.mischief.ToString(), mischiefStyle);

        GUI.DrawTexture(new Rect(12, sHeight - 90, 210, 80),
                        healthMeters[(int)Mathf.Ceil((healthMeters.Length-1)
                                     * game.health / game.maxHealth)]);

        if (towerMenu != null) {
            DrawTowerMenu();
        }


        if (IsDragging()) {
            if (canPlace) {
                towerRange.GetComponent<SpriteRenderer>().color
                    = canPlaceColor;
            } else {
                towerRange.GetComponent<SpriteRenderer>().color
                    = cannotPlaceColor;
            }
            Vector3 realMousePos
                = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            realMousePos.z = 0;
            cursorSprite.transform.position = realMousePos;
            towerRange.transform.position = realMousePos;
        }

        if (winWindow) {
            DrawWinWindow();
        }
        if (loseWindow) {
            DrawLoseWindow();
        }
    }


    public void TowerButton(float x, float y, Tower t, Texture2D image)
    {
        if (game.mischief < t.cost)
            GUI.color = disbColor;
        else
            GUI.color = enblColor;
        Rect rect = new Rect(x, y, 60, 60);
        if (GUI.Button(rect, image, towerButtonStyle) ) {
            if (game.mischief >= t.cost) {
                StartDrag(t);
            }
        }
        GUI.color = enblColor;
        if (rect.Contains(Event.current.mousePosition)) {
            TowerInfo(t, rect.x + 55, rect.y + 0);
        }
    }

    private void TowerInfo(Tower t, float x, float y)
    {
        float menuWidth = 165;
        float menuHeight = 160;
        GUI.backgroundColor = new Color(1, 1, 1, 0.8f);
        GUI.BeginGroup(new Rect(x, y, menuWidth, menuHeight));
        GUI.Box(new Rect(0, 0, menuWidth, menuHeight),
                t.towerName + " (" + t.cost + ")", towerInfoStyle);
        GUI.Label(new Rect(9, 40, menuWidth, 20),
                  t.strFrate + "\n" +
                  t.strRange + "\n" +
                  t.strDamage, textStyle);
        GUI.Label(new Rect(9, 106, menuWidth, 20),
                  "Upgrades:\n" + t.strUpgrades, textStyle);
        GUI.EndGroup();
        GUI.backgroundColor = new Color(1, 1, 1, 1);
    }

    public void SetWinWindow(bool b)
    {
        winWindow = b;
    }

    public void SetLoseWindow(bool b)
    {
        loseWindow = b;
    }

    public void TowerMenu(Tower t)
    {
        towerMenu = t;
    }

    public bool IsDragging()
    {
        return dragging != null;
    }

    public void SetCanPlace(bool b)
    {
        canPlace = b;
    }

    private void DrawTowerMenu()
    {
        int menuWidth = 150;
        Vector3 v = towerMenu.transform.position;
        Vector3 menuPos = Camera.main.WorldToScreenPoint(v) / screenf;
        menuPos.y = sHeight - menuPos.y - 20;
        menuPos.x -= menuWidth / 2;
        GUI.BeginGroup(new Rect(menuPos.x, menuPos.y, menuWidth, 120));
        GUI.Label(new Rect(0, 0, menuWidth, 40), towerMenu.towerName,
                towerMenuTitleStyle);

        GUIStyle upgradeButtonStyle;
        if (game.mischief < towerMenu.upgradeCost) {
            upgradeButtonStyle = buttonDisbStyle;
        } else {
            upgradeButtonStyle = buttonStyle;
        }
        if (towerMenu.nextUpgrade == null) {
						GUI.Button (new Rect (0, 30, 150, 30),
                       "No more upgrades", buttonDisbStyle);
				} else if (GUI.Button (new Rect (0, 30, menuWidth, 30),
                              "Upgrade (" + towerMenu.upgradeCost + ")",
                              upgradeButtonStyle)) {
						UpgradeTower ();
				}

        if (towerMenu != null && GUI.Button(new Rect(0, 60, menuWidth, 30),
                                            "Sell (" + towerMenu.cost + ")",
                                            buttonStyle)) {
			SellTower();
//            game.AddMischief(towerMenu.cost);
//            Destroy(towerMenu.gameObject);
//            towerMenu = null;
        }
        if (towerMenu != null
            && GUI.Button(new Rect(0, 90, menuWidth, 30), "Close",
                          buttonStyle)) {
            towerMenu = null;
        }
        GUI.EndGroup();
    }

    private void DrawWinWindow()
    {
        float menuWidth = 330;
        float menuHeight = 380;
        GUI.Box(new Rect(0, 0, sWidth, sHeight), "", boxStyle);
        GUI.BeginGroup(new Rect(sWidth / 2 - menuWidth / 2,
                                sHeight / 2 - menuHeight / 2,
                                menuWidth, menuHeight));
        GUI.Box(new Rect(0, 0, menuWidth, menuHeight),
                "", noBgBoxStyle);
        GUI.DrawTexture(new Rect(menuWidth / 2f - 355f * 0.5f / 2f,
                                 120,
                                 355f * 0.5f, 499f * 0.5f), victoryTexture);
        GUI.DrawTexture(new Rect(0, 0, menuWidth, menuWidth / 750 * 148),
                        victoryTextTexture);
	
        GUI.Label(new Rect(0, 70, menuWidth, 20),
                  "Level Score: " + game.GetScore(), textCenterStyle);
		GUI.Label(new Rect(0, 100, menuWidth, 20),
		          "Total Score: " + (game.score + game.GetScore()), textCenterStyle);

        float buttonWidth = 110;
        if (GUI.Button(new Rect(menuWidth / 2 - buttonWidth - 5,
                                menuHeight - 40, buttonWidth, 30),
                       "Main menu", buttonStyle)) {
            Application.LoadLevel(startMenuName);
        }
        if (game.nextLevelName == "") {
            GUI.Button(new Rect(menuWidth / 2 + 5,
                               menuHeight - 40, buttonWidth, 30),
                      "No more levels", buttonDisbStyle);
        } else {
            if (GUI.Button(new Rect(menuWidth / 2 + 5,
                                    menuHeight - 40, buttonWidth, 30),
                           "Next level", buttonStyle)) {
				PlayerPrefs.SetInt("CurrentTotalScore",game.score+game.GetScore());
				PlayerPrefs.SetInt ("LevelTracker", Application.loadedLevel);
				Application.LoadLevel ("WorldMap");
            }
        }
        GUI.EndGroup();
    }

    private void DrawLoseWindow()
    {
        float menuWidth = 330;
        float menuHeight = 380;
        GUI.Box(new Rect(0, 0, sWidth, sHeight), "", boxStyle);
        GUI.BeginGroup(new Rect(sWidth / 2 - menuWidth / 2,
                                sHeight / 2 - menuHeight / 2,
                                menuWidth, menuHeight));
        GUI.Box(new Rect(0, 0, menuWidth, menuHeight),
                "", noBgBoxStyle);
        GUI.DrawTexture(new Rect(menuWidth / 2f - 2228f * 0.126f / 2f,
                                 96f,
                                 2228f * 0.126f, 2047f * 0.126f), defeatTexture);
        GUI.DrawTexture(new Rect(0, 20, menuWidth, menuWidth / 750 * 150),
                        gameOverTexture);
        float buttonWidth = 110;
        if (GUI.Button(new Rect(menuWidth / 2 - buttonWidth - 5,
                                menuHeight - 40, buttonWidth, 30),
                       "Main menu", buttonStyle)) {
            Application.LoadLevel(startMenuName);
        }
        if (GUI.Button(new Rect(menuWidth / 2 + 5,
                                menuHeight - 40, buttonWidth, 30),
                       "Try again", buttonStyle)) {
            game.RestartLevel();
        }
        GUI.EndGroup();
    }

	private void DrawPauseWindow()
	{
		float menuWidth = 330;
		float menuHeight = 380;
		GUI.Box(new Rect(0, 0, sWidth, sHeight), "", boxStyle);
		GUI.BeginGroup(new Rect(sWidth / 2 - menuWidth / 2,
		                        sHeight / 2 - menuHeight / 2,
		                        menuWidth, menuHeight));
		GUI.Box(new Rect(0, 0, menuWidth, menuHeight),
		        "", noBgBoxStyle);
		GUI.DrawTexture(new Rect(menuWidth / 2f - 2228f * 0.126f / 2f,
		                         96f,
		                         2228f * 0.126f, 2047f * 0.126f), pauseTexture);
		GUI.DrawTexture(new Rect(0, 20, menuWidth, menuWidth / 750 * 150),
		                pauseTextTexture);
		float buttonWidth = 110;
		if (GUI.Button(new Rect(menuWidth / 2 - buttonWidth - 5,
		                        menuHeight - 40, buttonWidth, 30),
		               "Quit", buttonStyle)) {
			Application.LoadLevel(startMenuName);
		}
		if (GUI.Button(new Rect(menuWidth / 2 + 5,
		                        menuHeight - 40, buttonWidth, 30),
		               "Restart Level", buttonStyle)) {
			game.RestartLevel();
		}
		GUI.EndGroup();
	}

	private void UpgradeTower(){
		if (towerMenu.nextUpgrade != null && game.mischief >= towerMenu.upgradeCost) {
						game.AddMischief (-towerMenu.upgradeCost);
						Instantiate (towerMenu.nextUpgrade,
			            towerMenu.transform.position,
			            Quaternion.identity);

						Destroy (towerMenu.gameObject);
						towerMenu = null;

				}	
		}

	private void SellTower(){
		game.AddMischief(towerMenu.cost*2/3);
		Destroy(towerMenu.gameObject);
		towerMenu = null;

		}
}
