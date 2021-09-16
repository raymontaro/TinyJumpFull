using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Tiny;

public class TryTapSystem : ComponentSystem
{

    //protected override void OnCreate()
    //{
    //    base.OnCreate();
        
    //    RequireSingletonForUpdate<Camera>();
    //}

    protected override void OnUpdate()
    {
        Entities.WithAll<TryTap>().ForEach((ref Tappable tappable) =>
        {
            if (tappable.IsTapped)
            {
                tappable.IsTapped = false;

                Debug.Log("Tapped");                
            }
        });

    }
}