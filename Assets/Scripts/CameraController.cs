using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    Transform _target;

    [Header("Diminuer pour que la caméra soit plus lente")]
    [SerializeField]
    [Range(0f, 5f)]
    float _lerpTime;
    
    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        _velocity = Vector3.zero;
        Vector3 targetPosition_zOffeset = new Vector3(_target.position.x, _target.position.y, -10f);

        // Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition_zOffeset, ref _velocity, _lerpTime * Time.deltaTime);
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition_zOffeset, _lerpTime * Time.deltaTime);
        //transform.position = new Vector3(newPosition.x, newPosition.y, -10f);
        transform.position = newPosition;
    }

    private void LateUpdate()
    {
        
    }


    #endregion

    #region Methods


    #endregion

    #region Private & Protected

    Vector3 _velocity;

    #endregion
}
