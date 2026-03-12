using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] _roadSection;
    [SerializeField] int _position = 50;
    [SerializeField] bool _isCreatingSection = false;
    private int _sectionIndex;

    private void Start()
    {
        if (!_isCreatingSection)
        {
            _isCreatingSection = true;
            StartCoroutine(SectionGenerate());
        }
       
    }

    IEnumerator SectionGenerate()
    {
        _sectionIndex= Random.Range(0, _roadSection.Length);
        Instantiate(_roadSection[_sectionIndex], new Vector3(0, 0, _position), Quaternion.identity);
        _position = +50;
        yield return new WaitForSeconds(3);
        _isCreatingSection = false;
    }
}
