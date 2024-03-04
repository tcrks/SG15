using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testface : MonoBehaviour
{
    public float flinktimer;
    float flinktime;
    [SerializeField]float curflink;
    [SerializeField] SkinnedMeshRenderer skin;
    public bool flink;
    [SerializeField] int eyeindex,fearindex;
    [SerializeField] float flinkSpeed;
    [SerializeField] bool startflink,startshakeEye;
    // The maximum distance the object can move while shaking
    public float shakeAmount = 0.1f;
    // The original position of the object
    public  Transform originalPosition;
    public Vector2 FearEyeRange;
    public float FearDuration;
    public FaceState faceState;
    public Animator anim;

    [Header("TestButton")]
    public GameObject light;
    public Toggle Nor, Fear, Walk, Run, Hold,Light,Idle,Dance;

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    void Start()
    {
        flink = true;
        StartCoroutine(Startflink());


        //////
   if (Nor!=null)
            {
            Nor.onValueChanged.AddListener(delegate { normal(); });
            Fear.onValueChanged.AddListener(delegate { fear(); });
            Hold.onValueChanged.AddListener(delegate { holdlight(); });
            Walk.onValueChanged.AddListener(delegate { walk(); });
            Run.onValueChanged.AddListener(delegate { run(); });
            Idle.onValueChanged.AddListener(delegate { idle(); });
            Dance.onValueChanged.AddListener(delegate { dance(); });
        }

    }

 public void setstate(FaceState state)
    {
        switch (state)
        {
            case FaceState.idle:
                anim.SetBool("idle",true);
                break;
            case FaceState.normal:
                startshakeEye = false;
                skin.SetBlendShapeWeight(fearindex, 0);
                skin.SetBlendShapeWeight(10, 0);
                anim.SetLayerWeight((int)AnimLayer.fear, 0);
                skin.SetBlendShapeWeight(34, 0);
                skin.SetBlendShapeWeight(0, 100);
                break;
            case FaceState.fear:
                startshakeEye = true;
                skin.SetBlendShapeWeight(10,77);
                anim.SetLayerWeight((int)AnimLayer.fear, 1);
                skin.SetBlendShapeWeight(34, 35);
                skin.SetBlendShapeWeight(0,0);
                StartCoroutine(FearCoroutine());
                StartCoroutine(ShakeCoroutine());
                break;
            case FaceState.walk:
    
                anim.SetBool("idle", false);
                anim.SetTrigger("walk");
                break;
            case FaceState.dance:
                anim.Play("dance");
                setstate(FaceState.normal);
                anim.SetBool("idle", false);
                break;
            case FaceState.run:
                anim.SetBool("idle", false);
                anim.SetTrigger("run");
                break;
        }
    
        anim.SetBool("fear", startshakeEye);
    }
    IEnumerator Startflink()
    {
        while (startflink)
        {
                float currentTime = 0f;
            while (flink)
            {
            float   t = currentTime / .25f;
                curflink = Mathf.Lerp(curflink, 100, t);
                skin.SetBlendShapeWeight(eyeindex, curflink);
                currentTime += Time.deltaTime;
                if (currentTime >= .25f)
                {
                    flink = false;
                }
                yield return null;
            }
            yield return null;
            float closetime = 0f;
            while (!flink)
            {

             float   t = currentTime / 1.5f;

                curflink = Mathf.Lerp(curflink, 0, t);
                skin.SetBlendShapeWeight(eyeindex, curflink);
                closetime += Time.deltaTime;
                if (closetime >= 1.5f)
                {
                    yield return new WaitForSeconds(flinktimer);
                    flink = true;
                }
                yield return null;
            }
            yield return null;

        }
    
    
    }
    private IEnumerator FearCoroutine()
    {
        while (startshakeEye)
        {

            skin.SetBlendShapeWeight(fearindex, Random.Range(FearEyeRange.x, FearEyeRange.y));
            yield return new WaitForSeconds(FearDuration);
        }
    }
    private IEnumerator ShakeCoroutine()
    {

        Vector3 _originalPosition = originalPosition.transform.localPosition;


        while (startshakeEye)
        {

            // Generate a random offset for the shake effect
            Vector3 randomOffset = Random.insideUnitSphere * shakeAmount/1000;
           // randomOffset.z = _originalPosition.z;

            // Apply the random offset to the object's position
            originalPosition.localPosition = _originalPosition + randomOffset;

            // Wait for the end of frame before continuing
            yield return null;
        }

        transform.position = _originalPosition;
    }

    public void open()
    {
        startflink = true;
        StartCoroutine(Startflink());
    }
    void normal()
    {
        startshakeEye = false;
        setstate(FaceState.normal);

  
    }
    void dance()
    {

        setstate(FaceState.dance);
    }
    void idle()
    {
        setstate(FaceState.idle);
    }
    void fear()
    {
        anim.SetLayerWeight(0, Fear.isOn?0:100);
        if (Fear.isOn)
        {
            setstate(FaceState.fear);
            anim.SetLayerWeight((int)AnimLayer.fear, 1);
            startshakeEye = true;
        }
        else
        {
            setstate(FaceState.normal);
            anim.SetLayerWeight((int)AnimLayer.fear, 0);
            startshakeEye = false;
        }
    }
    private void Update()
    {
        if (light != null)
            if (Light.isOn )
            {
                light.transform.Rotate(Vector3.up, 35 * Time.deltaTime);
            }

    }
    void walk()
    {
        setstate(FaceState.walk);
    }
    void run()
    {
        setstate(FaceState.run);
    }
    void holdlight()
    {
        if (Hold.isOn)
        {
            anim.SetLayerWeight((int)AnimLayer.lefthand, 1);
        }
        else
        {
            anim.SetLayerWeight((int)AnimLayer.lefthand, 0);
        }
   
  
    }
}
public enum FaceState
{
   idle,
    normal,
    fear,
    walk,run,dance
}
public enum AnimLayer
{
    none,
    fear,
    lefthand
}