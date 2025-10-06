using UnityEngine;
using System.Collections.Generic;

public class BlockOutline : MonoBehaviour
{
    [SerializeField]
    private Texture m_outlineTexture;

    HashSet<Renderer> m_previousSelection   = new HashSet<Renderer>();
    //HashSet<Renderer> m_currentSelection    = new HashSet<Renderer>();

    Dictionary<Renderer, Texture> m_originalTexture = new Dictionary<Renderer, Texture>();

    public void UpdateOutline(HashSet<Renderer> newSelection)
    {
        foreach (var renderer in m_previousSelection)
        {
            if (renderer == null || renderer.gameObject == null) continue;

            if (!newSelection.Contains(renderer))
                RestoreTexture(renderer);
        }

        foreach (var renderer in newSelection)
        {
            if (renderer == null || renderer.gameObject == null) continue;

            if (!m_previousSelection.Contains(renderer))
                ApplyOutline(renderer);
        }

        m_previousSelection = new HashSet<Renderer>(newSelection);
    }


    void ApplyOutline(Renderer renderer)
    {
        var block = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(block);

        if(!m_originalTexture.ContainsKey(renderer))
        {
            m_originalTexture[renderer] = renderer.sharedMaterial.GetTexture("_MainTex");
        }

        block.SetTexture("_MainTex", m_outlineTexture);
        renderer.SetPropertyBlock(block);
    }

    void RestoreTexture(Renderer renderer)
    {
        if (renderer == null || renderer.gameObject == null) return;

        var block = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(block);

        if (m_originalTexture.TryGetValue(renderer, out var original))
        {
            block.SetTexture("_MainTex", original);
            renderer.SetPropertyBlock(block);
            m_originalTexture.Remove(renderer);
        }
    }

}
