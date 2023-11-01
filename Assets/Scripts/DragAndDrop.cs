using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public bool isDragging = false;
    private Vector3 offset;
    public int selectedArrow;
    public GameObject[] slobberSpawns;
    public Sprite noSprite;
    public SlobberMovement slobberMovement;
    public int slobberEatCounter;
    public Vector3 resetPosition;

    private void Start()
    {
        resetPosition= transform.position;
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        isDragging = false;
        if (selectedArrow == 0)
        {
            transform.position = resetPosition;
        }
        else
        {
            //make opaque
            Color spriteColor = slobberSpawns[selectedArrow - 1].GetComponent<SpriteRenderer>().color;
            spriteColor.a = 255f;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = noSprite;

            //set ghost counter, direction and tag
            slobberSpawns[selectedArrow - 1].GetComponent<SlobberMovement>().ghostEatCounter = slobberEatCounter;
            slobberSpawns[selectedArrow - 1].GetComponent<SlobberMovement>().rayDirection = slobberSpawns[selectedArrow - 1].GetComponentInParent<TriggerDisplay>().selectedDirection;
            slobberSpawns[selectedArrow - 1].tag = this.gameObject.tag;

            //start movement coroutine
            slobberMovement = slobberSpawns[selectedArrow - 1].GetComponent<SlobberMovement>();
            slobberMovement.RayCastCheck();

        }

    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }
}
