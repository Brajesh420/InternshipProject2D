using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    int orange = 0; // Counter for collected oranges
    public Text OrangeDisplay; // UI text to display 
    public AudioSource CollectSFX; 

    private void OnTriggerEnter2D(Collider2D collision) // Trigger detection for item collection
    {
        if (collision.gameObject.CompareTag("Orange")) // Check if collided object is an orange
        {
            CollectSFX.Play(); // Play collection sound
            orange++; // Increment orange 
            Destroy(collision.gameObject); // Remove collected item from scene
            OrangeDisplay.text = "" + orange; // Update UI text with new count
        }
    }
}
