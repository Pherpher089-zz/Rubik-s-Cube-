using UnityEngine;

public class CameraController : RubiksController {

    public Transform target;
    [Range(0,100)]public float distance = 15f;
    [Range(0, 100)] public float xSpeed = 8f;
    [Range(0, 100)] public float ySpeed = 15f;
    [Range(-90,0)] public float yMinLimit = -90f;
    [Range(0, 90)] public float yMaxLimit = 90f;
    [Range(0, 100)] public float distanceMin = .5f;
    [Range(0, 100)] public float distanceMax = 40f;
    [Range(0, 100)] public float smoothTime = 40f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;
        target = GameObject.FindWithTag("RubiksCube").transform;
    }
    private void Update()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        OrbitCam(Input.GetButton("Fire1"), mouseAxis);
    }
    public void OrbitCam(bool rmb, Vector3 mouseAxis)
    {
        if (target)
        {
            if (rmb)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, 1000))
                {
                    velocityX += xSpeed * mouseAxis.x * distance * 0.02f;
                    velocityY += ySpeed * mouseAxis.y * distance * 0.02f;
                }
                   
            }
            rotationYAxis += velocityX;
            rotationXAxis -= velocityY;
            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
            Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
            RaycastHit hit;

            if (Physics.Linecast(target.position, transform.position, out hit, 8))
            {
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
