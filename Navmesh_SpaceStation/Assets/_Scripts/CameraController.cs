using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
    Faz movimentação da câmera usando mouse.
    Scroll -> zoom
    Mouse nas bordas da tela -> movimentação
*/
public class CameraController : MonoBehaviour
{
    [Header("References")]
    private Camera mainCamera;
    private Vector2 mousePosition;
    private float mouseDistanceToTop;
    private float mouseDistanceToBottom;
    private float mouseDistanceToRight;
    private float mouseDistanceToLeft;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceToBorderToBeginMovement;

    private void Awake()
    {
        mainCamera = Camera.main;   
    }
    private void Update()
    {
        GetMousePosition();
        GetMouseDistanceToScreenExtremeties();
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 moveDirection = GetMoveDirection();
        
        mainCamera.transform.Translate(moveDirection * moveSpeed, Space.World); // Move câmera nos eixos globais X e Z -> Movimentação
        mainCamera.transform.Translate(new Vector3(0, 0, 1) * Input.mouseScrollDelta.y, Space.Self); // Move câmera no eixo local Z -> "Zoom"
    }
    private void GetMousePosition()
    {
        Vector2 mousePositionScreen;
        Vector2 mousePositionViewport;

        mousePositionScreen = Input.mousePosition;
        mousePositionViewport = mainCamera.ScreenToViewportPoint(mousePositionScreen); // Converte a posição do mouse pra coordenadas da viewport

        mousePosition = mousePositionViewport;
    }
    private void GetMouseDistanceToScreenExtremeties()
    {
        mouseDistanceToBottom = mousePosition.y;
        mouseDistanceToTop = 1 - mouseDistanceToBottom;
        mouseDistanceToLeft = mousePosition.x;
        mouseDistanceToRight = 1 - mouseDistanceToLeft;
    }
    private Vector3 GetMoveDirection()
    {
        Vector2 moveDirectionViewport = Vector2.zero;
        Vector3 moveDirectionWorld;

        if(mouseDistanceToBottom <= distanceToBorderToBeginMovement) moveDirectionViewport += Vector2.right;
        else if(mouseDistanceToTop <= distanceToBorderToBeginMovement) moveDirectionViewport += Vector2.left;

        if(mouseDistanceToLeft <= distanceToBorderToBeginMovement) moveDirectionViewport += Vector2.down;
        else if(mouseDistanceToRight <= distanceToBorderToBeginMovement) moveDirectionViewport += Vector2.up;

        moveDirectionWorld = new Vector3(moveDirectionViewport.x, 0, moveDirectionViewport.y);
        moveDirectionWorld.Normalize();
        
        return moveDirectionWorld;
    }
}
