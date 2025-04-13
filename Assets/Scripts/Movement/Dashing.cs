using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("Reference")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("CameraEffects")]
    public PlayerCam cam;
    public float dashFov;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.Q;

    private PlayerInputActions _input;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();

        _input = new();
        _input.Enable();
        _input.Movement.Enable();
    }

    private void Update()
    {
        if (_input.Movement.Dash.IsPressed())
        {
            Dash();
        }
        if (dashCdTimer > 0)
        {
            dashCdTimer -= Time.deltaTime;
        }
    }

    public void Dash(Vector3 force) {
        pm.dashing = true;
        rb.AddForce(force, ForceMode.Impulse);
        Invoke(nameof(delayedDashForce), 0.25f);
        Invoke(nameof(ResetDash), dashDuration);
    }

    private void Dash()
    {
        if (dashCdTimer > 0)
        {
            return;
        }
        else
        {
            dashCdTimer = dashCd;
        }
        pm.dashing = true;
        cam.DoFov(dashFov);

        Transform forwardT;
        if (useCameraForward)
        {
            forwardT = playerCam;
        }
        else
        {
            forwardT = orientation;
        }

        Vector3 direction = GetDirection(forwardT);

        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

        if (disableGravity)
        {
            rb.useGravity = false;
        }

        rb.AddForce(forceToApply, ForceMode.Impulse);
        Invoke(nameof(delayedDashForce), 0.25f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForceToApply;

    private void delayedDashForce()
    {
        
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }
    private void ResetDash()
    {
        pm.dashing = false;

        cam.DoFov(85f);

        if (disableGravity)
        {
            rb.useGravity = true;
        }
    }

    private Vector3 GetDirection(Transform forwardT) {
        Vector2 vector = _input.Movement.MoveDir.ReadValue<Vector2>();
        
        float horizontalInput = vector.x;
        float verticallInput = vector.y;

        Vector3 direction = new Vector3();

        if (allowAllDirections)
        {
            direction = forwardT.forward * verticallInput + forwardT.right * horizontalInput;
        }
        else
        {
            direction = forwardT.forward;
        }

        if (verticallInput == 0 && horizontalInput == 0)
        {
            direction = forwardT.forward;
        }

        return direction.normalized;
    }
}
