using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Cinemachine;

public class CameraZoomAndUnitInteraction : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera; 
    [SerializeField] private Transform newLookPoint; //����� � ������� ������ ����� ���������
    [SerializeField] private Transform LookBasePoint; // �������� ����� �� ������� ������ �������� ������
    [SerializeField] private Transform currentLookPoint;
    [SerializeField] private float zoomSpeed; // �������� �����������/���������
    [SerializeField] private float zoomValueMax; // �������� �� ������� ������ ����� ������������
    [SerializeField] private float zoomValueMin; // ����������� �������� fov ������
    private float currentZoomDistance; // �������� �������� fov ������

    [SerializeField] private Animator animator;
    public enum TypeFov 
    {
        Zoom,
        OutZoom,
        Default,
    }

    public TypeFov Fov = TypeFov.Default;

    private void Awake()
    {

    }
    private void Start()
    {
        currentZoomDistance = virtualCamera.m_Lens.OrthographicSize;
        currentLookPoint = LookBasePoint;
    }


    void Update()
    { 
        virtualCamera.m_Lens.OrthographicSize = currentZoomDistance;
        virtualCamera.Follow = currentLookPoint;
        virtualCamera.LookAt = currentLookPoint;
        switch (Fov)
        {
            case TypeFov.Zoom:
                CameraZooming();
                break;

            case TypeFov.OutZoom:
                CameraOutZoming();
                break;

            default:
                Fov = TypeFov.Default;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("collection point"))
        {
            Fov = TypeFov.Zoom;
            currentLookPoint = newLookPoint;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("collection point"))
        {
            Fov = TypeFov.OutZoom;
            currentLookPoint = LookBasePoint;
        }
    }
    private void CameraZooming()
    {
        currentZoomDistance = Mathf.MoveTowards(currentZoomDistance, zoomValueMin, Time.deltaTime * zoomSpeed);
        animator.SetBool("isUnitsClose", true);
    }

    private void CameraOutZoming() // ������ ���� ��� ������
    {
        currentZoomDistance = Mathf.MoveTowards(currentZoomDistance, zoomValueMax, Time.deltaTime * zoomSpeed);
        animator.SetBool("isUnitsClose", false);
    }

    private void UiInterfaceOpen() // �������� ����������
    {
        
    }
    private void UiInterfaceClose() // �������� ����������
    {

    }
}
