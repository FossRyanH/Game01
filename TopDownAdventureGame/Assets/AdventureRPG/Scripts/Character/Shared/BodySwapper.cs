using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BodySwapper : MonoBehaviour
{
    #region Bodies
    [field: SerializeField] public GameObject FemaleBodyPrefab { get; private set; }
    [field: SerializeField] public GameObject MaleBodyPrefab { get; private set; }
    GameObject _body;
    #endregion

    public void InstantiateBody(BodyType bodyType)
    {
        if (_body != null) Destroy(_body);

        switch (bodyType)
        {
            case BodyType.Female:
                _body = Instantiate(FemaleBodyPrefab, transform.position, Quaternion.identity);
                break;
            case BodyType.Male:
                _body = Instantiate(MaleBodyPrefab,  transform.position, Quaternion.identity);
                break;
            default:
                Debug.Log("No body Type Selected");
                break;
        }

        _body.transform.SetParent(transform);
        _body.transform.localPosition = Vector3.zero;
        _body.transform.localRotation = Quaternion.identity;
    }
}
public enum BodyType { Female = 0, Male = 1 }
