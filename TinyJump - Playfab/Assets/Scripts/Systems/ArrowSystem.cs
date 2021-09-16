using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Tiny;
using UnityEngine;

public class ArrowSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float time = (float)Time.ElapsedTime;
        
        Entities.ForEach((ref Arrow arrow, ref Rotation rotation) =>
        {            

            float3 m_from = new float3(0f, 0f, 45f);
            float3 m_to = new float3(0f, 0f, -45f);
            float m_frequency = 1f;

            quaternion from = quaternion.Euler(m_from);
            quaternion to = quaternion.Euler(m_to);

            float lerp = (float)(0.5f * (1f + math.sin(math.PI * time * m_frequency)));
            quaternion rot = math.nlerp(from, to, lerp);
            rotation.Value = rot;


        }).WithBurst().Run();
    }

}
