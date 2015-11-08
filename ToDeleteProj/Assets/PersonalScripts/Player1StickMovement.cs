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
    bool _isGrounded;
    
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
            //_anim.applyRootMotion = true;
        }
        else
        {
            _isGrounded = false;
            groundNormal = Vector3.up;
            //_anim.applyRootMotion = false;
        }
    }


    // TODO: fix jump to lerp or add force vertically
    public void Jump(float jumpForce)
    {
        _isGrounded = true;
        if (_isGrounded)
            _anim.SetTrigger("Jump");
            transform.Translate(new Vector3(0, jumpForce, 0));
        //transform.position = Vector3.Lerp(transform.position, transform.position, 15f);

    }

    public void Eat(float calorieAmt, float eatTime)
    {
        _anim.SetTrigger("Eat");
    }

    public void StopEating()
    {
        _anim.SetTrigger("DoneEating");
    }

    public void Sleep()
    {
        _anim.SetTrigger("Sleep");
    }

    public void Attack1()
    {
        _anim.SetTrigger("Attack1");
    }

    public void Attack2()
    {
        _anim.SetTrigger("Attack2");
    }

    public void SayGoodbye()
    {
        _anim.SetTrigger("Bye");
    }

    public void StopSayingGoodbye()
    {
        _anim.SetTrigger("DoneBye");
    }

    public void Putup()
    {
        _anim.SetTrigger("Putup");
    }

    public void PutDown()
    {
        _anim.SetTrigger("PutDown");
    }

    public void Talk(float convoTime)
    {
        _anim.SetTrigger("Talk");
    }

    public void StopTalking()
    {
        _anim.SetTrigger("DoneTalk");
    }

    public void TakeUp()
    {
        _anim.SetTrigger("TakeUp");
    }

    public void TakeDown()
    {
        _anim.SetTrigger("TakeDown");
    }
    
}
