using Unity.Entities;
using Unity.Tiny.Text;

public class BestTimeSystem : ComponentSystem
{

    public static float currentTimer = 0f;
    static bool playTimer = false;

    protected override void OnUpdate()
    {               
        if (playTimer)
            currentTimer += Time.DeltaTime;

        Entities.WithAll<CurrentTime>().ForEach((ref CurrentTime currentTime) =>
        {
            string text = string.Format("{0:0.00}", currentTimer);

            TextLayout.SetEntityTextRendererString(EntityManager, currentTime.entity, text);
        });

        Entities.WithAll<BestTime>().ForEach((ref BestTime bestTime) =>
        {
            string text;
            if (PlayfabSystem.bestTime == 0f)
                text = "best time: -";
            else
                text = "best time: " + string.Format("{0:0.00}", PlayfabSystem.bestTime);

            TextLayout.SetEntityTextRendererString(EntityManager, bestTime.entity, text);
        });
    }

    public static void ResetTimer()
    {
        currentTimer = 0f;
        playTimer = true;
    }

    public static void PlayTimer()
    {
        playTimer = true;
    }

    public static void StopTimer()
    {
        playTimer = false;
    }
}
