using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{

    // target impact on game
    public int scoreAmount = 0;
    public float timeAmount = 0.0f;

    public float secondsToChange = 5.0f;
    private float targetTime = 0.0f;

    public bool hasDynamicScore;
    public int maxScoreRange = 0;
    public int minScoreRange = 0;

    public bool hasDynamicTime;
    public float maxTimeRange = 0f;
    public float minTimeRange = 0f;

    public Material[] materials;
    public GameObject[] explosions;

    private Renderer renderer;

    void Start()
    {
        targetTime = Time.time + secondsToChange;
        renderer = gameObject.GetComponent<Renderer>();

        if (hasDynamicScore || hasDynamicTime)
            SelectMaterialnExplosion();
    }

    void Update()
    {
        if ((hasDynamicScore || hasDynamicTime) && Time.time >= targetTime)
        {
            SelectMaterialnExplosion();

            targetTime = Time.time + secondsToChange;
        }
    }

    // explosion when hit?
    public GameObject explosionPrefab;

    // when collided with another gameObject
    void OnCollisionEnter(Collision newCollision)
    {
        // exit if there is a game manager and the game is over
        if (GameManager.gm)
        {
            if (GameManager.gm.gameIsOver)
                return;
        }

        // only do stuff if hit by a projectile
        if (newCollision.gameObject.tag == "Projectile")
        {
            if (explosionPrefab)
            {
                // Instantiate an explosion effect at the gameObjects position and rotation
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }

            // if game manager exists, make adjustments based on target properties
            if (GameManager.gm)
            {
                GameManager.gm.targetHit(scoreAmount, timeAmount);
            }

            // destroy the projectile
            Destroy(newCollision.gameObject);

            // destroy self
            Destroy(gameObject);
        }
    }

    void SelectMaterialnExplosion()
    {
        if (hasDynamicScore)
            scoreAmount = Random.Range(minScoreRange, maxScoreRange);

        if (hasDynamicTime)
            timeAmount = Random.Range(minTimeRange, maxTimeRange);

        if (scoreAmount > 0)
        {
            renderer.material = materials[0];
            explosionPrefab = explosions[0];
        }
        else if (scoreAmount < 0)
        {
            renderer.material = materials[1];
            explosionPrefab = explosions[1];
        }
        else if (scoreAmount == 0)
        {
            if (timeAmount > 0)
            {
                renderer.material = materials[2];
                explosionPrefab = explosions[2];
            }
            else if (timeAmount <= 0)
            {
                renderer.material = materials[3];
                explosionPrefab = explosions[1];
            }

        }
    }
}
