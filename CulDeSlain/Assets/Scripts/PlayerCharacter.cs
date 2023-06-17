using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : MonoBehaviour {
    [Header("Components")] 
    [SerializeField] private CharacterController CharacterController;
    [SerializeField] private Transform CameraAnchor;
    [SerializeField] private Camera CharacterCamera;
    private PlayerControls Controls;

    [Header("Character Variables")] 
    [SerializeField] private bool GravityEnabled = true; 
    private Vector3 MovementDirection3D;

    [Header("Movement Variables")] 
    [SerializeField] private float MaxMovementSpeed = 4f;
    [SerializeField] private bool IsMoving = false;
    private float MovementSpeed = 0f;
    private float MovementSmoothTime = 0.25f;
    private float MovementVelocity = 0f;

    [Header("Camera Constraints")] 
    [SerializeField] private float CameraXMax = 60f;
    [SerializeField] private float CameraXMin = -45f;
    private float CameraSenetivity => GameManager.Instance.PlayerSettings.MouseSensitivity;

    [Header("Camera Curves")] 
    public AnimationCurve CameraBobb; // Key 1: 0, 0 | Key 2: 0.25f, 1 | Key 3: 0.75f, -1 | Key 4: 1, 0
    private Vector3 CameraBobbVelocity;
    private float CurveTime = 0f;

    [Header("Zoom Variables")]
    [SerializeField] private bool Zooming = false;
    [SerializeField] private bool LockOn = false;
    [SerializeField] public Transform LockOnTarget;
    private float ZoomFOV = 30f;
    private float ZoomTime = 0.5f;
    private float ZoomVelocity = 0f;
    
    [Header("Camera Rotations")] 
    private float RotationX = 0;
    private float RotationY = 0;

    private void Awake() {
        // Creating Controls
        Controls = new PlayerControls();
        
        // Cursor Settings 
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        TryGetComponent(out CharacterController);
        transform.GetChild(0).TryGetComponent(out CharacterCamera);
        
        Controls.Player.Look.performed += Look;
        
        Controls.Player.Movement.performed += OnMovePerformed;
        Controls.Player.Movement.canceled += OnMoveCancelled;

        Controls.Player.Zoom.performed += OnZoom;
        Controls.Player.Zoom.canceled += OnZoomCanceled;
    }

    private void OnEnable() {
        Controls.Enable();
    }

    private void Start() {
        PlayerSettings CurrentSettings = GameManager.Instance.PlayerSettings;
        CharacterCamera.fieldOfView = CurrentSettings.PlayerFOV;
    }
    
    private void OnDisable() {
        Controls.Disable();
    }

    #region Mouse Controls and LateUpdate

    private void Look(InputAction.CallbackContext context) {
        
        Vector2 LookingVectorDelta = context.ReadValue<Vector2>();

        RotationX += LookingVectorDelta.y * CameraSenetivity;
        if (!LockOn) RotationY += LookingVectorDelta.x * CameraSenetivity;
    }

    private void LateUpdate() {
        // Y Rotation (Applied to the Character Capsule)
        transform.rotation = Quaternion.Euler(0f, RotationY, 0f);

        // X Rotation (Applied to the Camera)
        float ClampedEulerX = Mathf.Clamp(RotationX, CameraXMin, CameraXMax);
        CharacterCamera.transform.localEulerAngles = new Vector3(-ClampedEulerX, 0f, 0f);
    }
    
    #endregion
    
    #region Zoom, Movement and Update

    private void OnMovePerformed(InputAction.CallbackContext context) {
        Vector2 MovementDirection = context.ReadValue<Vector2>();
        MovementDirection3D = new Vector3(MovementDirection.x, 0, MovementDirection.y);
        IsMoving = true;
    }

    private void OnMoveCancelled(InputAction.CallbackContext context) {
        IsMoving = false;
        CurveTime = 0;
    }

    private void OnZoom(InputAction.CallbackContext context) { if (!LockOn) Zooming = true; }
    private void OnZoomCanceled(InputAction.CallbackContext context) { if (!LockOn) Zooming = false; }

    // Update is called once per frame
    void Update() {

        PlayerSettings CurrentSettings = GameManager.Instance.PlayerSettings;

        if (CharacterCamera.fieldOfView != CurrentSettings.PlayerFOV)
            CharacterCamera.fieldOfView = CurrentSettings.PlayerFOV;

        #region Movement
        
        Vector3 ForwardRelativeToVerticalInput = transform.forward * MovementDirection3D.z;
        Vector3 RightRelativeToHorizontalInput = transform.right * MovementDirection3D.x;
        Vector3 WorldMoveVector = ForwardRelativeToVerticalInput + RightRelativeToHorizontalInput;
        WorldMoveVector.y = 0;

        if (GravityEnabled) CharacterController.Move(Vector3.down * (9.81f * Time.deltaTime));

        if (IsMoving) MovementSpeed = Mathf.SmoothDamp(MovementSpeed, MaxMovementSpeed, ref MovementVelocity, MovementSmoothTime);
        else MovementSpeed = Mathf.SmoothDamp(MovementSpeed, 0, ref MovementVelocity, 0.1f);
        
        CharacterController.Move(WorldMoveVector * (MovementSpeed * Time.deltaTime));
        
        // Movement Bobbing

        if (IsMoving) {
            CurveTime += Time.deltaTime;
            if (CurveTime >= 1f) CurveTime -= 1f;

            Vector3 CameraPosition = CameraAnchor.position;
            CameraPosition += 0.1f * (MovementSpeed / MaxMovementSpeed) * Mathf.Sin(CameraBobb.Evaluate(CurveTime)) * CurrentSettings.ViewBobbingIntensitiy * CameraAnchor.right;
            CameraPosition += -0.1f * (MovementSpeed / MaxMovementSpeed) * Mathf.Abs(Mathf.Sin(CameraBobb.Evaluate(CurveTime))) * CurrentSettings.ViewBobbingIntensitiy * CameraAnchor.up;
            
            // For audio!
            //if (Mathf.Sin(CameraBobb.Evaluate(CurveTime)) == 1 || Mathf.Sin(CameraBobb.Evaluate(CurveTime)) == -1)
            
            CharacterCamera.transform.position = CameraPosition;
        }
        else {
            CharacterCamera.transform.position = Vector3.SmoothDamp(CharacterCamera.transform.position, CameraAnchor.position, ref CameraBobbVelocity, 0.1f);
        }

        #endregion
        
        #region Zoom
        
        if (Zooming) CharacterCamera.fieldOfView = Mathf.SmoothDamp(CharacterCamera.fieldOfView, ZoomFOV, ref ZoomVelocity, ZoomTime);
        else CharacterCamera.fieldOfView = Mathf.SmoothDamp(CharacterCamera.fieldOfView, 80.0f, ref ZoomVelocity, ZoomTime);

        #endregion
        
        #region Interacting

        if (LockOn) LookAtTarget();

        #endregion
    }
    
    #endregion

    // Takes over the Look controls from the player
    private void LookAtTarget() {
        // Look at from the capsule perspective
        Vector3 targetDir = LockOnTarget.position - transform.position;
        targetDir.y = 0;
 
        Vector3 NewRotation = Vector3.RotateTowards(transform.forward, targetDir, 5f * Time.deltaTime, 0.0F);
        Vector3 NewQuaternion = Quaternion.LookRotation(NewRotation).eulerAngles;
        
        RotationY = NewQuaternion.y;
    }

    public void SetZoomTarget(Transform NewTarget) {
        LockOn = true;
        Zooming = true;
        ZoomFOV = 50f;
        LockOnTarget = NewTarget;
    }

    public void ResetZoomTarget() {
        Zooming = false;
        LockOn = false;
        ZoomFOV = 30f;
        LockOnTarget = null;
    }
}
