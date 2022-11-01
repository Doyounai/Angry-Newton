using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultController : MonoBehaviour
{
    public static CatapultController Instance;
    private CatapultManager Manager;

    public Camera cam;

    [Header("Catapult")]
    public float DragSensitive = 50f;
    public Transform m_shootPos;
    private Vector3 shootPos { get { return m_shootPos.position; } }
    public float MaxChargeDistance;
    public float shootForce;
    public Apple apple;
    public Trajectory trajectory;

    [Header("Line Renderer")]
    public Transform m_linePosA;
    public Transform m_linePosB;
    Vector3 linePosA { get { return m_linePosA.position; } }
    Vector3 linePosB { get { return m_linePosB.position; } }
    public LineRenderer lineA;
    public LineRenderer lineB;

    bool isDragging = false;

    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 direction;
    Vector3 force;
    float distance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Manager = GetComponent<CatapultManager>();

        lineA.SetPosition(0, linePosA);
        lineB.SetPosition(0, linePosB);
    }

    private void Update()
    {
        if (apple == null)
            return;

        Vector3 mousePos = Input.mousePosition;
        mousePos = math.setVector3AtZ(mousePos, 1);

        if (CameraController.Instance.isZoomingPhase)
            return;

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

    private void OnDrawGizmos()
    {
        if (apple == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shootPos, MaxChargeDistance);
    }

    public void SetApple(Apple apple)
    {
        this.apple = apple;
        apple.transform.position = shootPos;
    }

    #region drag
    private void OnDragStart(Vector3 mousePos)
    {
        apple.DesactivateRb();

        startPoint = cam.ScreenToWorldPoint(mousePos);
        startPoint = math.setVector3AtZ(startPoint, 0);

        trajectory.Show();
    }

    private void OnDragEnd(Vector3 mousePos)
    {
        apple.ActivateRb();

        apple.Push(this.force);

        trajectory.Hide();
        apple = null;

        Manager.Next();

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

        if(Vector3.Distance(shootPos, newApplePos) > MaxChargeDistance)
        {
            newApplePos = shootPos + ((direction * MaxChargeDistance) * -1);
        }

        apple.transform.position = newApplePos;

        // find force from distance between shootpos and draged apple
        force = direction * shootForce * Vector3.Distance(shootPos, apple.pos);

        //just for debug
        //Debug.DrawLine(startPoint, endPoint);
        trajectory.UpdateDots(apple.pos, force, apple.GetComponent<Rigidbody>().mass);

        // line renderer
        lineA.enabled = true;
        lineB.enabled = true;
        lineA.SetPosition(1, apple.pos);
        lineB.SetPosition(1, apple.pos);
    }
    #endregion
}
