using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Target/GameObject")]
public class GameObjectTarget : TaskTarget
{
    [SerializeField]
    private GameObject _value;

    public override object Value => _value;

    public override bool IsEqual(object target)
    {
        var targetAsGameObject = target as GameObject;
        if (targetAsGameObject == null)
            return false;
        return targetAsGameObject.name.Contains(_value.name);
    }
}

