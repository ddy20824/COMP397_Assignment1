namespace Platformer397
{
    public interface IDataPersistent
    {
        void LoadData(GameState data);
        void SaveData();
    }
}