using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDisplay : MonoBehaviour
{
    public SpriteRenderer ghostRenderer;
    public GameObject ghost;
    public GameObject slobber;
    public Sprite ghostSprite;
    public Sprite noSprite;
    public int arrowIndex;
    public TextMeshPro textComponent;

    public Vector3[] arrowDirections = {
        Vector3.up,
        Vector3.down,
        Vector3.left,
        Vector3.right
    };

    public Vector3 selectedDirection;


    private void Start()
    {
        // Ensure you have a reference to the SpriteRenderer component.
        if (ghostRenderer == null)
        {
            ghostRenderer = GetComponent<SpriteRenderer>();
        }
     
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the collider is tagged as "Player" (you can use any tag that fits your needs).
        if (other.CompareTag("Slobber1")|| other.CompareTag("Slobber2"))
        {
            // Display the new sprite.
            ghostRenderer.sprite = ghostSprite;

           //hide slobber text
            textComponent = other.GetComponentInChildren<TextMeshPro>();
            textComponent.text = " ";
            
            //show ghost text
            ghost.GetComponentInChildren<TextMeshPro>().text = slobber.GetComponent<DragAndDrop>().slobberEatCounter.ToString();


            //hide slobber
            other.GetComponent<SpriteRenderer>().sprite = noSprite;
            other.GetComponent<DragAndDrop>().selectedArrow = this.arrowIndex;
            Debug.Log("Slobber collision");
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ghostRenderer.sprite = noSprite;
        ghost.GetComponentInChildren<TextMeshPro>().text = " ";
        //reappear slobber
        other.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        other.GetComponent<DragAndDrop>().selectedArrow = 0;
        textComponent.text = other.GetComponent<DragAndDrop>().slobberEatCounter.ToString();
    }
}
