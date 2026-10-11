using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementZ;

    private int count;

    public float speed = 5.0f;

    public TextMeshProUGUI countText;
    public GameObject WinText;

    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        SetCountText();
        WinText.SetActive(false);
    }

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        movementX = inputVector.x;
        movementZ = inputVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movementVector = new Vector3(movementX, 0.0f, movementZ);
        rb.AddForce(movementVector * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
            other.gameObject.SetActive(false);
        count++;
        SetCountText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            WinText.SetActive(true);
            WinText.GetComponent<TextMeshProUGUI>().color = Color.red;
            WinText.GetComponent<TextMeshProUGUI>().text = "You Lose";
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count == 10)
            WinText.SetActive(true);
        
    }
}
