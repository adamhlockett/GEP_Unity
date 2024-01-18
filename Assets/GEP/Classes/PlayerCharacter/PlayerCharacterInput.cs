using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterInput : MonoBehaviour
{
    private Grid grid;
    private int x, y;
    public Canvas canvas;
    [SerializeField] private Transform testIcon;

    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    //public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    private void Start()
    {
        //grid = new Grid(6, 4, 10f, new Vector3(0,0));
    }

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnClick(InputValue value)
    {
        //Instantiate(testIcon, Mouse.current.position.ReadValue() / canvas.scaleFactor, Quaternion.identity, canvas.GetComponent<Transform>());
        //Debug.Log(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x + "x" + Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y + "y");
        //grid.SetValue(Mouse.current.position.ReadValue() / canvas.scaleFactor, 56);
        //grid.GetXY(Mouse.current.position.ReadValue(), out x, out y);
        //Debug.Log(x + " " + y);
    }

    public void OnRightClick(InputValue value)
    {
        //Debug.Log(grid.GetValue(Mouse.current.position.ReadValue() / canvas.scaleFactor));  
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    //private void OnApplicationFocus(bool hasFocus)
    //{
    //    SetCursorState(cursorLocked);
    //}
    //
    //private void SetCursorState(bool newState)
    //{
    //    Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    //}
}
