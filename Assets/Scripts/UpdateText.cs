using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateText : MonoBehaviour
{
    #region Exposed
    [SerializeField] private IntVariable _beersCollected;
    

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _label = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        _label.text= _beersCollected.m_Value.ToString();
    }

    private void FixedUpdate()
    {
        
    }

    #endregion

    #region Methods

    #endregion

    #region Private & Protected

    private TextMeshProUGUI _label;

    #endregion
}
