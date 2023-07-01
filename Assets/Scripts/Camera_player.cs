using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_player : MonoBehaviour
{
    [SerializeField] Transform camera_pos;
    Transform player;
    [SerializeField] float speed_pos;
    [SerializeField] float speed_rot;
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, camera_pos.position, Time.fixedDeltaTime * speed_pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, camera_pos.rotation, Time.fixedDeltaTime * speed_rot);
    }
}
