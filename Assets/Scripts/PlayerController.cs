using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody rigidBody;
    public AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip gemSound;
    public AudioClip starSound;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused)        
            return;        

        Move();
        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }
    }

    public void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(xInput, 0, zInput) * moveSpeed;

        direction.y = rigidBody.velocity.y;

        rigidBody.velocity = direction;

        Vector3 facingDir = new Vector3(xInput, 0, zInput);

        if (facingDir.magnitude > 0)
        {
            transform.forward = facingDir;
        }
    }
    
    public void TryJump()
    {
        Ray ray1 = new Ray(transform.position + new Vector3(-0.5f, 0, 0.5f), Vector3.down);
        Ray ray2 = new Ray(transform.position + new Vector3(-0.5f, 0, -0.5f), Vector3.down);
        Ray ray3 = new Ray(transform.position + new Vector3(0.5f, 0, 0.5f), Vector3.down);
        Ray ray4 = new Ray(transform.position + new Vector3(0.5f, 0, -0.5f), Vector3.down);


        if (Physics.Raycast(ray1, 0.7f) || Physics.Raycast(ray2, 0.7f) || Physics.Raycast(ray3, 0.7f) || Physics.Raycast(ray4, 0.7f))
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }                 
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.instance.GameOver();            
        }
        else if (other.CompareTag("Coin"))
        {
            GameManager.instance.AddScore(1);    
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinSound);
        }
        else if (other.CompareTag("Diamond"))
        {
            GameManager.instance.AddScore(2);
            Destroy(other.gameObject);
            audioSource.PlayOneShot(gemSound);
        }
        else if (other.CompareTag("Star"))
        {
            GameManager.instance.AddScore(5);
            Destroy(other.gameObject);
            audioSource.PlayOneShot(starSound);
            GameManager.instance.LevelEnd();
        }
        else if (other.CompareTag("Lava"))
        {
            GameManager.instance.GameOver();
        }
        else if (other.CompareTag("EndGoal"))
        {
            GameManager.instance.LevelEnd();
        }
    }
}
