using System.Linq;

public class AnimationController
{
    public FrameList[] FrameLists;

    public struct FrameList
    {
        public int CurrentFrame;
        public float CurrentTime;
        public bool CurrentlyActive;

        public FrameList(int frame, float time, bool active)
        {
            CurrentFrame = frame;
            CurrentTime = time;
            CurrentlyActive = active;
        }
    }

    public void UpdateTimes(float time)
    {
        if (FrameLists == null) return;
        for (int i = 0; i < FrameLists.Count(); i++)
        {
            if (FrameLists[i].CurrentlyActive) FrameLists[i].CurrentTime += time;
        }
    }
}