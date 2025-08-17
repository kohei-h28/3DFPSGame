using UnityEngine;
using Unity.Netcode;
using System;
/// <summary>
/// 
/// </summary>
public enum GameState
{
    Playing,
    GameOver
}

[Serializable]
public struct ScoreEntry : INetworkSerializable, IEquatable<ScoreEntry>
{
    /// <summary>
    /// �v���C���[(�N���C�A���g)�̈�ӂ�Id
    /// </summary>
    public ulong ClientId;

    /// <summary>
    /// ���̃v���C���[�̃X�R�A
    /// </summary>
    public int Score;

    /// <summary>
    /// �f�[�^�𐶐�����Ƃ��Ɏg�p����R���X�g���N�^
    /// </summary>
    /// <param name="id"></param>
    /// <param name="score"></param>
    public ScoreEntry(ulong id,int score)
    {
        ClientId = id; 
        Score = score;
    }

    /// <summary>
    /// �X�R�A�̃f�[�^�������v���C���[���𔻒肷��
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(ScoreEntry other)
    {
        return ClientId == other.ClientId;
    }

    /// <summary>
    /// �T�[�o�[���̏����Ŏg���A��������l�̏�������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serializer"></param>
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientId);
        serializer.SerializeValue(ref Score);
    }
}
