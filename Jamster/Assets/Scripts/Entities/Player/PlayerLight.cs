using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{

    [SerializeField] public GameObject _LightCircle;
    [SerializeField] private Vector3 _MinScale = new Vector3(4, 0.2f, 4);
    [SerializeField] private Vector3 _MaxScale = new Vector3(30, 0.2f, 30);

    [SerializeField] private float _LightJauge;
    [SerializeField] private float _MaxJauge=100;
    [SerializeField] private float _SpeedJauge=10;

    // Start is called before the first frame update
    void Start()
    {
        RechargeLight();
    }


    // Update is called once per frame
    void Update()
    {
        DepleteLightJauge();
    }

    public void RechargeLight()
    {
        _LightJauge = _MaxJauge;
        ActualiseLightSize();
    }

    private void DepleteLightJauge()
    {
        if (_LightJauge > 0)
        {
            _LightJauge -= _SpeedJauge * ((_LightJauge) / (_MaxJauge/4)) * Time.deltaTime; //gd animation curve
        }
        ActualiseLightSize();
    }

    private void ActualiseLightSize()
    {
        _LightCircle.transform.localScale = _MinScale + (_LightJauge/_MaxJauge )* (_MaxScale - _MinScale);
    }
}
