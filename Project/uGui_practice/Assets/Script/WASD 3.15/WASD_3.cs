 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD_3 : MonoBehaviour
{
    [Tooltip("移動速度")]
    public float walkSpeed = 2;
    [Tooltip("跑步速度")]
    public float runSpeed = 6;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelcocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelcocity;
    float currentSpeed;

    Rigidbody m_rig;
    [Tooltip("跳躍力度")]
    public float jumpForce = 150;
    [Tooltip("跳躍延遲")]
    public float delayTime=.5f;
    int jumpCount=0;
    float JumpCD;

    Animator animator;
    Transform CameraT;

    // Start is called before the first frame update
    void Start()
    {
        m_rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        CameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + CameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y,targetRotation,ref turnSmoothVelcocity,turnSmoothTime) ;
        }

        if (Input.GetButtonDown("Jump") && jumpCount<2)
        {
            m_rig.AddForce(new Vector3(0,1* jumpForce));
            jumpCount += 1;
        }

        if (jumpCount >= 2)
        {
            JumpCD += Time.deltaTime;
            if (JumpCD >= delayTime)
            {
                jumpCount = 0;
                JumpCD = 0;
            }
        }

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelcocity, speedSmoothTime);
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;

        //animator.SetFloat("SpeedPercent", animationSpeedPercent,speedSmoothTime,Time.deltaTime);
    }
}
