using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform referenceTransform;
    [SerializeField] private Transform parentTransform;

    [Header("Setting")]
    [SerializeField] private float collisionOffset = 0.3f; //To prevent Camera from clipping through Objects
    [SerializeField] private float cameraSpeed = 15f; //How fast the Camera should snap into position if there are no obstacles
    [SerializeField, Range(0.5f, 10.0f)] private float cameraRotateSpeed = 1.0f;

    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraRotation;
    private Vector3 defaultPos;
    private Vector3 directionNormalized;
    private float defaultDistance;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.localPosition;
        directionNormalized = defaultPos.normalized;
        parentTransform = transform.parent;
        defaultDistance = Vector3.Distance(defaultPos, Vector3.zero);

        cameraOffset = new Vector3(referenceTransform.position.x + transform.position.x, referenceTransform.position.y + transform.position.y, referenceTransform.position.z + transform.position.z);
        transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, cameraRotation.z);

        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // LateUpdate is called after Update
    void LateUpdate()
    {
        // ï«Ç…ìñÇΩÇÁÇ»Ç¢ÇÊÇ§Ç…ìñÇΩÇËîªíË
        Vector3 currentPos = defaultPos;
        RaycastHit hit;
        Vector3 dirTmp = parentTransform.TransformPoint(defaultPos) - referenceTransform.position;
        if (Physics.SphereCast(referenceTransform.position, collisionOffset, dirTmp, out hit, defaultDistance))
        {
            currentPos = (directionNormalized * (hit.distance - collisionOffset));

            transform.localPosition = currentPos;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, Time.deltaTime * cameraSpeed);
        }

        // ÉJÉÅÉâÇâÒì]
        cameraOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * cameraRotateSpeed, Vector3.up) * Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * cameraRotateSpeed, Vector3.right) * cameraOffset;
        cameraOffset = cameraOffset.normalized;
        transform.position = referenceTransform.position + cameraOffset * 2.0f;
        transform.LookAt(referenceTransform.position);
    }
}