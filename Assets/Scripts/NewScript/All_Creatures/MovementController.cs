using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class MovementController : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float runSpeed = 3.5f;
    public float gravity = -9.8f;
    public float speedAnim;
    public GameObject skeleton;
    private float targetSpeed = 0;
    private bool death;
    public int weak_Damage=30;
    public int large_Damage = 80;
    public bool Aiming = false;
    //public Transform spawnpoint;

    [Space]
    public float smoothTime = 0.1f;
    private float currentSpeed;
    private float currentVelocitySpeed;

    private float currentVelocityRotate;

    [Space]
    public float jumpForce = 1.35f;
    public float jumpSpeedOffset = 0.7f;
    private float jumpVelocity = 0;


    //private bool
    private bool attack_weak;
    private bool attack_large;

    //Cash
    private CharacterController characterController;
    private Animator animator;
    public  Transform camera;
    private GameObject stats;
    private Stats stats_script;

    public float curve_a
    {
        get { return animator.GetFloat("Attack_Curve"); }
    }
    public Animator animator_p
    {
        get { return animator; }
        set { animator = value; }
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsSword", true);
        stats = GameObject.Find("test_2");
        stats_script = GetComponent<Stats>();
        // camera = Camera.main.transform;
    }
    void Update()
    {
        death = animator.GetBool("Death");
        animator.SetFloat("Attack_WeakF", stats_script.Weak_attack_count);
        animator.SetFloat("Attack_LargeF", stats_script.Large_attack);
        animator.SetBool("isGround", characterController.isGrounded);
        if (!animator.GetBool("IsSword") && Input.GetKey(KeyCode.Mouse1))
            Aiming = true;
        else
            Aiming = false;
        animator.SetBool("Aiming", Aiming);
        if (!characterController.isGrounded)
        {
            stats_script.Weak_attack_count = 0;
            stats_script.Large_attack = 0;
            stats_script.Weak_Attack_NOT_COUNT = false;
            stats_script.Large_Attack_NOT_COUNT = false;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Red_Enemy"))
            {
                Destroy(enemy);
            }
        }
    }
    public void Move(Vector2 _input, bool _isAcceleration)
    {
        if (_input != Vector2.zero && !death)
        {
            float targetRotation = Mathf.Atan2(_input.x, _input.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref currentVelocityRotate, smoothTime);
            //Debug.Log("targetRotation" + targetRotation.ToString());
            //Debug.Log("transform.eulerAngles" + transform.eulerAngles.ToString());
        }

        if (_input.x != 0 || _input.y != 0 && !(_input.x != 0 && _input.y != 0) && !death)
        {
            targetSpeed = (_isAcceleration ? runSpeed : walkSpeed) * 1;
        }
        else
        {
            targetSpeed = (_isAcceleration ? runSpeed : walkSpeed) * _input.magnitude;
        }
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocitySpeed, smoothTime);


        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * gravity + Vector3.up * jumpVelocity;
        if (velocity.y > 0)
        {
            velocity.x *= currentSpeed * jumpSpeedOffset;
            velocity.z *= currentSpeed * jumpSpeedOffset;
        }

        characterController.Move(velocity * Time.deltaTime);

        if (jumpVelocity > 0)
            jumpVelocity += gravity * Time.deltaTime;
        else if (jumpVelocity < 0)
            jumpVelocity = 0;

        animator.SetBool("isMove", targetSpeed != 0);
        animator.SetFloat("movementSpeed", targetSpeed * speedAnim);

        if (characterController.isGrounded)
            animator.SetFloat("groudDistance", GetGroundDistance());
        //Debug.Log("Velocity" + velocity.ToString());
        //Debug.Log("currentSpeed" + currentSpeed.ToString());
        //Debug.Log("jumpVelocity" + jumpVelocity.ToString());
        //Debug.Log((targetSpeed * speedAnim).ToString());

    }

    public void Jump()
    {
        if (characterController.isGrounded && !death)
        {
            jumpVelocity = -gravity * jumpForce;
            animator.SetTrigger("Jump");
        }
    }
    public int Damage
    {
        get;set;
    }
    public void Attack(bool strange)
    {
        if (characterController.isGrounded && !death && animator.GetBool("IsSword"))
        {
            if (strange && !stats_script.Weak_Attack_NOT_COUNT)
            {
                stats_script.Weak_attack_count++;
                Damage = weak_Damage;
            }
            else if (!strange && !stats_script.Weak_Attack_NOT_COUNT)
            {
                stats_script.Large_attack++;
                Damage = large_Damage;
                Debug.Log(Damage);
            }
        }
    }

    private float GetGroundDistance()
    {
        float downPoint = transform.position.y + characterController.center.y - characterController.height * 0.5f;
        Vector3 startPoint = transform.position;
        startPoint.y = downPoint + 0.1f;

        if (Physics.Raycast(startPoint, Vector3.down, out RaycastHit hit))
        {
            return (float)System.Math.Round(hit.distance, 2);
        }
        return float.MaxValue;
    }
}
