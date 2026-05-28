using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public float scrollSpeedX = 0.2f;
    public float scrollSpeedY = 0.3f;
    private Material waterMat;

    void Start()
    {
        waterMat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;
        waterMat.SetTextureOffset("_BumpMap", new Vector2(offsetX, offsetY));
    }
}