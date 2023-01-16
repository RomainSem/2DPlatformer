using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarDamage : MonoBehaviour
{
    #region Exposed

    [SerializeField] private Sprite[] _sprites;

    [SerializeField] private int _healthPoints = 3;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {

    }

    void Start()
    {
        _graphics = transform.Find("Graphics").gameObject;
        _renderer = _graphics.GetComponent<SpriteRenderer>();

        ChangeSprite();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Weapon"))
        {
            _nbDamage++;
            if (_nbDamage >= _healthPoints)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log(_nbDamage);
                ChangeSprite();
            }
        }
    }

    #endregion

    #region Methods

    private void ChangeSprite()
    {
        _renderer.sprite = _sprites[_nbDamage];
    }

    #endregion

    #region Private & Protected

    private int _nbDamage = 0;
    private GameObject _graphics;
    private SpriteRenderer _renderer;

    #endregion
}
