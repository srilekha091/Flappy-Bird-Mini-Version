using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController instance;
    private const string High_Score = "High Score";
    private const string Select_Bird = "Select Bird";
    private const string Green_Bird = "Green Bird";
    private const string Red_Bird = "Red Bird";

	// Use this for initialization
	void Awake () {
        MakeSingleton();
        PlayerPrefs.DeleteAll();
        IsGameStartedForFirstTime();
    }

/* If we already have instance of this class then we will destroy the duplicate
 *  If not we will create instance of this class. */
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;  // making this class(GameController) Instance.
            DontDestroyOnLoad(gameObject);
        }
    }

    void IsGameStartedForFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsGameStartedForFirstTime"))
        {
            PlayerPrefs.SetInt(High_Score, 0);
            PlayerPrefs.SetInt(Select_Bird, 0);
            PlayerPrefs.SetInt(Green_Bird, 1);
            PlayerPrefs.SetInt(Red_Bird, 1);
            PlayerPrefs.SetInt("IsGameStartedForFirstTime", 0);
        }
    }

    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(High_Score, score);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(High_Score);
    }

    public void SetSelectBird(int selectBird)
    {
        PlayerPrefs.SetInt(Select_Bird, selectBird);
    }

    public int GetSelectBird()
    {
        return PlayerPrefs.GetInt(Select_Bird);
    }

// 1 - Bird Unlocked, 0 - Bird Locked.
    public void UnlockGreenBird()
    {
        PlayerPrefs.SetInt(Green_Bird, 1);
    }

    public int IsGreenBirdUnlocked()
    {
        return PlayerPrefs.GetInt(Green_Bird);
    }

    public void UnlockRedBird()
    {
        PlayerPrefs.SetInt(Red_Bird, 1);
    }

    public int IsRedBirdUnlocked()
    {
        return PlayerPrefs.GetInt(Red_Bird);
    }

}
