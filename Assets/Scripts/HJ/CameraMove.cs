using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject target;
    public float distance = 1.0f;
    float xMove;
    float yMove;

    Ray hit;

    PlayerMove pm;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        target = GameObject.Find("Player");
        pm = target.GetComponent<PlayerMove>();
    }

    private void LateUpdate()
    {
        #region 원거리 줌 기능
        if (PlayerState.stunCheck == false)
        {
            xMove += Input.GetAxisRaw("Mouse X");
            transform.rotation = Quaternion.Euler(0, xMove, 0);

            
            if (PlayerState.playerZoomCheck == true)
            {
                if (pm.check == false)
                {
                    yMove += Input.GetAxisRaw("Mouse Y");
                    transform.rotation = Quaternion.Euler(-yMove, xMove, 0);

                    target.transform.rotation = transform.rotation;
                    transform.position = target.transform.position + transform.rotation * new Vector3(0, 0.5f, 0);
                }
            }
            if (PlayerState.playerZoomCheck == false)
            {
                yMove = 0;
                target.transform.rotation = new Quaternion(0, target.transform.rotation.y, 0,
                    target.transform.rotation.w);
                transform.position = target.transform.position - transform.rotation * new Vector3(0, -distance / 3, distance);
            }
        }
        #endregion
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
    }
}
