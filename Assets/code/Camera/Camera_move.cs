using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Cameramove : MonoBehaviour
{

    public Camera mainCamera;
    public CinemachineCamera cinemachine;
    public Transform[] targetTransform;
    public float duration = 1.5f;

    private Vector3 originPos;
    private float originSize;

    public static Cameramove instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        originPos = mainCamera.transform.position;
        originSize = mainCamera.orthographicSize;
    }
    public void StartSequence(int i) // 움직임 실행
    {
        cinemachine.gameObject.SetActive(false);
        StartCoroutine(CameraRoutine(i));
    }

    public void Reset() // 기본 카메라
    {
        StartCoroutine(CameraReset());
    }

    public void DoBoss() // 보스전 카메라
    {
        StartCoroutine(CameraDoBoss());
    }

    private IEnumerator CameraRoutine(int i)
    {
        Vector3 startPos = mainCamera.transform.position;
        Vector3 endPos = new Vector3(targetTransform[i].position.x, targetTransform[i].position.y, startPos.z);
        float startSize = mainCamera.orthographicSize;
        cinemachine.Lens.OrthographicSize = 10;
        float endSize = 13f;

        float time = 0f;

        //???? ???? ??? ????
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, t);
            mainCamera.orthographicSize = Mathf.Lerp(startSize, endSize, t);
            yield return null;
        }

        mainCamera.transform.position = endPos;
        mainCamera.orthographicSize = endSize;
    }

    private IEnumerator CameraDoBoss()
    {
        Vector3 startPos = mainCamera.transform.position;
        float startSize = mainCamera.orthographicSize;

        Vector3 endPos = originPos;
        float endSize = cinemachine.Lens.OrthographicSize;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, t); 
            mainCamera.orthographicSize = Mathf.Lerp(startSize, endSize, t);

            yield return null;
        }
        cinemachine.gameObject.SetActive(true);
        mainCamera.transform.position = endPos;
        mainCamera.orthographicSize = endSize;
    }
    private IEnumerator CameraReset()
    {
        Vector3 startPos = mainCamera.transform.position;
        float startSize = mainCamera.orthographicSize;

        Vector3 endPos = originPos;
        float endSize = originSize;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, t);
            mainCamera.orthographicSize = Mathf.Lerp(startSize, endSize, t);

            yield return null;
        }
        cinemachine.gameObject.SetActive(true);
        mainCamera.transform.position = endPos;
        mainCamera.orthographicSize = endSize;
    }
}

