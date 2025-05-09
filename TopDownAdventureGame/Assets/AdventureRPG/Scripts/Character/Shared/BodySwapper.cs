using UnityEngine;

public class BodySwapper : MonoBehaviour
{
    #region Bodies
    [field: SerializeField] public GameObject FemaleBodyPrefab { get; private set; }
    [field: SerializeField] public GameObject MaleBodyPrefab { get; private set; }
    GameObject _body;
    #endregion

    public void InstantiateBody(bool isFemale)
    {
        if (isFemale)
        {
            _body = Instantiate(FemaleBodyPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            _body = Instantiate(MaleBodyPrefab, transform.position, Quaternion.identity);
        }        
    }
}
