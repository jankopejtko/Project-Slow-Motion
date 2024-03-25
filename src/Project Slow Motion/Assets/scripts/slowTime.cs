using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class slowTime : MonoBehaviour
{
    [SerializeField] ActionBasedContinuousMoveProvider provider;
    [SerializeField] ActionBasedSnapTurnProvider turnProvider;

    [SerializeField] CharacterController playerCh;
    float timeScale;
    float maxSpeed;
    bool SlowSpeedEnabled = false;
    [SerializeField] float minSpeed;
    private List<GameObject> gunList = new List<GameObject>();
    [SerializeField] AudioSource audio;

    private void Start()
    {
        gunList.AddRange(GameObject.FindGameObjectsWithTag("gun"));
        maxSpeed = provider.moveSpeed;
    }
    void Update()
    {
        if (SlowSpeedEnabled)
        {
            foreach (GameObject gun in gunList)
            {
                try
                {
                    gun.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
                }
                catch
                {
                    Debug.Log("error");
                }
            }
            timeScale = (playerCh.velocity.magnitude - minSpeed) / (maxSpeed - minSpeed);
            if (timeScale < minSpeed)
            {
                timeScale = minSpeed;
                audio.Play();
            }
            if (timeScale >= 1)
            {
                timeScale = 1;
            }
            turnProvider.debounceTime = timeScale / 3;
            Time.timeScale = timeScale;
        }
    }
    public void SlowSpeedEnable()
    {
        SlowSpeedEnabled = true;
    }
    public void SlowSpeedDisable()
    {
        SlowSpeedEnabled = false;
        setToDefault();
    }
    public void setToDefault()
    {
        Time.timeScale = 1;
        foreach (GameObject gun in gunList)
        {
            try
            {
                gun.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
            }
            catch
            {
                Debug.Log("error");
            }
        }
        provider.moveSpeed = 3f;
        turnProvider.debounceTime = 0.3f;
    }
}
