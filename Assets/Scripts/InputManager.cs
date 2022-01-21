using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool useKeyboardInput;
    public bool useMouseInput;
    public bool useKinectInput;

    private InputKinect inputKinect;
    private InputMouse inputMouse;
    private InputKeyboard inputKeyboard;
    private GameManager gameManager;

    private TimeController inputLeap;

    private bool keyboardInput;
    private bool mouseInput;
    private bool kinectInput;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        inputKinect = FindObjectOfType<InputKinect>();
        inputMouse = FindObjectOfType<InputMouse>();
        inputKeyboard = FindObjectOfType<InputKeyboard>();
    }

    void Update()
    {
        if(inputLeap is null) inputLeap = FindObjectOfType<TimeController>();

        if (inputLeap.isColliding)
        {
            if (keyboardInput) RemoveKeyboardInput();
            if (kinectInput) RemoveKinectInput();
            if (mouseInput) RemoveMouseInput();
        }
        else
        {
            if (useKeyboardInput && !keyboardInput) SetupKeyboardInput();
            if (useKinectInput && !kinectInput) SetupKinectInput();
            if (useMouseInput && !mouseInput) SetupMouseInput();

            if (!useKeyboardInput && keyboardInput) RemoveKeyboardInput();
            if (!useKinectInput && kinectInput) RemoveKinectInput();
            if (!useMouseInput && mouseInput) RemoveMouseInput();
        }

    }

    private void SetupMouseInput()
    {
        inputMouse.onDetectedGestureUp += gameManager.MoveRowUp;
        inputMouse.onDetectedGestureDown += gameManager.MoveRowDown;
        inputMouse.onDetectedGestureLeft += gameManager.MoveRowLeft;
        inputMouse.onDetectedGestureRight += gameManager.MoveRowRight;
        mouseInput = true;
    }

    private void SetupKeyboardInput()
    {
        inputKeyboard.onDetectedGestureUp += gameManager.MoveRowUp;
        inputKeyboard.onDetectedGestureDown += gameManager.MoveRowDown;
        inputKeyboard.onDetectedGestureLeft += gameManager.MoveRowLeft;
        inputKeyboard.onDetectedGestureRight += gameManager.MoveRowRight;
        keyboardInput = true;
    }

    private void SetupKinectInput()
    {
        Debug.Log("SUBSCRIBING TO EVENT");
        inputKinect.onDetectedGestureUp += gameManager.MoveRowUp;
        inputKinect.onDetectedGestureDown += gameManager.MoveRowDown;
        inputKinect.onDetectedGestureLeft += gameManager.MoveRowLeft;
        inputKinect.onDetectedGestureRight += gameManager.MoveRowRight;
        kinectInput = true;
    }

    private void RemoveKinectInput()
    {
        inputKinect.onDetectedGestureUp -= gameManager.MoveRowUp;
        inputKinect.onDetectedGestureDown -= gameManager.MoveRowDown;
        inputKinect.onDetectedGestureLeft -= gameManager.MoveRowLeft;
        inputKinect.onDetectedGestureRight -= gameManager.MoveRowRight;
        kinectInput = false;
    }

    private void RemoveMouseInput()
    {
        inputMouse.onDetectedGestureUp -= gameManager.MoveRowUp;
        inputMouse.onDetectedGestureDown -= gameManager.MoveRowDown;
        inputMouse.onDetectedGestureLeft -= gameManager.MoveRowLeft;
        inputMouse.onDetectedGestureRight -= gameManager.MoveRowRight;
        mouseInput = false;
    }

    private void RemoveKeyboardInput()
    {
        inputKeyboard.onDetectedGestureUp -= gameManager.MoveRowUp;
        inputKeyboard.onDetectedGestureDown -= gameManager.MoveRowDown;
        inputKeyboard.onDetectedGestureLeft -= gameManager.MoveRowLeft;
        inputKeyboard.onDetectedGestureRight -= gameManager.MoveRowRight;
        keyboardInput = false;
    }
}