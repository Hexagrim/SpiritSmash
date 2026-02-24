using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFromOrigin : MonoBehaviour
{
    [Header("Origin object to point from")]
    public Transform originObject;
    public SpriteRenderer CursorDash, CursorBlob;
    MouseDash mouseDash;
    BlobFireMech blobFireMech;
    public Color active, disabled;
    private void Start()
    {
        mouseDash = FindFirstObjectByType<MouseDash>();
        blobFireMech = FindFirstObjectByType<BlobFireMech>();
    }
    void Update()
    {
        if (blobFireMech.playerCanShoot)
        {
            CursorBlob.color = active;
        }
        else
        {
            CursorBlob.color = disabled;

        }

        if (mouseDash.canDash)
        {
            CursorDash.color = active;
        }
        else
        {
            CursorDash.color = disabled;
        }


        if (originObject == null) return;

        Cursor.visible = false;       // Hide the OS cursor
        Cursor.lockState = CursorLockMode.Confined; // Keeps cursor in game window (optional)

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; 

        // Put the cursor exactly at muse
        transform.position = mousePos;


        Vector3 direction = mousePos - originObject.position;

        // Calculate Rotation yes
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(0f, 0f, angle);



    }
}
