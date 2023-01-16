using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LES ENUMS DOIVENT ÊTRE EN DEHORS DE LA CLASSE
enum WaypointMode
{
    LOOP,
    PINGPONG
}
public class WaypointPlaftorm : MonoBehaviour
{
    #region Exposed
    [SerializeField]
    private WaypointMode _mode = WaypointMode.LOOP;

    [SerializeField]
    private Transform[] _waypoints;

    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private float _reachTolerance = 0.1f;


    #endregion

    #region Unity Lifecycle

    private void Awake()
    {

    }

    void Start()
    {
        transform.position = _waypoints[0].position;
        _targetWaypointIndex = 1;
    }

    void Update()
    {
        Vector3 currentWaypointPosition = _waypoints[_targetWaypointIndex].position;

        Vector3 position = Vector3.MoveTowards(transform.position, currentWaypointPosition, _speed * Time.deltaTime);
        transform.position = position;

        if (Vector3.Distance(transform.position, currentWaypointPosition) <= _reachTolerance)
        {

            switch (_mode)
            {
                case WaypointMode.LOOP:
                    Loop();
                    break;

                case WaypointMode.PINGPONG:
                    PingPong();
                    break;

                default:
                    break;
            }



        }
    }

    private void FixedUpdate()
    {

    }

    #endregion

    #region Methods

    private void Loop()
    {
        _targetWaypointIndex++;
        if (_targetWaypointIndex >= _waypoints.Length)
        {
            _targetWaypointIndex = 0;
        }
    }

    private void PingPong()
    {
        if (_isForward)
        {
            _targetWaypointIndex++;
            if (_targetWaypointIndex >= _waypoints.Length - 1)
            {
                _isForward = false;
            }
        }
        else
        {
            _targetWaypointIndex--;
            if (_targetWaypointIndex <= 0)
            {
                _isForward = true;
            }
        }
    }

    #endregion

    #region Private & Protected

    private int _targetWaypointIndex;
    private bool _isForward = true;

    #endregion
}
