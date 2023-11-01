using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlobberMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f; // Speed at which the object moves.
    public LayerMask detectionLayer; // Layer for circle detection.
    public float detectionDistance = 1.0f; // Maximum detection distance to the right.
    public float moveDistance = 1.0f;
    public float moveDuration = 0.5f;
    private bool timeToRayCast = true;
    public int ghostEatCounter;
    public TextMeshPro text;
    public Sprite slime;
    public Color slimeColor;
    public int cakesEaten;
    public int cakesAvailable;
    public bool ateCake;

    private bool isMoving = false;
    bool gameWon;
    public GameObject winPanel;
    public Vector3 rayDirection;



    private void Start()
    {
        text.text = " ";
        ateCake = false;
        gameWon = false;
    }

    void Update()
    {
        if (timeToRayCast && ghostEatCounter > 0)
        {
            RayCastCheck();
            text.text = (ghostEatCounter-1).ToString();
        }
    }

    public void RayCastCheck()
    {
        // Cast a 2D ray

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, detectionDistance, detectionLayer);

        if (hit.collider != null && hit.collider.CompareTag("Trash") || hit.collider != null && hit.collider.CompareTag("Goal1") || hit.collider != null && hit.collider.CompareTag("Goal2"))
        {
            if (!isMoving)
            {
                isMoving = true;
                StartCoroutine(MoveObject());
                
            }
            Debug.DrawRay(transform.position, rayDirection * detectionDistance, Color.green); // Visualize the ray

            if (hit.collider.CompareTag("Goal1")&& this.gameObject.CompareTag("Slobber1")|| hit.collider.CompareTag("Goal2") && this.gameObject.CompareTag("Slobber2"))
                {
                cakesEaten++;
                ateCake = true;
            }

            if (cakesEaten >= cakesAvailable)
            {
                gameWon = true;
            }
        }
        else if (hit.collider == null)
        {
            timeToRayCast = true; // Re-enable raycasting if there's no object ahead.
        }
    }

    public void SlimeCastCheck()
    {
        // Cast a 2D ray

        RaycastHit2D hit = Physics2D.Raycast(transform.position + -rayDirection*0.5f, -rayDirection, detectionDistance, detectionLayer);

        if (hit.collider != null && hit.collider.CompareTag("Trash"))
        {
            SpriteRenderer hitRenderer = hit.collider.GetComponent<SpriteRenderer>();
            hitRenderer.sprite = slime;
            hitRenderer.color = slimeColor;
            hit.collider.gameObject.tag = "Slime";
        }
        else if (hit.collider == null)
        {
            timeToRayCast = true; // Re-enable raycasting if there's no object ahead.
        }
    }

    IEnumerator MoveObject()
    {
        float elapsedTime = 0.0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + moveDistance * rayDirection; // Move to the right.

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SlimeCastCheck();

        if (gameWon)
        {
            winPanel.SetActive(true);
        }
        // Wait for a few seconds before re-enabling raycasting
        yield return new WaitForSeconds(0.7f); // Adjust the duration (2.0f) to your desired wait time.
        ghostEatCounter--;
        

        transform.position = targetPosition;
        isMoving = false;
        timeToRayCast = true; // Re-enable raycasting after the movement is complete.


    }
}
