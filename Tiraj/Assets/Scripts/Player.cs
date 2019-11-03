using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{

    public float speed = 500;
    public float jumpForce = 325;
    public float growingTempo = 0.1f;
    public int childrenLimit = 3;
    public float xBirthRange = 1f;
    public int worldReversingLimit = 2;

    private bool isGrowing = false;
    private bool isGrounded;

    private float xDisplacement;
    private float sizeGrowth;
    private Vector2 newSize;
    private float maxSizeFactor = 1.8f;
    private float maxSize;
    private float deathTime = 1.5f;
    private Vector2 startPosition;
    private Vector2 startScale;
    private float movementFactor = 0f;
    private bool movingEnabled = false;
    private int broughtChildren = 0;
    private float xOffset;
    private int horizontalDirection = 1;
    public int worldReversingCounter = 0;
    private int pointsForDeath = 1;
    private int pointsForSuccess = 2;


    private Rigidbody2D rigidBody;
    private Animator animator;

    [SerializeField] GameObject plopVFX;
    [SerializeField] GameObject smokeVFX;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] AudioClip[] deathSounds;
    [SerializeField] AudioClip[] plopSounds;
    [SerializeField] AudioClip[] successedSounds;

    private float plopSoundVolume = 0.8f;
    public float deathSoundVolume = 0.5f;
    private float successedSoundVolume = 1f;


    void Start()
    {
        if (startPosition == Vector2.zero)
        {
            startPosition = transform.position;
            Vector2 birthPosition = startPosition;
            xOffset = Random.Range(0, xBirthRange);
            birthPosition.x += xOffset;
            transform.position = birthPosition;
        }

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isGrounded = false;
        maxSize = maxSizeFactor * transform.localScale.x;
        startPosition = transform.position;
        startScale = transform.localScale;
    }


    void Update()
    {
        ServeHorizontalMoving();
        ServeJumping();
        ServeAnimations();
        ServeFacingDirection();
        ServeResizing();
    }

    private void ServeHorizontalMoving()
    {
        if (!movingEnabled) return;

        xDisplacement = Input.GetAxis("Horizontal") * speed * Time.deltaTime * horizontalDirection;
        rigidBody.velocity = new Vector2(xDisplacement, rigidBody.velocity.y);
    }

    private void ServeResizing()
    {
        if (isGrowing)
        {
            sizeGrowth = growingTempo * Time.deltaTime;
            newSize = new Vector2(transform.localScale.x + sizeGrowth, transform.localScale.y + sizeGrowth);

            transform.localScale = newSize;
        }

        if (transform.localScale.x > maxSize)
        {
            Plop();
        }
    }

    private void Plop()
    {
        ScoreDisplay scoreDisplay = FindObjectOfType<ScoreDisplay>();
        scoreDisplay.RemovePoints(pointsForDeath);

        int plopSoundIndex = Random.Range(0, plopSounds.Length);
        AudioSource.PlayClipAtPoint(plopSounds[plopSoundIndex], Camera.main.transform.position, plopSoundVolume);

        Destroy(gameObject);

        int playersAmount = FindObjectsOfType<Player>().Length;
        if (playersAmount <= 1) BringNewAlien();

        GameObject explosion = Instantiate(plopVFX, transform.position, Quaternion.identity);
        Destroy(explosion, deathTime);
    }

    public void BringNewAlien()
    {
        if (broughtChildren >= childrenLimit) return;

        GameObject newPlayer = Instantiate(playerPrefab, startPosition, Quaternion.identity);
        newPlayer.transform.localScale = startScale;
        newPlayer.GetComponent<Player>().enabled = true;
        newPlayer.GetComponent<Player>().worldReversingCounter = 0;
        newPlayer.GetComponent<Player>().horizontalDirection = horizontalDirection;
        newPlayer.GetComponent<BoxCollider2D>().enabled = true;
        newPlayer.GetComponent<Animator>().enabled = true;

        broughtChildren++;
    }

    private void ServeJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;
        }
    }

    private void ServeAnimations()
    {
        animator.SetFloat("runSpeed", Mathf.Abs(rigidBody.velocity.x));
        animator.SetFloat("jumpSpeed", rigidBody.velocity.y);
    }

    private void ServeFacingDirection()
    {
        if (rigidBody.velocity.x < 0)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        else if (rigidBody.velocity.x > 0)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        animator.SetTrigger("grounded");
        movingEnabled = true;
    }

    internal void MakeGrowing()
    {
        isGrowing = true;
    }

    internal void StopGrowing()
    {
        isGrowing = false;
    }

    public bool isAllowedToReverseWorld()
    {
        return worldReversingCounter < worldReversingLimit;
    }

    public void ReverseHorizontal()
    {
        horizontalDirection *= -1;
    }

    public void BurnDeath()
    {
        ScoreDisplay scoreDisplay = FindObjectOfType<ScoreDisplay>();
        scoreDisplay.RemovePoints(pointsForDeath);
        Destroy(gameObject);

        int deathSoundIndex = Random.Range(0, deathSounds.Length);
        AudioSource.PlayClipAtPoint(deathSounds[deathSoundIndex], Camera.main.transform.position, deathSoundVolume);

        int playersAmount = FindObjectsOfType<Player>().Length;
        if (playersAmount <= 1) BringNewAlien();

        GameObject explosion = Instantiate(smokeVFX, transform.position, Quaternion.identity);
        explosion.transform.Rotate(-90, 0, 0, Space.Self);
        Destroy(explosion, deathTime);
    }

    public void Successed()
    {
        int playersAmount = FindObjectsOfType<Player>().Length;
        if (playersAmount <= 1) BringNewAlien();

        int sucessedSoundIndex = Random.Range(0, successedSounds.Length);
        print(sucessedSoundIndex);
        AudioSource.PlayClipAtPoint(successedSounds[sucessedSoundIndex], Camera.main.transform.position, successedSoundVolume);

        ScoreDisplay scoreDisplay = FindObjectOfType<ScoreDisplay>();
        scoreDisplay.AddPoints(pointsForSuccess);
        Destroy(gameObject);
    }
}
