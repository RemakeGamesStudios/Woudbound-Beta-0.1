using UnityEngine;


public class SoundManager : MonoBehaviour
{

    public static SoundManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    [Header("params")]
    public AudioClip playerAttackSound;
    public AudioClip playerWalkSound;
    public AudioSource audioSource;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerAttackSound = Resources.Load<AudioClip>("playerAttack");
        playerWalkSound = Resources.Load<AudioClip>("playerWalk");

        audioSource = GetComponent<AudioSource>();

    }

    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "playerAttack":
                audioSource.PlayOneShot(playerAttackSound);
                break;
            case "playerWalk":
                audioSource.clip = playerWalkSound;
                audioSource.Play();
                audioSource.loop = true;
                break;
            case "playerStopWalking":
                audioSource.Stop();
                break;
        }
    }
    
}