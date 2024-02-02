public interface IRaceRenderAllOutput
{
    IRaceRenderOutput NewSprite(SpriteType spriteType, int layer);
    //IRaceRenderOutput ChangeSprite(SpriteType spriteType);
    IRaceRenderOutput ChangeSprite(SpriteType spriteType);
}