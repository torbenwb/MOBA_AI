using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IIntListener
{
    public GameObject heroPrefab;
    public Unit hero;
    public float respawnTime = 10f;
    public List<IAbility> abilities;

    public FollowCamera followCamera;
    public MoveToPosition moveToPosition;
    public AttackTarget attackTarget;
    public bool followHero = false;

    public void IntUpdate(IntWrapper i){
        if (i <= 0) Invoke("Respawn", respawnTime);
    }

    public void Respawn(){
        hero = Instantiate(heroPrefab, transform.position, Quaternion.identity).GetComponent<Unit>();
        followCamera = FindObjectOfType<FollowCamera>();
        moveToPosition = hero.GetComponent<MoveToPosition>();
        attackTarget = hero.GetComponent<AttackTarget>();

        followCamera.TargetPosition = hero.transform.position;
        hero.Health.listeners.Add(gameObject);

        abilities = new List<IAbility>(hero.GetComponents<IAbility>());
    }

    private void Awake()
    {
        
        Respawn();
    }

    // Update is called once per frame
    void Update(){
        CameraControl();
        SetMoveTarget();
        abilities[0].Request = Input.GetKey(KeyCode.Q);
    }

    void CameraControl()
    {   
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 f = Camera.main.transform.forward; f.y = 0f;
        Vector3 r = Camera.main.transform.right; r.y = 0f;
        Vector3 moveDirection = (f.normalized * y) + (r.normalized * x);

        followCamera.MoveDirection = moveDirection;

        if (!hero) return;
        if (!followHero) return;
        if (moveDirection.magnitude <= 0f){
            Vector3 heroScreenPosition = Camera.main.WorldToScreenPoint(hero.transform.position);
            Vector3 center = Camera.main.WorldToScreenPoint(followCamera.origin);
            float distanceFromCenter = (center - heroScreenPosition).magnitude;
            if (distanceFromCenter > 100) followCamera.TargetPosition = hero.transform.position;
        }
    }

    void SetMoveTarget()
    {
        if (!hero) return;
        if (!moveToPosition) return;
        if (!Input.GetMouseButton(1)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Default")) moveToPosition.Destination = hit.point;
            else if (hit.collider.gameObject.layer != gameObject.layer) {
                moveToPosition.destinationSet = false;
                attackTarget.target = hit.collider.gameObject;
            }
        }
    }

    public static Vector3 MouseTargetPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
