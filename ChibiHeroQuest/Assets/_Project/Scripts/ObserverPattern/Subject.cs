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
        [SerializeField] private List<IObserver> observers = new List<IObserver>();
        public void AddObserver(IObserver observer) => observers.Add(observer);
        public void RemoveObserver(IObserver observer) => observers.Remove(observer);
        public void NotifyObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.OnNotify();
            }
        }
    }
}
