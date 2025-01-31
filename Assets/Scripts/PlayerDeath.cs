
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerDeath : MonoBehaviour
{
    Animator anim; // Animator for the player's death animation
    Rigidbody2D rb; // Rigidbody2D to control the player's physics
    public AudioSource DeathSFX; // Sound effect for player death
    public Text deathtext; 
    public int deathcount = 3; // Initial number of lives 
    [SerializeField] Vector2 CPP; // Checkpoint position 

    private void Start()
    {
        anim = GetComponent<Animator>(); // Get Animator component
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
        CPP = transform.position; // Set the initial checkpoint position to current position
        deathtext.text = "Death : " + deathcount.ToString(); // Display death count in the UI
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap")) // Check if player collides with a trap
        {
            anim.SetTrigger("Death"); // Trigger death animation
            rb.bodyType = RigidbodyType2D.Static; // Disable player movement 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CP")) // Check if player enters checkpoint trigger
        {
            CPP = collision.gameObject.transform.position; // Set new checkpoint position
            Debug.Log("CP"); 
        }
    }

    void Die()
    {
        DeathSFX.Play(); // Play death sound effect
        rb.bodyType = RigidbodyType2D.Dynamic; // Reactivate player physics 
        transform.position = CPP; // Reset player position to last checkpoint
        anim.SetTrigger("Idle"); // Reset animation to idle after death
        deathcount--; // Decrease death count
        if (deathcount <= 0) // If no more deaths left
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the scene
        }
        deathtext.text = "Death : " + deathcount.ToString(); // Update death count UI
    }
}
