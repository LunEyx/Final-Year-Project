using UnityEngine;
using System.Collections;

public class Grayscale : MonoBehaviour
{
    private float power = 0f;
    public Material mat;

    private void Start()
    {
        mat.SetFloat("_Power", power);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }

    public void Increase(float value)
    {
        power = Mathf.Clamp(power + value, 0f, 1f);
        mat.SetFloat("_Power", power);
    }
}