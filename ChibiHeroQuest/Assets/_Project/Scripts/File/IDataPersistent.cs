/*
 * Source File: IDataPersistent.cs
 * Author: YuHsuan Chen
 * Student Number: 301448975
 * Date Last Modified: 2025-03-09
 * 
 * Program Description:
 * This program is interface for game object which need to use in save and load game.
 * 
 * Revision History:
 * - 2025-03-09: Initial version created.
 */

namespace Platformer397
{
    public interface IDataPersistent
    {
        void LoadData(GameState data);
        void SaveData();
    }
}