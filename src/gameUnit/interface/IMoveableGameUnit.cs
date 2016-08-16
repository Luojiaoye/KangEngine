
namespace KangEngine.GameUnit.Interface
{
    public interface IMoveableGameUnit
    {
        float moveSpeed { get; }

        void SetCurrentSpeed<T>(T key);

        void SetSpeed<T>(T key, float speed);

        float GetSpeed<T>(T key);
    }
}
