using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringCatapult : MonoBehaviour
{
    public Camera cam;

    [Header("Catapult")]
    public float DragSensitive = 50f;
    public Transform m_shootPos;
    private Vector3 shootPos { get { return m_shootPos.position; } }
    public Transform apple;

    [Header("Line Renderer")]
    public Transform m_linePosA;
    public Transform m_linePosB;
    public LineRenderer lineA;
    public LineRenderer lineB;

    bool isDragging = false;

    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 direction;
    float distance;

    private void Start()
    {
        lineA.SetPosition(0, m_linePosA.position);
        lineB.SetPosition(0, m_linePosB.position);
    }

    private void Update()
    {
        updateLine();

        Vector3 mousePos = Input.mousePosition;
        mousePos = math.setVector3AtZ(mousePos, 1);

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            OnDragStart(mousePos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd(mousePos);
        }

        if (isDragging)
        {
            OnDrag(mousePos);
        }
    }

    private void OnDragStart(Vector3 mousePos)
    {
        startPoint = cam.ScreenToWorldPoint(mousePos);
        startPoint = math.setVector3AtZ(startPoint, 0);
    }

    private void OnDragEnd(Vector3 mousePos)
    {
        //apple = null;

        lineA.enabled = false;
        lineB.enabled = false;
    }

    private void OnDrag(Vector3 mousePos)
    {
        endPoint = cam.ScreenToWorldPoint(mousePos);
        endPoint = math.setVector3AtZ(endPoint, 0);

        distance = Vector3.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;

        // find draged apple position
        Vector3 newApplePos = shootPos + ((direction * distance * DragSensitive) * -1);

        apple.transform.position = newApplePos;
    }

    private void updateLine()
    {
        lineA.SetPosition(1, apple.position);
        lineB.SetPosition(1, apple.position);
    }
}
