using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    /// <summary>
    /// �����prefab
    /// </summary>
    public GameObject WeaponPrefab;

    /// <summary>
    /// �e�ۂ�prefab
    /// </summary>
    public GameObject BulletPrefab;

    public float FireRate = 1f;
    /// <summary>
    /// ����̒e�̍ő�l
    /// </summary>
    public int MaxAmmo = 10;

    /// <summary>
    /// �����[�h�̎���
    /// </summary>
    public float ReloadTime = 1.5f;
}
