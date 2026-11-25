using UnityEngine;

public class Recoil : MonoBehaviour
{
    public static Recoil Instance { get; private set; }

    public float recoilAmount = 2f;
    public float recoilSpeed = 10f;
    public float returnSpeed = 5f;

    private float currentRecoil;
    private float targetRecoil;

    void Awake() { Instance = this; }

    private float currentX;
    private float targetX;

    void Update()
    {
        targetX = Mathf.Lerp(targetX, 0f, returnSpeed * Time.deltaTime);
        currentX = Mathf.Lerp(currentX, targetX, recoilSpeed * Time.deltaTime);

        float currentY = transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(-currentX, currentY, 0f);
    }

    public void ApplyRecoil(float amount) { targetX += amount; }
}