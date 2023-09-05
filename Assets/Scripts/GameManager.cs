using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public int lives = 3;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public int score = 0;

    public void AsteroidDestroy(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        //TODO: Increase score

        if (asteroid.size < 0.75)
        {
            score += 100;
        }else if(asteroid.size < 1.25f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }
    }
    public void PlayerDied()
    {
        explosion.transform.position = player.transform.position;
        this.explosion.Play();
        this.lives--;

        if(this.lives<=0)
        {
            GameOver();
        }
        else
        {
           Invoke(nameof(Respawn), this.respawnTime);
        }

    }
    void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions),this.respawnInvulnerabilityTime);

    }
    void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    void GameOver()
    {
        this.lives = 3;
        this.score = 0;
        Invoke(nameof(Respawn), this.respawnTime);
    }
} 
