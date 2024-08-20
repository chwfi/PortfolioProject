using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Category")]
public class Category : ScriptableObject, IEquatable<Category>
{
    [SerializeField]
    private string _codeName;
    [SerializeField]
    private string _displayName;

    public string CodeName => _codeName;
    public string DisplayName => _displayName;

    public bool Equals(Category other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(other, this))
            return true;
        if (GetType() != other.GetType())
            return false;

        return _codeName == other.CodeName;
    }

    public override int GetHashCode() => (CodeName, DisplayName).GetHashCode();

    public override bool Equals(object other) => base.Equals(other);

    public static bool operator ==(Category Ihs, string rhs)
    {
        if (Ihs is null)
            return ReferenceEquals(rhs, null);
        return Ihs.CodeName == rhs || Ihs.DisplayName == rhs;
    }

    public static bool operator !=(Category Ihs, string rhs) => !(Ihs == rhs);
}
