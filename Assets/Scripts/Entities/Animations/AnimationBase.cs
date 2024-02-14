using UnityEngine;

namespace Assets.Scripts.Entities.Animations
{
    internal class AnimationBase : MonoBehaviour
    {
        protected SpriteRenderer SpriteRenderer;
        protected Frame[] Frames;
        protected int CurrentFrame = 0;
        protected float CurrentTime = 0;

        public struct Frame
        {
            public Sprite Image;
            public Vector3 Shift;
            public float Time;

            public Frame(Sprite image, Vector3 shift, float time)
            {
                this.Image = image;
                this.Shift = shift;
                this.Time = time;
            }
        }

        private void Update()
        {
            CurrentTime += Time.deltaTime;
            if (CurrentTime > Frames[CurrentFrame].Time)
            {
                CurrentFrame += 1;
                if (CurrentFrame >= Frames.Length)
                {
                    Destroy(gameObject);
                    return;
                }

                CurrentTime = 0;
                SpriteRenderer.sprite = Frames[CurrentFrame].Image;
            }

            if (CurrentFrame > 0)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(Frames[CurrentFrame - 1].Shift.y, Frames[CurrentFrame].Shift.y, CurrentTime / Frames[CurrentFrame].Time), transform.localPosition.z);
            }
        }
    }
}