using UnityEngine;
using UnityEngine.UI;

public class Test_Shooting : MonoBehaviour
{
    public GameObject FirePoint;
    public float MaxLength;
    public GameObject[] Prefabs;
    public Text fxNameText;

    [Header("GUI")]
    private int Prefab;


    void Start()
    {
        Counter(0);
    }

    void Update()
    {
        //Single shoot
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
        }

        //To change projectiles
        if (Input.GetKeyDown(KeyCode.A))
        {
            Counter(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Counter(+1);
        }
    }

    void Counter(int count)
    {
        Prefab += count;
        if (Prefab > Prefabs.Length - 1)
        {
            Prefab = 0;
        }
        else if (Prefab < 0)
        {
            Prefab = Prefabs.Length - 1;
        }

        fxNameText.text = Prefabs[Prefab].name;
    }
}
