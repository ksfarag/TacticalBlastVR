using UnityEngine;


public class Firearm : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject barrel;
    [SerializeField] private float velocity;
    [SerializeField] private bool fullAuto;
    [SerializeField] private int magSize;
    [SerializeField] private float fireRate;
    [SerializeField] protected float hittingForce;
    [SerializeField] protected float damage;

    private bool TriggerPressed = false;
    private bool CanShootAgain = false;  //used to manage rate of fire
    private float TimePassedSinceLastBullet;

    private void Start()
    {
        TimePassedSinceLastBullet = 0f;
    }

    void Update()
    {
        if (TriggerPressed)
        {
            Shoot();
        }
        TimePassedSinceLastBullet += Time.deltaTime;

        if (TimePassedSinceLastBullet >= 1/fireRate)
        {
            CanShootAgain = true;
        }
        else
        {
            CanShootAgain = false;
        }
    }

    public void TriggerPress() //This method is called by the XR Grab Interactable script (Specifically by the Activated event)
    {
        TriggerPressed = true;
        ApplyRecoil(true);
    }

    public void Triggerrelease() //This method is called by the XR Grab Interactable script
    {
        TriggerPressed = false;
        ApplyRecoil(false);
    }

    private void Shoot() 
    {
        if (!CanShootAgain) 
        {
            return; 
        }
        TimePassedSinceLastBullet = 0f;
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.position = barrel.transform.position;
        newBullet.transform.forward = barrel.transform.forward;
        newBullet.GetComponent<Rigidbody>().velocity = barrel.transform.forward* velocity;
        newBullet.GetComponent<Bullet>().SetForce(hittingForce);
        newBullet.GetComponent<Bullet>().SetDamage(damage);
        Destroy(newBullet, 5);
        if (!fullAuto ) { TriggerPressed = false; } // If gun is not FullAuto, Release trigger after shooting once
    }

    private void ApplyRecoil(bool slideForward)
    {
        GetComponent<Animator>().SetBool("Shoot", slideForward);
    }


    public Vector3 GetProjectileDirectrion()
    {
        return barrel.transform.forward;
    }

}
