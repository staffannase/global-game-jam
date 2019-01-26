using UnityEngine;

public class CollisionController : MonoBehaviour {

    [SerializeField]
    private string targetName;

    void OnTriggerEnter (Collider coll) {
        if (coll.name.Contains (targetName)) {
            transform.parent.SendMessage("MakeFriend");
            gameObject.SetActive(false);
            Destroy(coll.gameObject);
        }
    }
}
