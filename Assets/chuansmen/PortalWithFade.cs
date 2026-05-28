using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PortalWithFade : MonoBehaviour
{
    public Transform destination;        // 另一个传送门的位置
    public string targetTag = "Player";  // 玩家标签
    public Image fadeImage;              // 全屏黑色图片
    public float fadeDuration = 0.5f;    // 淡入淡出时间

    private bool isTeleporting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTeleporting && other.CompareTag(targetTag))
        {
            StartCoroutine(TeleportWithFade(other.gameObject));
        }
    }

    IEnumerator TeleportWithFade(GameObject player)
    {
        isTeleporting = true;

        // 淡出（变黑）
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = t / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 传送玩家
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            player.transform.position = destination.position;
            cc.enabled = true;
        }
        else
        {
            player.transform.position = destination.position;
        }
        // 等待一帧，防止移动脚本覆盖旋转
        yield return null;

        // 设置朝向（只改 Y 轴）
        Vector3 newEuler = player.transform.eulerAngles;
        newEuler.y = destination.eulerAngles.y;
        player.transform.eulerAngles = newEuler;

        // 可选：再等一帧确保旋转生效
        yield return null;
        // 淡入（恢复）
        t = fadeDuration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            float alpha = t / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 防止短时间内反复传送
        yield return new WaitForSeconds(0.3f);
        isTeleporting = false;
    }
}