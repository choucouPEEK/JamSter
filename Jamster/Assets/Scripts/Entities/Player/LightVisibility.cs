using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightVisibility : MonoBehaviour
{
    [SerializeField] private string _EnemyTag="Ennemy";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_EnemyTag))
        {
            MaterialsEnemy lEnemy = other.GetComponent<MaterialsEnemy>();
            lEnemy.InLight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_EnemyTag))
        {
            MaterialsEnemy lEnemy = other.GetComponent<MaterialsEnemy>();
            lEnemy.isInLight = false;
        }
    }


}
