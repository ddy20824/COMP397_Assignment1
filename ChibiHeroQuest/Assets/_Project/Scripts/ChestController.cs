/*
 * Source File: ChestController.cs
 * Author: Sylker Teles, YuHsuan Chen
 * Student Number: 301448975
 * Date Last Modified: 2025-03-07
 * 
 * Program Description:
 * This program manages chests interact
 * Reference from the assets from © 2019 Flying Saci Game Studio
 * 
 * Revision History:
 * - 2025-03-07: Initial version created. Open by iteract input.
 */

using System;
using UnityEngine;

namespace Platformer397
{
    public class ChestController : MonoBehaviour
    {
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private bool isOpen { get; set; }
        [SerializeField] private ItemData chestContent;
        [SerializeField] private InputReader input;
        private bool isPlayerAround;
        private Animator animator;

        void OnEnable()
        {
            input.Interact += HandleInteract;
        }
        private void OnDisable()
        {
            input.Interact -= HandleInteract;
        }

        void Start()
        {
            animator = GetComponent<Animator>();
            input.EnablePlayerActions();
        }

        public void Open()
        {
            if (!isOpen)
            {
                isOpen = true;
                animator.Play("Open");
                GameState.Instance.AddInventory(chestContent);
            }
        }

        public void Close()
        {
            if (isOpen)
            {
                isOpen = false;
                animator.Play("Close");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == playerTag)
            {
                isPlayerAround = true;
            }
        }

        private void HandleInteract()
        {
            if (isPlayerAround)
            {
                Open();
            }
        }
    }
}
