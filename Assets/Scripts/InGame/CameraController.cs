using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _lerpSpeed;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, GetPositionToPlayer(), _lerpSpeed);
    }

    private Vector3 GetPositionToPlayer() 
    {
        return new Vector3(
            _playerTransform.position.x,
            transform.position.y,
            transform.position.z);
    }
}
