using UnityEngine;


public class FireBullet : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject barrel;
    [SerializeField] private float velocity;
    [SerializeField] private bool FullAuto;
    [SerializeField] private int MagSize;
    [SerializeField] private float FireRate;

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

        if (TimePassedSinceLastBullet >= 1/FireRate)
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
    }

    public void Triggerrelease() //This method is called by the XR Grab Interactable script
    {
        TriggerPressed = false;
    }

    private void Shoot() 
    {
        if (!CanShootAgain) { return; }
        TimePassedSinceLastBullet = 0f;
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.position = barrel.transform.position;
        newBullet.GetComponent<Rigidbody>().velocity = barrel.transform.forward* velocity;
        Destroy(newBullet, 5);
        if (!FullAuto ) { TriggerPressed = false; } // If gun is not FullAuto, Release trigger after shooting once
    }


}
