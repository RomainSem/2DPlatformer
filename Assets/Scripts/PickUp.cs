using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    #region Exposed
    [SerializeField] private IntVariable _beersCollected;
    [SerializeField] private int _score = 1;


    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        _beersCollected.m_Value = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _beersCollected.m_Value += _score;
            Destroy(gameObject);
        }
    }

    #endregion

    #region Methods

    #endregion

    #region Private & Protected



    #endregion
}
