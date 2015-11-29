using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Player1StickMovement : MonoBehaviour {

    [SerializeField]public float _rotateSpeed = 3.0f;
    [SerializeField]public float _speed = 2.0f;
    public Rigidbody _rigid;
    [HideInInspector]
    public Animator _anim;
    Vector3 groundNormal;
    public float _groundCheckDistance = .5f;
    public bool _isGrounded;
    
	// Use this for initialization
	void Start () 
    {
        // assigns necessary game object for movement, throws error elsewise
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        CheckGroundStatus();
	}

    void FixedUpdate()
    {
        // rotate about the y axis and activate animator
        _anim.SetFloat("Speed", CrossPlatformInputManager.GetAxis("Horizontal"));
        transform.Rotate(0, CrossPlatformInputManager.GetAxis("Horizontal") * _rotateSpeed, 0);

        // move transform and set animate var
        _anim.SetFloat("Speed", Mathf.Abs(CrossPlatformInputManager.GetAxis("Vertical")));
        transform.Translate(new Vector3(0, 0, CrossPlatformInputManager.GetAxis("Vertical") * _speed));
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        #if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * _groundCheckDistance));
        #endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, _groundCheckDistance))
        {
            groundNormal = hitInfo.normal;
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
            groundNormal = Vector3.up;
        }
    }

    public void Jump(float jumpForce)
    {
        
        //_isGrounded = true;
        if (_isGrounded)
        {
            _anim.SetTrigger("Jump");
            //transform.position += Vector3.up * 2F;
            transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3.up * 30f), .05f);
            //transform.Translate(new Vector3(0, jumpForce, 0));
        }
            
    }

    /*
    public void Attack1()
    {
        PersonalScripts.PlayerHealth playHealth = GetComponent<PersonalScripts.PlayerHealth>();
        if (playHealth != null)
            if (playHealth.currentHealth > 0)
                _anim.SetTrigger("Attack1");
    }

    public void Attack2()
    {
        PersonalScripts.PlayerHealth playHealth = GetComponent<PersonalScripts.PlayerHealth>();
        if (playHealth != null)
            if (playHealth.currentHealth > 0)
                _anim.SetTrigger("Attack2");
    }
    */
    
}
