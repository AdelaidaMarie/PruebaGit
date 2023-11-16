using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    private Rigidbody rb;
    public float mouseSensibility;

    public float maxViewX;
    public int currentLives;
    public int maxLife;
    public int minLife;
    public float minViewX;
    private float rotationX;
    private Camera camera;
    private WeaponController weaponController;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        weaponController = GetComponent<WeaponController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        CameraView();
        if(Input.GetButtonDown("Fire1"))
            if(weaponController.CanShoot())
                weaponController.Shoot();
    }
    /// <summary>
    /// Player Movement
    /// </summary>
    private void MovePlayer()
    { 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = (transform.right * x + transform.forward * z).normalized * speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;
    }
    /// <summary>
    /// Jump Action
    /// </summary>
    private void Jump()
    {
        //Throw a ray down
        Ray ray = new Ray(transform.position, Vector3.down);
        //if the ray collide with something at 1.1m then force up
        if(Physics.Raycast(ray, 1.1f))
        {
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }
    /// <summary>
    /// Camera Rotation view with mouse
    /// </summary>
    private void CameraView()
    {
        //Take Get from Mouse Input X and Y
        float y = Input.GetAxis("Mouse X") * mouseSensibility;
        rotationX += Input.GetAxis("Mouse Y") * mouseSensibility;
        // Cut x rotation
        rotationX = Mathf.Clamp(rotationX, minViewX, maxViewX);
    // Rotate the camera
    camera.transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
        transform.eulerAngles += Vector3.up * y;
    }
    public void DamagePlayer(int quantity)
    {
        currentLives -= quantity;
        if(currentLives <= 0)
        {
            Debug.Log("GAME OVER");
        }
    }
}
