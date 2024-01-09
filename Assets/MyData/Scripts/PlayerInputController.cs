using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController Instance { get; private set; }

    private PlayerInputActions inputActions;

    public float Horizontal { get ; private set; }
    public bool IsJumped { get; private set; }

    private void Awake()
    {
        Instance = this;
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        Horizontal = 0;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        Horizontal = inputActions.Player.Move.ReadValue<float>();
        IsJumped = inputActions.Player.Jump.WasPressedThisFrame();
    }
}
