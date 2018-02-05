using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    [SerializeField]
    private GameObject[] birds;

    private bool IsGreenBirdUnlocked, IsRedBirdUnlocked;

    void Awake()
    {
        MakeInstance();        
    }

	// Use this for initialization
	void Start () {
        birds[GameController.instance.GetSelectBird()].SetActive(true);
        CheckIfBirdsAreUnlocked();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void CheckIfBirdsAreUnlocked()
    {
        if(GameController.instance.IsGreenBirdUnlocked() == 1)
        {
            IsGreenBirdUnlocked = true;
        }

        if(GameController.instance.IsRedBirdUnlocked() == 1)
        {
            IsRedBirdUnlocked = true;
        }
    }

    public void PlayGame()
    {
        SceneFader.instance.FadeIn("GamePlay");
    }

    public void ChangeBird()
    {
        if(GameController.instance.GetSelectBird() == 0) //Checking if blue bird is selected.
        {
            if (IsGreenBirdUnlocked) 
            {
                birds[0].SetActive(false);
                GameController.instance.SetSelectBird(1);
                birds[GameController.instance.GetSelectBird()].SetActive(true);
            }
        }
        else if (GameController.instance.GetSelectBird() == 1)
        {
            if (IsRedBirdUnlocked)
            {
                birds[1].SetActive(false);
                GameController.instance.SetSelectBird(2);
                birds[GameController.instance.GetSelectBird()].SetActive(true);
            }
            else
            {
                birds[1].SetActive(false);
                GameController.instance.SetSelectBird(0);
                birds[GameController.instance.GetSelectBird()].SetActive(true);
            }
        }
        else if(GameController.instance.GetSelectBird() == 2)
        {
            birds[2].SetActive(false);
            GameController.instance.SetSelectBird(0);
            birds[GameController.instance.GetSelectBird()].SetActive(true);
        }
    }
}
