using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public int GameScore = 0;
    
    private int HighScore = 0;
    private const string HighScoreKey = "high_score";
    private const string RecentScoreKey = "recent_score";

    public GameObject LargeAsteroid;
    public GameObject MediumAsteroid;
    public GameObject SmallAsteroid;

    public ParticleSystem ExplosionFX;
    public ParticleSystem LittleExplosionFX;

    private GameObject MA1;
    private GameObject MA2;
    private GameObject SA1;
    private GameObject SA2;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            HighScore = PlayerPrefs.GetInt(HighScoreKey);
        }
        else
        {
            PlayerPrefs.SetInt(HighScoreKey, GameScore);
        }
        PlayerPrefs.SetInt(RecentScoreKey, GameScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int pointValue)
    {
        GameScore += pointValue;
    }

    public void RemoveLife()
    {
        // Check to see how many lives player has
            // If player lost final life, check if high score and load game over scene
            // Otherwise, remove life and send the player back to the origin/spawn.
    }

    public void SplitAsteroid(Transform tran)
    {
        if (tran.tag == "LargeAsteroid")
        {
            LargeExplosion(tran);
            MA1 = Instantiate(MediumAsteroid);
            MA2 = Instantiate(MediumAsteroid);

            Asteroid_Movement mAsteroid = MA1.GetComponent<Asteroid_Movement>();
            mAsteroid.Manager = this;
            Asteroid_Movement mAsteroid2 = MA2.GetComponent<Asteroid_Movement>();
            mAsteroid2.Manager = this;

            mAsteroid.AsteroidPosition = tran.transform.position;

            MA1.transform.position = mAsteroid.AsteroidPosition;
            var euler1 = transform.eulerAngles;
            euler1.z = Random.Range(0, 360);
            MA1.transform.eulerAngles = euler1;

            var euler2 = transform.eulerAngles;
            euler2.z = Random.Range(0, 360);
            MA2.transform.eulerAngles = euler2;
            MA2.transform.position = mAsteroid2.AsteroidPosition;
        }

        if (tran.tag == "MediumAsteroid")
        {
            LargeExplosion(tran);
            SA1 = Instantiate(SmallAsteroid);
            SA2 = Instantiate(SmallAsteroid);

            Asteroid_Movement sAsteroid = SA1.GetComponent<Asteroid_Movement>();
            sAsteroid.Manager = this;
            Asteroid_Movement sAsteroid2 = SA2.GetComponent<Asteroid_Movement>();
            sAsteroid2.Manager = this;

            sAsteroid.AsteroidPosition = tran.transform.position;

            SA1.transform.position = sAsteroid.AsteroidPosition;
            var euler1 = transform.eulerAngles;
            euler1.z = Random.Range(0, 360);
            SA1.transform.eulerAngles = euler1;

            var euler2 = transform.eulerAngles;
            euler2.z = Random.Range(0, 360);
            SA2.transform.eulerAngles = euler2;
            SA2.transform.position = sAsteroid2.AsteroidPosition;
        }

        if (tran.tag == "SmallAsteroid")
        {
            SmallExplosion(tran);
        }
    }

    private void LargeExplosion(Transform tran)
    {
        ParticleSystem Boom;
        Boom = Instantiate(ExplosionFX);
        Boom.transform.position = tran.position;
    }

    private void SmallExplosion(Transform tran)
    {
        ParticleSystem Boom;
        Boom = Instantiate(LittleExplosionFX);
        Boom.transform.position = tran.position;
    }
}
