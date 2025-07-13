using Snake;
using UnityEngine;

public class MovingPlatform : ActivatableBase {
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitTime = 1f;

    private int currentIndex = 0;
    private bool waiting = false;

    bool _isActive = false;

    private void Update() {
        if (!_isActive)
            return;

        if (waypoints.Length == 0 || waiting) return;

        Transform target = waypoints[currentIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if (Vector3.Distance(transform.position, target.position) < 0.01f) {
            // StartCoroutine(WaitThenAdvance());
            currentIndex = (currentIndex + 1) % waypoints.Length;
        }
    }

    // private System.Collections.IEnumerator WaitThenAdvance()
    // {
    //     waiting = true;
    //     yield return new WaitForSeconds(waitTime);
    //     currentIndex = (currentIndex + 1) % waypoints.Length;
    //     waiting = false;
    // }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    //     {
    //         collision.transform.SetParent(transform);
    //     }
    // }
    //
    // private void OnCollisionExit(Collision collision)
    // {
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    //     {
    //         collision.transform.SetParent(null);
    //     }
    // }
    public override void Activate() {
        _isActive = !_isActive;
    }
}