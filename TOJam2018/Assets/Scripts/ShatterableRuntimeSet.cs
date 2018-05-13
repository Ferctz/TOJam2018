using ScriptableObjects;
using UnityEngine;

namespace TOJAM2018.Gameplay
{
    /// <summary>
    /// Shatterable runtime set, which contains a list of ShatterOnCollision instances.
    /// </summary>
    [CreateAssetMenu(menuName = "TOJAM2018/ShatterableRuntimeSet")]
    public class ShatterableRuntimeSet : RuntimeSet<ShatterOnCollision> { }
}