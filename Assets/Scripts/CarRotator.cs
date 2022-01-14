using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
