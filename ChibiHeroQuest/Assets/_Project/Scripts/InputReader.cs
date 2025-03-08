/*
 * Source File: InputReader.cs
 * Author: Class sample, YuHsuanChen
 * Student Number:
 * Date Last Modified: 2025-02-23
 * 
 * Program Description:
 * This program manages the input of player.
 * 
 * Revision History:
 * - 2025-02-01: Initial version created.
 * - 2025-02-23: Add Map, Bag and RebindActions.
 */

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static InputSystem_Actions;

namespace Platformer397
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction Jump = delegate { };
        public event UnityAction Attack = delegate { };
        public event UnityAction Map = delegate { };
        public event UnityAction Bag = delegate { };
        public event UnityAction Pause = delegate { };
        public event UnityAction Interact = delegate { };

        InputSystem_Actions input;
        private void OnEnable()
        {
            if (input == null)
            {
                input = new InputSystem_Actions();
                input.Player.SetCallbacks(this);
            }
        }

        public void EnablePlayerActions()
        {
            input.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                case InputActionPhase.Canceled:
                    Move?.Invoke(context.ReadValue<Vector2>());
                    break;
                default:
                    break;
            }
        }
        public void OnLook(InputAction.CallbackContext context) { }
        public void OnAttack(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                case InputActionPhase.Canceled:
                    Attack?.Invoke();
                    break;
                default:
                    break;
            }
        }
        public void OnInteract(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    Interact?.Invoke();
                    break;
                default:
                    break;
            }
        }
        public void OnCrouch(InputAction.CallbackContext context) { }
        public void OnJump(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    Jump?.Invoke();
                    break;
                default:
                    break;
            }
        }
        public void OnPrevious(InputAction.CallbackContext context) { }
        public void OnNext(InputAction.CallbackContext context) { }
        public void OnSprint(InputAction.CallbackContext context) { }

        public void OnMap(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    Map?.Invoke();
                    break;
                default:
                    break;
            }
        }
        public void OnBag(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    Bag?.Invoke();
                    break;
                default:
                    break;
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    Pause?.Invoke();
                    break;
                default:
                    break;
            }
        }


        public void StartRebind(InputAction actionName, int bindingIndex, System.Action onComplete = null)
        {
            InputAction action = input.asset.FindAction(actionName.name);
            if (action == null)
            {
                Debug.LogError($"Rebind failed: Action '{action}' not found.");
                return;
            }

            action.Disable();
            action.PerformInteractiveRebinding(bindingIndex)
                .WithControlsExcluding("Mouse")
                .OnComplete(callback =>
                {
                    action.Enable();
                    callback.Dispose();
                    // SaveBinding();
                    onComplete?.Invoke();
                })
                .Start();
        }

        public void SaveBinding()
        {
            input.Disable(); // 先關閉，確保修改後能被儲存
            // 確保綁定已經更新到 InputActionAsset
            input.asset.LoadBindingOverridesFromJson(input.asset.SaveBindingOverridesAsJson());

            string bindingOverrides = input.asset.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString("InputBindings", bindingOverrides);
            PlayerPrefs.Save();

            input.Enable(); // 重新啟用
        }

        public void LoadBinding()
        {
            if (PlayerPrefs.HasKey("InputBindings"))
            {
                string bindingOverrides = PlayerPrefs.GetString("InputBindings");
                input.asset.LoadBindingOverridesFromJson(bindingOverrides);
                Debug.Log("Load Binding: " + bindingOverrides);
            }
        }

        public void ResetToDefault(InputAction actionName, int bindingIndex)
        {
            InputAction action = input.asset.FindAction(actionName.name);
            if (action == null)
            {
                Debug.LogError($"Rebind failed: Action '{action}' not found.");
                return;
            }

            if (action.bindings[bindingIndex].isComposite)
            {
                for (var i = bindingIndex + 1; i < action.bindings.Count && action.bindings[i].isPartOfComposite; ++i)
                    action.RemoveBindingOverride(i);
            }
            else
            {
                action.RemoveBindingOverride(bindingIndex);
            }
        }
    }
}
