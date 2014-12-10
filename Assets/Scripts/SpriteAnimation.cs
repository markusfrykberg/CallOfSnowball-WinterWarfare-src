using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    public float fps = 4;
    public bool repeat;
    private float startTime;
    private bool playing = false;
    private int frameNumber = 0;
    private int startFrame = 0;

    public Sprite GetSprite()
    {
        return sprites[GetFrameNumber()];
    }

    public int GetFrameNumber()
    {
        if (!playing)
            return frameNumber;

        int frame = (int)((Time.time - startTime) * fps) + startFrame;
        if (frame >= sprites.Length && !repeat) {
            frameNumber = sprites.Length - 1;
        } else {
            frameNumber = frame % sprites.Length;
        }
        return frameNumber;
    }

    public void GoTo(int frame)
    {
        if (frame < 0) {
            startFrame = sprites.Length + frame;
        } else {
            startFrame = frame;
        }
        frameNumber = startFrame;
        startTime = Time.time;
    }

    public void Play(bool repeat)
    {
        startTime = Time.time;
        startFrame = frameNumber;
        playing = true;
        this.repeat = repeat;
    }

    public void Pause()
    {
        playing = false;
    }
}
