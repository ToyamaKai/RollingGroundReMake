using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// キャラクターをスポーンさせるためのクラス
/// </summary>
public class CharacterSpowner
{
    private GameObject m_parentObject; // 生成したオブジェクトの親としてセットするオブジェクト

    /// <summary>
    /// オブジェクトと位置を指定してキャラクターをスポーンさせるメソッド
    /// </summary>
    /// <param name="character"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    public void SpawnCharacter(GameObject character, Vector3 position, Quaternion rotation)
    {
        var characterObject = Object.Instantiate(character, position, rotation);
        characterObject.transform.SetParent(m_parentObject.transform);
        characterObject.name = character.name;
    }

    /// <summary>
    /// 親オブジェクトとして使用するオブジェクトをセットするメソッド
    /// </summary>
    /// <param name="parentObject"></param>
    public void SetParentObject(GameObject parentObject)
    {
        m_parentObject = parentObject;
    }
}
