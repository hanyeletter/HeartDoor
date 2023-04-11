using UnityEngine;

public class HuarongdaoPuzzle : MonoBehaviour
{
    public float maxDragSpeed = 15;
    private float tmpXorY;
    private bool isDraging = false;
    [SerializeField]private Vector3 mouseOffset;
    [SerializeField] private float mouseZCoord;
    public int puzzleType;  //1heng,2shu
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (puzzleType == 1)
            tmpXorY = gameObject.transform.position.y;
        else
            tmpXorY = gameObject.transform.position.x;
    }
    void OnMouseDown()
    {
        isDraging = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
        rb.freezeRotation = true;
    }

    void OnMouseDrag()
    {
        Vector3 newPos = GetMouseWorldPos() + mouseOffset;
        if (puzzleType == 1)
            rb.MovePosition(new Vector2(newPos.x, gameObject.transform.position.y));
        else
            rb.MovePosition(new Vector2(gameObject.transform.position.x, newPos.y));
    }

    void OnMouseUp()
    {
        rb.freezeRotation = false;
        isDraging = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}