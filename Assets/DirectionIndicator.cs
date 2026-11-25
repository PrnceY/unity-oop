using TMPro;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI directionText;
    private Transform player;
    private Transform currentTarget;

    public Transform intelTarget;
    public Transform extractionTarget;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        currentTarget = intelTarget;
    }

    void Update()
    {
        if (currentTarget == null || directionText == null) return;

        Vector3 dir = currentTarget.position - player.position;
        dir.y = 0;
        float angle = Vector3.SignedAngle(player.forward, dir, Vector3.up);

        string arrow;
        if (angle > -30 && angle < 30)       arrow = "↑ Forward";
        else if (angle >= 30 && angle < 150)  arrow = "→ Right";
        else if (angle <= -30 && angle > -150) arrow = "← Left";
        else                                   arrow = "↓ Behind";

        directionText.text = arrow;
    }

    public void SetTarget(Transform t) { currentTarget = t; }
}