using UnityEngine;

[CreateAssetMenu(fileName = "TagCollection", menuName = "Scriptable Objects/TagCollection")]
public class TagCollectionSO : ScriptableObject
{
    [SerializeField] string[] _tags = null;

    public bool HasTag(GameObject _gameobject)
    {
        int _count = _tags.Length;

        for (int i = 0; i < _count; i++)
        {
            if (_gameobject.CompareTag(_tags[i]))
            {
                return true;
            }
        }

        return false;
    }
}
