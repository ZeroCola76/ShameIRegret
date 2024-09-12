using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 15f; // �޸��� �ӵ�
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    private bool isDashing;
    private float dashTime;
    public float dashSpeed = 10f;
    public float dashDuration = 4f;
    private Vector3 moveDirection; // ����: �̵� ������ ������ ���� �߰�
    private Vector3 dashDirection; // �뽬 ������ ����ϴ� ����
    private Vector3 lastMoveDirection; // ������ �̵� ������ ������ ����
    private bool isRunning; // �޸��� ���¸� ��Ÿ���� ����
    public float rotationSpeed = 5f;
    private bool canParry = true;  // �и� ���� ����
    private bool isParrying;
    public Transform cameraTransform;

    public bool isZero;
    public bool isVelvet;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastMoveDirection = Vector3.forward;
    }

    private void Update()
    {
        moveDirection = Vector3.zero;

        if (!isDashing)
        {
            // WASD Ű �Է� üũ
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveDirection += Vector3.back;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveDirection += Vector3.right;
            }
        }

        if (moveDirection != Vector3.zero)
        {
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0; // y�� ���� ����
            Quaternion cameraRotation = Quaternion.LookRotation(cameraForward);
            moveDirection = cameraRotation * moveDirection;
            lastMoveDirection = moveDirection;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        Debug.Log(isGrounded);

        // ��� �Է� ó��
        if (Input.GetKeyDown(KeyCode.R) && !isDashing /*&& moveDirection != Vector3.zero*/)
        {
            StartDash();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.Q) && canParry)
        {
            StartCoroutine(ParryRoutine());
        }

        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            moveDirection = dashDirection;
            if (dashTime <= 0)
            {
                EndDash();
            }
        }

        float currentSpeed = isDashing ? dashSpeed : (isRunning ? runSpeed : moveSpeed);

        // �̵� ������ �ִ� ��쿡�� ȸ��
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }

        rb.MovePosition(transform.position + moveDirection.normalized * currentSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (isParrying && collision.gameObject.CompareTag("Bullet"))
        {
            ParryBullet(collision.gameObject);
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        dashDirection = lastMoveDirection.normalized;
    }

    private void EndDash()
    {
        isDashing = false;
    }

    private void ParryBullet(GameObject bullet)
    {
        Debug.Log("Bullet parried!");
        bullet.SetActive(false);
    }

    private IEnumerator ParryRoutine()
    {
        isParrying = true;
        canParry = false;
        yield return new WaitForSeconds(0.5f); 
        isParrying = false;
        yield return new WaitForSeconds(5);
        canParry = true;
    }
}
