using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float smoothTime;
	[SerializeField] private float rollTime;
	[SerializeField] private float rollSpeed;
	
	private Vector3 moveDirection;
	private CharacterController controller;
    private Animator animator;
    private float smooth;
    private Transform mainCamera;
    private bool isRuning = false;
    public float curSpeed;
    private float speedSmooth;
	public bool isControll = true;
    public bool isMove = true;
    
	private float curRollTime;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
		if(!isControll)
			return;
		
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
		
		moveDirection = new Vector3(horizontal,0,vertical).normalized;
        animator.SetBool("Walk", (moveDirection.magnitude >= 0.1f));
		
        isRuning = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("Run", isRuning);
		
        if (isRuning){
            curSpeed = Mathf.SmoothDamp(curSpeed,runSpeed,ref speedSmooth,0.2f);
        }else{
            curSpeed = Mathf.SmoothDamp(curSpeed,walkSpeed,ref speedSmooth,0.2f);
        }
		
		if(Input.GetKey(KeyCode.LeftControl)){
			animator.SetTrigger("roll");
			curRollTime = rollTime;
			isControll = false;
			curSpeed = rollSpeed;
            Invoke(nameof(ReturnControll), rollTime);
		}
    }
    private void ReturnControll()
    {
        isControll = true;
        animator.ResetTrigger("roll");
    }
    private void Move()
    {
        controller.Move(new Vector3(0,0,0));
    }
    void FixedUpdate()
	{
        if (moveDirection.magnitude >= 0.1f & isMove)
        {
            float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smooth, smoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);
            Vector3 move = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
            controller.Move(move.normalized * curSpeed * Time.fixedDeltaTime);
        }
	}
}
