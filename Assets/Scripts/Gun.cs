using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.UI;

public class Gun : MonoBehaviour

{
    // [SerializeField] private Ammo AmmoPrefab; これでGameObjectがInstantiateされるのかわからない
    [SerializeField] private GameObject AmmoPrefab; // 弾丸のプレハブ
    [SerializeField] Transform muzzle; // 銃口
    [SerializeField] float bulletSpeed = 4.0f;
    [SerializeField] Slider loadingSlider;
    [SerializeField] GameObject loadingUI;
    private float bulletReloadTime = 2.0f;
    private bool isReloading;

    // InputAction fireAction;

    // public OVRInput.Button triggerButton = OVRInput.Button.PrimaryIndexTrigger; // トリガーボタン
    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        // Vector3 pos = new Vector3(0, 0, 0);
        // Instantiate(AmmoPrefab, pos, Quaternion.identity);
        // var playerInput = GetComponent<PlayerInput>();
        audioSource = gameObject.GetComponent<AudioSource>();
        isReloading = false;
        loadingUI.SetActive(false);
        // fireAction = playerInput.actions["Fire"];
    }

    // Update is called once per frame
    // 現コードでたまが連射されてしまう。どこを消せばいいのかわからない
    // https://developers.meta.com/horizon/documentation/unity/unity-ovrinput/
    void Update()
    {
        // var fireValue = fireAction.ReadValue<bool>();

        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && !isReloading){
            FireBullet();
            StartCoroutine(WaitBulletReload());
            StartCoroutine(LoadingSlider());
        }
        // if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)){
        //     FireBullet();
        // }
    }

    void FireBullet()
    {
        audioSource.PlayOneShot(shootSound);
        StartVibration(1.0f, 1.0f, 0.3f, OVRInput.Controller.RTouch);
        // 弾丸のプレハブを元に、弾丸のインスタンスを作成
        GameObject bullet = Instantiate(AmmoPrefab, muzzle.position, Quaternion.identity); // muzzleはGun.csにある
        // 弾丸に付いているRigidbodyコンポーネントを取得
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        // 弾丸のZ軸方向の速度を設定
        bulletRb.linearVelocity = muzzle.forward * bulletSpeed;
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
        while(loadingSlider.value < 1.0f)
        {
            loadingSlider.value += Time.deltaTime / bulletReloadTime;
            yield return null;
        }
        loadingUI.SetActive(false);
        loadingSlider.value = 0.0f;
    }
}
