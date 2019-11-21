﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class TPlayerController : MonoBehaviour
{
    public GameObject collectible;
    public GameObject enemy;
    public GameObject fireworks;
    public Text countText;
    public Text livesText;
    public Text finalText;

    public AudioSource audioSourceFootsteps;
    public AudioSource audioSourceJump;
    public AudioSource audioSourcePickup;

    public AudioClip audioClipMusic1;
    public AudioClip audioClipMusic2;
    public AudioClip audioClipMusic3;
    List<AudioSource> scArr = new List<AudioSource>();

    private int lives;
    private int count;

    public Image heart1;
    private Animator heartAnimator1;
    public Image heart2;
    private Animator heartAnimator2;
    public Image heart3;
    private Animator heartAnimator3;

    public GameObject Ragdoll;
    void Start()
    {
        count = 0;
        setCountText();

        lives = 15;
        setLivesText();

        finalText.text = "";

        for(int i = 0; i<10;i++)
        {
            createNewCollectible();
        }

        for (int i = 0; i < 3; i++)
        {
            createNewEnemy();
        }

        heartAnimator1 = heart1.GetComponent<Animator>();
        heartAnimator2 = heart2.GetComponent<Animator>();
        heartAnimator3 = heart3.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        audioFootSteps();
        audioJump();
    }
    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
    void Explode(Vector3 position)
    {
        GameObject firework = Instantiate(fireworks, position, Quaternion.identity);
        fireworks.GetComponent<ParticleSystem>().Play();
    }

    void setLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }
    void createNewCollectible()
    {
        Vector3 vector = new Vector3(Random.Range(-4.0f, 4.0f), 0.42f, Random.Range(-4.0f, 4.0f));
        Instantiate(collectible, vector, Quaternion.identity);
    }
    void createNewEnemy()
    {
        Vector3 vector = new Vector3(Random.Range(-4.0f, 4.0f), 0.35f, Random.Range(-4.0f, 4.0f));
        Instantiate(enemy, vector, Quaternion.identity);
    }
    void initDynamicMusic()
    {
        for(int i = 0;i<3;i++)
        {
            AudioSource sc = gameObject.AddComponent<AudioSource>() as AudioSource;
            sc.loop = true;
            sc.volume = 0.4f;
            scArr.Add(sc);
        }
    }
    void addDynamicMusic(AudioClip audioClip, int index)
    {
        scArr[index].Stop();
        scArr[index].clip = audioClip;
        scArr[index].Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            createNewCollectible();

            count++;
            setCountText();

            if (count >= 10)
            {
                finalText.text = "You win!";
                removeAllObjecta();
            }
            if (count == 5) { addDynamicMusic(audioClipMusic3, 1);}
            if (count == 8) { addDynamicMusic(audioClipMusic1, 1); }
        }
    }

    void audioFootSteps()
    {
        if(CrossPlatformInputManager.GetButton("Vertical") || CrossPlatformInputManager.GetButton("Horizontal"))
        {
            if (!audioSourceFootsteps.isPlaying) audioSourceFootsteps.Play();
        }
    }
    void audioJump()
    {
        if(CrossPlatformInputManager.GetButton("Jump"))
        {
            audioSourceJump.Play();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lives--;
            setLivesText();
            
            switch(lives)
            {
                case 2: heartAnimator1.SetBool("flag", true);addDynamicMusic(audioClipMusic2, 0); break;
                case 1: heartAnimator2.SetBool("flag", true); break;
                case 0: heartAnimator3.SetBool("flag", true); break;
            }

            if(lives <= 0)
            {
                finalText.text = "Game Over";
                removeAllObjecta();
                addRagdoll();
            }
        }
    }
    void removeAllObjecta()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject ob in allObjects)
        {
            if ((ob.CompareTag("Pick Up")) || (ob.CompareTag("Enemy")))
            {
                Destroy(ob);
            }
        }
    }
    void addRagdoll()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach(GameObject ob in allObjects)
        {
            if(ob.CompareTag("Player"))
            {
                Instantiate(Ragdoll, ob.transform.position, Quaternion.identity);
                ob.SetActive(false);
                Explode(ob.transform.position);
            }
        }
    }

}
