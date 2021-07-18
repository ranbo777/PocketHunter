using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject target;
    public float distance = 1.0f;
    float xMove;
    float yMove;

    public static float shakeTime;
    Vector3 yShakeMove;

    Ray hit;

    PlayerMove pm;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        target = GameObject.Find("Player");
        pm = target.GetComponent<PlayerMove>();

    }

    private void Update()
    {
        if (shakeTime >= 0)
        {
            shakeTime -= Time.deltaTime;

        }

    }

    private void LateUpdate()
    {

        if (shakeTime > 0)
        {
            yShakeMove = new Vector3(0, Random.Range(-0.05f, 0.05f), 0);
        }

        #region 원거리 줌 기능
        xMove += Input.GetAxisRaw("Mouse X");
        transform.rotation = Quaternion.Euler(0, xMove, 0);

        if (PlayerState.playerZoomCheck == true && PlayerState.stunCheck == false)
        {
            if (pm.check == false)
            {
                yMove += Input.GetAxisRaw("Mouse Y");
                transform.rotation = Quaternion.Euler(-yMove, xMove, 0);

                target.transform.rotation = transform.rotation;
                transform.position = target.transform.position + transform.rotation * new Vector3(0, 0.5f, 0) + yShakeMove;
            }
        }
        if (PlayerState.playerZoomCheck == false)
        {
            yMove = 0;
            target.transform.rotation = new Quaternion(0, target.transform.rotation.y, 0,
                target.transform.rotation.w);
            transform.position = target.transform.position - transform.rotation * new Vector3(0, -distance / 3, distance) + yShakeMove;
        }

        #endregion
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
    }
}
