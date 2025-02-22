using System.Collections;
using UnityEngine;

public class CopsLight : MonoBehaviour
{
    [SerializeField] GameObject[] _light;

    bool _switchLight;

    private IEnumerator Start()
    {
        while (true)
        {
            _switchLight = !_switchLight;

            _light[0].SetActive(_switchLight);
            _light[1].SetActive(!_switchLight);

            yield return new WaitForSeconds(Random.Range(.4f,.6f));
        }
    }
}
