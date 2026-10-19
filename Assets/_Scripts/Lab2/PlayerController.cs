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
    public GameObject deathPaticlePF;
    public GameObject coinPaticlePF;
    public GameObject winPaticlePF;
    public ParticleSystem moveParticle;

    public Vector3 particleOffset = new Vector3(0f, -0.3f, 0f);

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

    private void Update()
    {
        if (moveParticle == null)
            return;

        bool isMoving = movementX != 0f || movementZ != 0f;
        var emission = moveParticle.emission;
        emission.enabled = isMoving;
    }

    private void LateUpdate()
    {
        if (moveParticle == null)
            return;

        moveParticle.transform.position = transform.position + particleOffset;
        moveParticle.transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            if(coinPaticlePF != null)
                Instantiate(coinPaticlePF, other.transform.position, Quaternion.identity);

            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(deathPaticlePF != null)
            {
                Vector3 hitPoint = collision.GetContact(0).point;
                Instantiate(deathPaticlePF, hitPoint, Quaternion.identity);
            }

            SoundManager.Instance.PlayLose();
            Destroy(gameObject);
            WinText.SetActive(true);
            WinText.GetComponent<TextMeshProUGUI>().color = Color.red;
            WinText.GetComponent<TextMeshProUGUI>().text = "You Lose";
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            float volume = Mathf.Clamp01(collision.relativeVelocity.magnitude / 10f);
            SoundManager.Instance.PlayWallHit(volume);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count == 1)
        {
            if (winPaticlePF != null)
                Instantiate(winPaticlePF, transform.position, Quaternion.identity);

            WinText.SetActive(true);
            SoundManager.Instance.PlayWin();
        }
    }
}
