using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnime;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private AudioSource audioP;
    public AudioClip jumpClip;
    public AudioClip crashClip;
    public float upForce = 10.0f;
    public float gravityModifier;
    public bool isOnGround;
    public bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnime = GetComponent<Animator>();
        audioP = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true && isGameOver != true)
        {
            playerRb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnime.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            audioP.PlayOneShot(jumpClip, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            isGameOver = true;
            playerAnime.SetBool("Death_b", true);
            playerAnime.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            audioP.PlayOneShot(crashClip, 1.0f);

        } else if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
    }
}
