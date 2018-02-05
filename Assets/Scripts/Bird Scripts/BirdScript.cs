using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour {

    public static BirdScript instance;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private Animator anim;

    private float forwardSpeed = 2f;
    private float bounceSpeed = 4f;

    //Programming the movement and flapping of the Bird.
    private bool didFlap;
    public bool isAlive;

    private Button flapButton;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip flapClip, pointClip, diedClip;

    public int score;

	void Awake()
    {
        if(instance == null)
        {
            instance = this; // this refers to Birdscipt.
        }

        isAlive = true;

        score = 0;

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button> ();
        flapButton.onClick.AddListener (() => FlapTheBird()); // same as adding the function on the Button.

        SetCamerasX();
    }
    
    // Use this for initialization
	void Start () {
	
	}
	
/* Update is called once per frame & FixedUpdate is called every 2 or 3 frames.
 * Transform position gives the current position of the bird.
 * Time.deltaTime is very small number used in Update or FixedUpdate function. */
	void FixedUpdate () {
        if (isAlive)
        {
            Vector3 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;

            if (didFlap)
            {
                didFlap = false;
                myRigidBody.velocity = new Vector2(0, bounceSpeed);
                audioSource.PlayOneShot(flapClip);
                anim.SetTrigger("Flap");                
            }

            if(myRigidBody.velocity.y >= 0){
                transform.rotation = Quaternion.Euler(0, 0, 0);
            } else
            {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, myRigidBody.velocity.y / 7);
                //will be shifting between 0 & 90 in the mentioned time.
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
	
	}

    void SetCamerasX()
    {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x - 1f);
    }

    public float GetPositionX()
    {
        return transform.position.x;
    }

/* For flapping the Bird once we are using this function.
 * We assign this function to the FlapButton inorder to flap the bird once.  */
    public void FlapTheBird()
    {
        didFlap = true;
    }

//Using this function when the GameObject is not set to trigger
    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe")
        {
            if (isAlive)
            {
                isAlive = false;
                anim.SetTrigger("Bird Died");
                audioSource.PlayOneShot(diedClip);
                GameplayController.instance.PlayerDiedShowScore(score);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "PipeHolder")
        {
            score++;
            GameplayController.instance.Setscore(score);
            audioSource.PlayOneShot(pointClip);
        }
    }
}
