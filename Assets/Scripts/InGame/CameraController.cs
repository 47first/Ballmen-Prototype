using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private void LateUpdate()
    {
        transform.position = new Vector3( 
            _playerTransform.position.x,
            transform.position.y,
            transform.position.z );
    }
}
