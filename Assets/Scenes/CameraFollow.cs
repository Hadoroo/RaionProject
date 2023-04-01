using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    public float yOffset = 1f; // jarak antara target dan posisi kamera pada sumbu y

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        // Jika player berada di ground, maka kamera akan diatur sedikit lebih tinggi
        RaycastHit2D hit = Physics2D.Raycast(target.position, Vector2.down, yOffset, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            targetCamPos = new Vector3(targetCamPos.x, hit.point.y + yOffset, targetCamPos.z);
        }
        
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}