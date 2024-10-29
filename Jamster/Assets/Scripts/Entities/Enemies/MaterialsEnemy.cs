using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsEnemy : Enemy
{
    [SerializeField] private Material _BaseMaterial;
    [SerializeField] private Material _DarkMaterial;
    [SerializeField] public float reducingSpeed;
    private MeshRenderer _Renderer;
    public bool isInLight = true;
    // Start is called before the first frame update
    void Start()
    {
        _Renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInLight)
        {

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
        //Alpha au max ici
        _Renderer.material = _DarkMaterial;
        //_DarkMaterial
        //if ()
        //{
        //    isInLight = true;
        //}
    }
}
