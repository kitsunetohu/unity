using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform pivot; // the object being followed
    public Vector3 pivotOffset = Vector3.zero; // offset from target's pivot
    public float distance = 10.0f; // distance from target (used with zoom)
    public float minDistance = 2f;
    public float maxDistance = 15f;
    public float zoomSpeed = 1f;//scale speed

    public float xSpeed = 250.0f;//rotation speed
    public float ySpeed = 120.0f;
    public float damping = 0.02f;//ローテーションに対する抗力

    public bool allowYTilt = true;//允许旋转
    private float targetDistance = 0f;
    private float zoomVelocity = 1f;

    float rotateX = 0;
    float rotateY = 0;



    void Start()
    {
        transform.LookAt(pivot.position);
        var angles = transform.eulerAngles;

        Debug.Log(angles.y);

        targetDistance = distance = (transform.position - pivot.position).magnitude;
        transform.position = pivot.position - transform.forward * distance;
    }

    void LateUpdate()
    {
        if (pivot)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll > 0.0f) targetDistance -= zoomSpeed;
            else if (scroll < 0.0f) targetDistance += zoomSpeed;
            targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);

            // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
            // right mouse button must be held down to tilt/rotate cam
            // or player can use the left mouse button while holding Ctr
            if (Input.GetMouseButton(1) || (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))))
            {
                rotateY = Input.GetAxis("Mouse X") * xSpeed * 0.02f;

                if (allowYTilt)
                {
                    rotateX = -Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                }

            }
            if (!(Input.GetMouseButton(1) || (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))))
            {
                rotateX = Mathf.Lerp(rotateX, 0, damping);
                rotateY = Mathf.Lerp(rotateY, 0, damping);
            }
            transform.RotateAround(pivot.position, Vector3.up, rotateY);
            transform.RotateAround(pivot.position, transform.right, rotateX);
            distance = Mathf.Lerp(distance, targetDistance, 0.3f);
            
            Vector3 v0=transform.forward;
            Vector3 v1=pivot.position-transform.position;
            v0.y=0;//接下来要求y方向的旋转角度，所以这里y置0
            v1.y=0;
            
            Quaternion tmp=Quaternion.FromToRotation(v0,v1);//求v0到v1的旋转矩阵
            transform.rotation=tmp*transform.rotation;//进行旋转
            
            transform.position = pivot.position - transform.forward * distance;
            
        }
    }


    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}