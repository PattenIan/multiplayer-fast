using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public IEnumerator Shake(float Magnitude, float Duration)
    {
        print("SHAKE");
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < Duration) 
        {
            float x = Random.Range(-1f,1f) * Magnitude;
            float y = Random.Range(-1f, 1f) * Magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
