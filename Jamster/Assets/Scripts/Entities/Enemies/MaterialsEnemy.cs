using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsEnemy : MonoBehaviour 
{
    [SerializeField] private Material _BaseMaterial;
    [SerializeField] private Material _DarkMaterial;
    [SerializeField] public float reducingSpeed;
    private MeshRenderer _Renderer;
    public bool isInLight = true;
    private Color _Color;
    private float _ReducingSpeed = .001f;
    // Start is called before the first frame update
    void Start()
    {
        _Renderer = GetComponent<MeshRenderer>();
        _Color = _DarkMaterial.color;

    }

    // Update is called once per frame
    void Update()
    {
        if(!isInLight)
        {

            ChangeAlpha();

        }
    }
    public void InLight()
    {
        if (isInLight)
        {
            _Renderer.material = _BaseMaterial;
        }
        
    }
    private void ChangeAlpha()
    {
        //_Color.a = 1;
        _DarkMaterial.color = _Color;
        _Renderer.material = _DarkMaterial;
        _Color.a -= reducingSpeed * Time.deltaTime;
        Debug.Log(_Color.a);
        if (_Color.a <= 0)
        {
            isInLight = true;
        }

    }
}
