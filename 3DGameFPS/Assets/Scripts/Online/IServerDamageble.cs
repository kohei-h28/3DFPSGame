using UnityEngine;

public interface IServerDamageble 
{
    /// <summary>
    /// �T�[�o�[�ł̂݁AHP���Z���s��
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="attackClientId"></param>
    /// <param name="hitPoint"></param>
    void ApplyDamageServer(int damage, ulong attackClientId, Vector3 hitPoint);
}
