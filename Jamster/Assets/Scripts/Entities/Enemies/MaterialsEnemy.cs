using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class MaterialsEnemy : MonoBehaviour 
=======
public class MaterialsEnemy : Enemy
>>>>>>> main
{
    [SerializeField] private Material _BaseMaterial;
    [SerializeField] private Material _DarkMaterial;
    [SerializeField] public float reducingSpeed;
    private MeshRenderer _Renderer;
    public bool isInLight = true;
<<<<<<< HEAD
    private Color _Color;
    private float _ReducingSpeed = .001f;
=======
>>>>>>> main
    // Start is called before the first frame update
    void Start()
    {
        _Renderer = GetComponent<MeshRenderer>();
<<<<<<< HEAD
        _Color = _DarkMaterial.color;
=======
>>>>>>> main
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInLight)
        {
<<<<<<< HEAD
            ChangeAlpha();
=======

>>>>>>> main
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
<<<<<<< HEAD
        _Color.a = 1;
        _DarkMaterial.color = _Color;
        _Renderer.material = _DarkMaterial;
        _Color.a -= reducingSpeed * Time.deltaTime;
        if (_Color.a <= 0)
        {
            isInLight = true;
        }
=======
        //Alpha au max ici
        _Renderer.material = _DarkMaterial;
        //_DarkMaterial
        //if ()
        //{
        //    isInLight = true;
        //}
>>>>>>> main
    }
}
