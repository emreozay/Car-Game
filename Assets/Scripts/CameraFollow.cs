using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        target = target.GetChild(PlayerPrefs.GetInt("SelectedCar", 0));
    }

    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        if (targetPosition.y < 4)
        {
            targetPosition = new Vector3(targetPosition.x, 4f, targetPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
   

        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, translateSpeed * Time.deltaTime);
    }
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);


        //transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(transform.rotation.eulerAngles, rotation.eulerAngles, ref velocity, rotationSpeed * Time.deltaTime));
    }
}
