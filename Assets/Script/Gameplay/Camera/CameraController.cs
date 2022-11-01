using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region sigaton

    public static CameraController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    private Vector3 OriginPos;
    public Transform camera;
    public float startZoomDelay = 0.9f;

    [Header("Follow Target")]
    public Transform followTarget;
    public float followSmooth = 0.6f;
    public float followZoom;
    public Vector3 followOffset;
    private bool isFollow = false;

    [Header("Focus")]
    public LeanTweenType moveEase = LeanTweenType.easeOutSine;
    public LeanTweenType zoemEase = LeanTweenType.easeOutSine;
    public float zoomTarget;
    public float zoomingDuration;
    public float zoomingGoDuration;
    public Transform zoomingPos;
    private float originZoom;
    public float holdingDuration = 1.4f;

    [Space]
    public bool isZoomingPhase = false;

    private IEnumerator Start()
    {
        OriginPos = transform.position;
        originZoom = camera.localPosition.z;

        isZoomingPhase = true;
        yield return new WaitForSeconds(startZoomDelay);
        zoomIn();
    }

    public void zoomIn()
    {
        isZoomingPhase = true;
        LeanTween.moveLocalZ(camera.gameObject, zoomTarget, zoomingDuration).setEase(zoemEase);
        LeanTween.moveLocal(gameObject, zoomingPos.position, zoomingGoDuration).setEase(moveEase).setOnComplete(() =>
        {
            StartCoroutine("zoomDelay");
        });
    }

    public void zoomOut()
    {
        LeanTween.moveLocalZ(camera.gameObject, originZoom, zoomingDuration).setEase(zoemEase);
        LeanTween.moveLocal(gameObject, OriginPos, zoomingGoDuration).setEase(moveEase).setOnComplete(() =>
        {
            isFollow = false;
            isZoomingPhase = false;
        });
    }

    public IEnumerator zoomDelay()
    {
        yield return new WaitForSeconds(holdingDuration);
        zoomOut();
    }

    public void setFollowTarget(Transform target)
    {
        isFollow = true;
        followTarget = target;
        LeanTween.moveLocalZ(camera.gameObject, followZoom, zoomingDuration).setEase(zoemEase);
    }

    public void DefollowTarget()
    {
        zoomOut();
    }

    private void LateUpdate()
    {
        if (isFollow)
        {
            if (followTarget == null)
            {
                isFollow = false;
                return;
            }
            transform.position = Vector3.Lerp(
                transform.position, 
                math.setVector3AtZ(followTarget.position, transform.position.z) + followOffset, 
                followSmooth
                );
        }
    }
}
