using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    #region Exposed

    [SerializeField] Transform _respawnPoint;

    #endregion

    #region Unity Lifecycle

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = _respawnPoint.position;
        }
    }

    #endregion

    #region Methods

    #endregion

    #region Private & Protected



    #endregion
}
