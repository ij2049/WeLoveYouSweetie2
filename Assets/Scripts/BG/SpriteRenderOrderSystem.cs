using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderOrderSystem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] renderers;

    private void Update()
    {
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.sortingOrder = (int) (renderer.transform.position.y * -100);
        }
    }
}
