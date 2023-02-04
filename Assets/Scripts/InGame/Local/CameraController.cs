using UnityEngine;

namespace Ballmen.InGame 
{
    internal sealed class CameraController : MonoBehaviour
    {
        private static CameraController _instance;
        private Transform _target;
        [SerializeField] private float _lerpSpeed;

        internal static CameraController Singleton => _instance;

        internal void SetTarget(Transform targetTransform)
        {
            _target = targetTransform;
        }

        private void Start()
        {
            _instance = this;
        }

        private void FixedUpdate()
        {
            if (_target != null)
                transform.position = Vector3.Lerp(transform.position, GetPositionToTarget(), _lerpSpeed);
        }

        private Vector3 GetPositionToTarget()
        {
            return new Vector3(
                _target.position.x,
                transform.position.y,
                transform.position.z);
        }
    }
}
