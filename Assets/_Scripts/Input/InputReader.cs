using Narry;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Narry
{
    public class InputReader : MonoBehaviour, InputSystem_Actions.IUIActions, InputSystem_Actions.IGameplayActions

    {
        private InputSystem_Actions _gameInput;

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new InputSystem_Actions();
                _gameInput.Gameplay.SetCallbacks(this);
                _gameInput.UI.SetCallbacks(this);
                
                SetGameplay();
            }
        }

        public void SetGameplay()
        {
            _gameInput.Gameplay.Enable();
            _gameInput.UI.Disable();
        }
        
        public void SetUI()
        {
            _gameInput.UI.Enable();
            _gameInput.Gameplay.Disable();
        }

        public event Action<Vector2> MoveEvent;
        public event Action PauseEvent;
        public event Action ResumeEvent;
        
        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                ResumeEvent?.Invoke();
                SetGameplay();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
            Debug.Log($"Phase : {context.phase}, Value : {context.ReadValue<Vector2>()}");
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
                SetUI();
            }
        }
    }

}
