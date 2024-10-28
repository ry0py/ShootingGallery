using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject AmmoPrefab; // 弾丸のプレハブ
    [SerializeField] Transform muzzle; // 銃口
    [SerializeField] float bulletSpeed = 4.0f;
    [SerializeField] Slider loadingSlider;
    [SerializeField] GameObject loadingUI;
    private float bulletReloadTime = 2.0f;
    private bool isReloading;
    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        isReloading = false;
        loadingUI.SetActive(false);
    }

    void Update()
    {
        // OVRInputによる発射
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && !isReloading)
        {
            FireBullet(muzzle.forward); // 銃口方向に発射
            StartCoroutine(WaitBulletReload());
            StartCoroutine(LoadingSlider());
        }

        // マウスクリックによる発射
        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 direction = (hit.point - muzzle.position).normalized;
                FireBullet(direction);
            }
            else
            {
                // レイが何もヒットしなかった場合、画面中央からの方向に発射
                Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                Ray centerRay = Camera.main.ScreenPointToRay(screenCenter);
                FireBullet(centerRay.direction);
            }
            StartCoroutine(WaitBulletReload());
            StartCoroutine(LoadingSlider());
        }
    }

    void FireBullet(Vector3 direction)
    {
        audioSource.PlayOneShot(shootSound);
        StartVibration(1.0f, 1.0f, 0.3f, OVRInput.Controller.RTouch);
        GameObject bullet = Instantiate(AmmoPrefab, muzzle.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = direction * bulletSpeed;
    }

    private void StartVibration(float frequency, float amplitude, float duration, OVRInput.Controller controller)
    {
        StartCoroutine(Vibrate(frequency, amplitude, duration, controller));
    }

    private IEnumerator Vibrate(float frequency, float amplitude, float duration, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controller); // バイブレーションを停止
    }

    private IEnumerator WaitBulletReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(bulletReloadTime);
        isReloading = false;
    }

    private IEnumerator LoadingSlider()
    {
        loadingUI.SetActive(true);
        while (loadingSlider.value < 1.0f)
        {
            loadingSlider.value += Time.deltaTime / bulletReloadTime;
            yield return null;
        }
        loadingUI.SetActive(false);
        loadingSlider.value = 0.0f;
    }
}
