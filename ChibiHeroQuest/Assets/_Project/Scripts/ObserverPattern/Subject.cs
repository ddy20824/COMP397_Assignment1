/*
 * Source File: IObserver.cs
 * Author: Class example, Chiayi Lin
 * Student Number: 301448962
 * Date Last Modified: 2025-04-04
 * 
 * Program Description:
 * This program manages observer pattern.
 * 
 * Revision History:
 * - 2025-04-04: Initial version created.
 */
using UnityEngine;
using System.Collections.Generic;

namespace Platformer397
{
    public abstract class Subject : MonoBehaviour
    {
        [SerializeField] private List<IObserver> achieveObservers = new List<IObserver>();
        [SerializeField] private List<IObserver> questObservers = new List<IObserver>();

        public void AddObserver(IObserver observer, ObserverType type)
        {
            if (type == ObserverType.Achievement)
                achieveObservers.Add(observer);
            else
                questObservers.Add(observer);
        }

        public void RemoveObserver(IObserver observer, ObserverType type)
        {
            if (type == ObserverType.Achievement)
                achieveObservers.Remove(observer);
            else
                questObservers.Remove(observer);
        }

        public void NotifyObservers(ObserverType type)
        {
            if (type == ObserverType.Achievement)
            {
                foreach (IObserver observer in achieveObservers)
                {
                    observer.OnNotify();
                }
            }
            else
            {
                foreach (IObserver observer in questObservers)
                {
                    observer.OnNotify();
                }
            }
        }
    }
}
