using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseAim : MonoBehaviour
{

    private Vector2 mousePos;
    public GameObject cube;

    public LayerMask worldMask;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        AimPos();
    }

    private void AimPos()
    {

        mousePos = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        Physics.Raycast(ray, out RaycastHit hit, worldMask);

        if(hit.collider)
        {
            Vector3 dirToCursor = (new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position).normalized;

            Quaternion lookRot = Quaternion.LookRotation(dirToCursor, Vector3.up);

            transform.rotation = lookRot;
           // Debug.Log(mousePos);
        }

        // Aim reticle
        cube.transform.position = new Vector2(mousePos.x,mousePos.y);
    }
    // viewport cordinates , world to viewport method.
}
