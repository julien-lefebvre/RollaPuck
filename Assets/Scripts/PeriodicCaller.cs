using UnityEngine;

public class PeriodicCaller : MonoBehaviour {

    [SerializeField] private float interval = 2f;
    [SerializeField] private Transform collectiblePrefab;
    [SerializeField] private Transform projectilePrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int maxCollectibles = 10;
    private int collectibleCount;
    private float time;
    private bool shouldCall;
    
    private void Start() {
        time = 0f;
        shouldCall = true;
    }
    
    private void Update() {
        time += Time.deltaTime;
        while(time >= interval) {
            if (collectibleCount < maxCollectibles && shouldCall) {
                SpawnCollectible();
                SpawnProjectile();
                collectibleCount++;
            }
            time -= interval;
        }
    }
    
    private void SpawnCollectible() {
        float x = Random.Range(-9f, 9f);
        float y = 1f;
        float z = Random.Range(-9f, 9f);
        Instantiate(collectiblePrefab, new Vector3(x, y, z), Quaternion.identity);
    }

    private void SpawnProjectile() {
        float maxAngularVelocity = 5f;
        float throwForce = Random.Range(2f, 4f);

        float x = Random.Range(0, 2) == 0 ? -12.5f : 12.5f;
        float y = 3f;
        float z = Random.Range(-9f, 9f);

        Vector3 position = new Vector3(x,y,z);
        Vector3 throwDirection = (playerTransform.position - position + transform.up * 20f).normalized;
        Vector3 randomAngularVelocity = new Vector3(Random.Range(-maxAngularVelocity, maxAngularVelocity), Random.Range(-maxAngularVelocity, maxAngularVelocity), Random.Range(-maxAngularVelocity, maxAngularVelocity));
        
        Quaternion randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        GameObject cup = Instantiate(projectilePrefab, new Vector3(x, y, z), randomRotation).gameObject;
        Rigidbody cupRigidbody = cup.GetComponent<Rigidbody>();
        
        cupRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        cupRigidbody.angularVelocity = randomAngularVelocity;
    }

    public void DecrementCollectibleCount() {
        collectibleCount--;
    }

    public void StopCalling() {
        shouldCall = false;
    }
}
