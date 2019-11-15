using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PlayerMove))]
public class Player : MonoBehaviour
{

    public float growingTempo = 0.1f;
    public int childrenLimit = 3;
    public float xBirthRange = 1f;
    public int worldReversingLimit = 2;

    public static int playersAmount = 0;

    private bool isGrowing = false;

    private float sizeGrowth;
    private Vector2 newSize;
    private float maxSizeFactor = 1.8f;
    private float maxSize;
    private float deathTime = 1.5f;
    public Vector2 startPosition;
    private Vector2 startScale;
    private int broughtChildren = 0;
    private float xOffset;
    public int worldReversingCounter = 0;
    private int pointsForDeath = 1;
    private int pointsForSuccess = 1;

    [SerializeField] GameObject plopVFX;
    [SerializeField] GameObject smokeVFX;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] AudioClip[] deathSounds;
    [SerializeField] AudioClip[] plopSounds;
    [SerializeField] AudioClip[] successedSounds;

    private float plopSoundVolume = 0.8f;
    public float deathSoundVolume = 0.5f;
    private float successedSoundVolume = 0.75f;


    void Start()
    {
        SetStartPosition();
        SetStartingValues();
        playersAmount++;
    }

    private void SetStartingValues()
    {
        maxSize = maxSizeFactor * transform.localScale.x;
        startScale = transform.localScale;
    }

    private void SetStartPosition()
    {
        startPosition = playerPrefab.transform.position;
        Vector2 birthPosition = startPosition;
        xOffset = Random.Range(0, xBirthRange);
        birthPosition.x += xOffset;
        transform.position = birthPosition;
    }

    void Update()
    {
        ServeResizing();
    }

    private void ServeResizing()
    {
        ManageGrowing();
        CheckPlopDeath();
    }

    private void CheckPlopDeath()
    {
        if (transform.localScale.x > maxSize)
        {
            Plop();
        }
    }

    private void ManageGrowing()
    {
        if (isGrowing)
        {
            sizeGrowth = growingTempo * Time.deltaTime;
            newSize = new Vector2(transform.localScale.x + sizeGrowth, transform.localScale.y + sizeGrowth);

            transform.localScale = newSize;
        }
    }

    private void Plop()
    {
        MenageDeathPoints();
        PlayPlopSound();
        Explode();
    }

    private void Explode()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(plopVFX, transform.position, Quaternion.identity);
        Destroy(explosion, deathTime);
    }

    public void Successed()
    {
        PlaySuccessSound();
        ManageSuccessPoints();

        Destroy(gameObject);
    }

    public void BringNewAlien()
    {
        if (broughtChildren >= childrenLimit) return;

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.BringNewAlien();

        broughtChildren++;
    }

    internal void MakeGrowing()
    {
        isGrowing = true;
    }

    internal void StopGrowing()
    {
        isGrowing = false;
    }

    public bool IsAllowedToReverseWorld()
    {
        return worldReversingCounter < worldReversingLimit;
    }

    public void BurnDeath()
    {
        MenageDeathPoints();
        PlayDeathSound();
        GetSmoked();
    }

    private void GetSmoked()
    {
        Destroy(gameObject);
        GameObject smoke = Instantiate(smokeVFX, transform.position, Quaternion.identity);
        smoke.transform.Rotate(-90, 0, 0, Space.Self);
        Destroy(smoke, deathTime);
    }

    private void PlaySuccessSound()
    {
        int sucessedSoundIndex = Random.Range(0, successedSounds.Length);
        AudioSource.PlayClipAtPoint(successedSounds[sucessedSoundIndex], Camera.main.transform.position, successedSoundVolume);
    }

    private void PlayDeathSound()
    {
        int deathSoundIndex = Random.Range(0, deathSounds.Length);
        AudioSource.PlayClipAtPoint(deathSounds[deathSoundIndex], Camera.main.transform.position, deathSoundVolume);
    }

    private void PlayPlopSound()
    {
        int plopSoundIndex = Random.Range(0, plopSounds.Length);
        AudioSource.PlayClipAtPoint(plopSounds[plopSoundIndex], Camera.main.transform.position, plopSoundVolume);
    }

    private void ManageSuccessPoints()
    {
        Score scoreDisplay = FindObjectOfType<Score>();
        scoreDisplay.AddPoints(pointsForSuccess);
    }

    private void MenageDeathPoints()
    {
        Score score = FindObjectOfType<Score>();
        score.RemovePoints(pointsForDeath);
    }

    private void OnDestroy()
    {
        playersAmount--;
    }
}
