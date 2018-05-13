using UnityEngine;
using ScriptableObjects;

namespace TOJAM2018.Gameplay
{
    /// <summary>
    /// Player runtime set, which contains a list of PlayerData instances.
    /// </summary>
    [CreateAssetMenu(menuName = "TOJAM2018/PlayerRuntimeSet")]
    public class PlayerRuntimeSet : RuntimeSet<PlayerData> { }
}