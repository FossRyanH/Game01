using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components
    [field: SerializeField] public BodySwapper CharacterRig { get; private set; }
    #endregion

    #region Stats
    #endregion

    #region Testing Vars
    [SerializeField] bool isFemale = true;
    #endregion

    void Awake()
    {
        CharacterRig = GetComponentInChildren<BodySwapper>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetBodyType();
    }

    void SetBodyType()
    {
        CharacterRig.InstantiateBody(isFemale);
    }
}
