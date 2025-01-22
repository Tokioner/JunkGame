using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float shootTime;
    private PlayerMovement movement;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && movement.isControll)
        {
            movement.isMove = false;
            movement.isControll = false;
            movement.curSpeed = 0.0f;
            animator.SetTrigger("shoot");
            Invoke(nameof(ReturnControll),shootTime);
        }
    }
    private void ReturnControll()
    {
        movement.isMove = true;
        movement.isControll = true;
        animator.ResetTrigger("shoot");
        
    }
}