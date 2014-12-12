using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    public string nextLevelName;
    public string lostLevelName;
    public Wave[] waves;
    public int mischief;
    public int health;
    public int maxHealth;
    public AudioClip Victory;
    public int levelNumber;
    private int waveNumber;
    private bool waveRunning;
    private Gui gui;
    private bool muted;

    public void Start()
    {
        gui = GameObject.Find("Gui").GetComponent<Gui>();
        waveNumber = 0;
        waveRunning = false;
        Time.timeScale = 1;
		Physics2D.IgnoreLayerCollision (8, 9, true);
    }

    public void Update()
    {
    }

    public void NewWave()
    {
        if (waveRunning)
            return;

        if (waveNumber < waves.Length) {
            waves[waveNumber].Run();
            waveRunning = true;
            waveNumber++;
        }
    }

    public void NextLevel()
    {
        gui.SetWinWindow(true);
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoadNextLevel()
    {
        Application.LoadLevel(nextLevelName);        
    }

    public void Lose()
    {
        gui.SetLoseWindow(true);
        Time.timeScale = 0;
    }

    public void EnemyDestroyed()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            waveRunning = false;
            if (waveNumber == waves.Length) {
                if (health <= 0) {
                    Lose();
                } else {
                    audio.PlayOneShot(Victory);
                    Invoke("NextLevel", audio.clip.length);
                }
            }
        }
    }

    public bool AddMischief(int m)
    {
        if (mischief + m >= 0) {
            mischief += m;
            return true;
        } else {
            return false;
        }
    }

    public void AddHealth(int h)
    {
        health += h;
        if (health <= 0) {
            health = 0;
            Lose();
        }
    }

    public int GetScore()
    {
        return (int)((float)mischief
                     * ((float)health / (float)maxHealth / 2f + 0.5f));
    }

    public bool IsWaveRunning()
    {
        return waveRunning;
    }

    public bool WavesOver()
    {
        return waveNumber == waves.Length;
    }

    public void ToggleAudio()
    {
        if (muted) {
            AudioListener.volume = 1;
        } else {
            AudioListener.volume = 0;
        }
        muted = !muted;
    }

    public bool IsAudioOn()
    {
        return !muted;
    }
}
