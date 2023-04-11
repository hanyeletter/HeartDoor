using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectLightMirror : MonoBehaviour
{
    public bool isInteractive = true;

    public bool isActive = false;
    public float maxDragSpeed = 15;
    private float tmpXorY;
    private bool isDraging = false;
    [SerializeField] private Vector3 mouseOffset;
    [SerializeField] private float mouseZCoord;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnMouseDown()
    {
        if (isInteractive)
        {
            isDraging = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mouseOffset = gameObject.transform.position - GetMouseWorldPos();
            rb.freezeRotation = true;
        }
    }

    void OnMouseDrag()
    {
        if (isInteractive)
        {
            Vector3 newPos = GetMouseWorldPos() + mouseOffset;
            rb.MovePosition(new Vector2(newPos.x, newPos.y));
        }
    }

    void OnMouseUp()
    {
        if (isInteractive)
        {
            rb.freezeRotation = false;
            isDraging = false;
            rb.bodyType = RigidbodyType2D.Static;
        }    
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
