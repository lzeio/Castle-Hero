using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public UpgradeableComponent _upgradeable;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent<UpgradeableComponent>(out UpgradeableComponent upgradeable))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if(_upgradeable != null)
                    {
                        _upgradeable.ToggleUI(false);
                    }
                    _upgradeable = upgradeable;
                    _upgradeable.ToggleUI(true);
                }
            }
        }
        else
        {
            if (_upgradeable != null && Input.GetMouseButtonDown(1))
            {
                _upgradeable.ToggleUI(false);
            }
        }
    }

}

   

