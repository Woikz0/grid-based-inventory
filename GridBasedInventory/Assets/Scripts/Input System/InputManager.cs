using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public PlayerInputActions playerControls;

    public static UnityEngine.Events.UnityAction OnFire;
    public static UnityEngine.Events.UnityAction OnKarspex;
    public static UnityEngine.Events.UnityAction OnInteract;

    private Vector2 movement;
    private bool isClicked;
    private bool isKarspex;

    private void Start()
    {
        instance = this;

        playerControls.Player.Move.started += Move;
        playerControls.Player.Move.performed += Move;
        playerControls.Player.Move.canceled += Move;

        playerControls.Player.Fire.started += context => OnFire.Invoke();

        playerControls.Player.Karspex.started += context => OnKarspex.Invoke();

        playerControls.Player.Interact.started += context => OnInteract.Invoke();

    }

    private void OnEnable()
    {
        playerControls = new PlayerInputActions();

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public Vector2 GetMovementValue()
    {
        return movement;
    }
    public bool GetFireValue()
    {
        return isClicked;
    }
    public bool GetKarspexValue()
    {
        return isKarspex;
    }

    public Vector2 GetMousePosition()
    {
        return Mouse.current.position.ReadValue();
    }

}
