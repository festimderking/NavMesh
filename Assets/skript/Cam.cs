using UnityEngine;

public class Cam : MonoBehaviour {
//Begrenzungen für die Kamera-Position
    float minX = -35f, maxX = 35f;
    float minZ = -30f, maxZ = 45f;
    float minY = 2.5f, maxY = 10f; // Zoom-Grenzen (Y-Achse)

    public float moveSpeed = 10f;
    public float zoomSpeed = 10f;

    void Update()
    {
        // Bewegung auf X/Z mit WSAD und Pfeiltasten
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow))
            moveZ -= 1f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.DownArrow))
            moveZ += 1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow))
            moveX += 1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow))
            moveX -= 1f;

        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);

        // Zoomen mit Mausrad (Y-Achse)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float newY = Mathf.Clamp(transform.position.y - scroll * zoomSpeed, minY, maxY);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        // Begrenzung der Kamera-Position auf X/Z
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}