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
    /// プレイヤー(クライアント)の一意なId
    /// </summary>
    public ulong ClientId;

    /// <summary>
    /// このプレイヤーのスコア
    /// </summary>
    public int Score;

    /// <summary>
    /// データを生成するときに使用するコンストラクタ
    /// </summary>
    /// <param name="id"></param>
    /// <param name="score"></param>
    public ScoreEntry(ulong id,int score)
    {
        ClientId = id; 
        Score = score;
    }

    /// <summary>
    /// スコアのデータが同じプレイヤーかを判定する
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(ScoreEntry other)
    {
        return ClientId == other.ClientId;
    }

    /// <summary>
    /// サーバー側の処理で使う、同期する値の書き込み
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serializer"></param>
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientId);
        serializer.SerializeValue(ref Score);
    }
}
