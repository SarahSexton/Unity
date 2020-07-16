﻿using UnityEngine;

public class Graph : MonoBehaviour
{

    public Transform pointPrefab;

    [Range(10, 100)]
    public int resolution = 10;

    [Range(0, 1)]
    public int function;

    Transform[] points;

    void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.y = 0f;
        position.z = 0f;
        points = new Transform[resolution];
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    void Update()
    {
        float t = Time.time;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            if (function == 0)
            {
                position.y = SineFunction(position.x, t);
            }
            else
            {
                position.y = MultiSineFunction(position.x, t);
            }
            point.localPosition = position;
        }
    }

    float SineFunction(float x, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + t));
    }

    float MultiSineFunction(float x, float t)
    {
        float y = Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(2f * Mathf.PI * (x + 2f * t)) / 2f;
        y *= 2f / 3f;
        return y;
    }
}