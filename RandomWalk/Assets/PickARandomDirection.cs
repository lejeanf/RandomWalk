using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickARandomDirection : MonoBehaviour {

    public int min = 0;
    public int max = 4;
    public int maxInterval = 10;
    private int rdmValue = 0;

    public float timeInterval = 1;
    private float deltaTime = 0;
    private int direction;

    public Image _image;
    public  Sprite[] _sprites;
    private Sprite currentSprite;
    private Sprite pausedSprite;

    public Camera cam;
    public Color _defaultColor;
    public Color _secondColor;

    public Text _text;
    public Slider _slider;

    public AudioSource audioData;
    public AudioClip forward;
    public AudioClip backward;
    public AudioClip left;
    public AudioClip right;

    private bool runningStatus;

    //public List<AudioClip> _audioClips = new List<AudioClip>();

    private bool appStarted = false;

    void Start () {
        Time.timeScale = 0;
        direction = 0;
        rdmValue = Random.Range(min, max);
        deltaTime = Time.deltaTime;
        currentSprite = _sprites[0];
        cam.backgroundColor = _defaultColor;
        
        //audioData = GetComponent<AudioSource>();
        audioData.Stop();
        audioData.volume = 1;
        appStarted = false;

        //set screen orientation
        Screen.orientation = ScreenOrientation.Portrait;
    }
	
	void FixedUpdate ()
    {
        rdmWalk();
    }

    public void pauseUnpause()
    {
        if (Time.timeScale == 0)
        {
            _image.sprite = _sprites[5];
            Time.timeScale = 1;
            Debug.Log("unpaused");
        }
        else if (Time.timeScale == 1)
        {
            _image.sprite = _sprites[0];
            cam.backgroundColor = _defaultColor;
            _text.text = "Paused";
            Debug.Log("paused");
            Time.timeScale = 0;
        }
    }

    void Direction()
    {
        cam.backgroundColor = _defaultColor;

        rdmValue = Random.Range(min, max);
        deltaTime = 0;
        direction = rdmValue;
        currentSprite = _sprites[rdmValue + 1];

        // change the text in the UI
        switch (direction)
        {
            case 3:
                //print("Go to the left");
                _text.text = "Go to the left";
                audioData.clip = left;
                break;
            case 2:
                //print("Go Backward");
                _text.text = "Go backward";
                audioData.clip = backward;
                break;
            case 1:
                //print("Go to the right");
                _text.text = "Go to the right";
                audioData.clip = right;
                break;
            case 0:
                //print("Go to forward");
                _text.text = "Go forward";
                audioData.clip = forward;
                break;
            default:
                //print("Don't move, there is a problem.");
                _text.text = "Don't move, there is a problem.";
                break;
        }
        audioData.Play();
    }

    void rdmWalk()
    {
        deltaTime += Time.deltaTime;
        timeInterval = _slider.value;
        _image.sprite = currentSprite;
        _slider.maxValue = maxInterval;

        if (appStarted) { cam.backgroundColor = Color.Lerp(_defaultColor, _secondColor, deltaTime / timeInterval); }

        // activate direction function each n seconds (according to the time interval setted)
        if (deltaTime >= (Time.deltaTime + timeInterval))
        {
            Direction();
            appStarted = true;
        }
    }
}
