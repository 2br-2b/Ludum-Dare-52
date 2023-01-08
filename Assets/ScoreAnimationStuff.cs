using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ScoreAnimationStuff : MonoBehaviour
{
    float rotation;
    RectTransform tsf;
    [SerializeField] float totalPixelsToMove;
    [SerializeField] float timeToMove;
    float posx;
    float posy;
    Quaternion rot;
    int cycles = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        tsf = GetComponent<RectTransform>();
        rotation = Random.Range(-20f, 20f);
        tsf.rotation = Quaternion.Euler(0, 0, rotation);
        posx = tsf.anchoredPosition.x + Screen.width / 2;
        posy = tsf.anchoredPosition.y + Screen.height / 2;
        rot = tsf.rotation;
    }

    private void Update()
    {
        float q = totalPixelsToMove * (Time.deltaTime / timeToMove);
        posy += q;
        tsf.SetPositionAndRotation(new Vector3(posx, posy, 0), rot);
        print("posy:" + posy);

        
    }

    private void FixedUpdate()
    {
        cycles += 1;
        if (cycles > 300)
        {
            Destroy(transform.gameObject);
        }
    }

}
