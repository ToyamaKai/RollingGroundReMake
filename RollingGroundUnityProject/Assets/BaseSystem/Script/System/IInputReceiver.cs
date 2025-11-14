using UnityEngine;
using UnityEngine.InputSystem;

namespace RollingGround
{
    public interface IInputReceiver
    {
        public virtual void OnMove(InputAction.CallbackContext context) { }
        public virtual void OnBlockSet(InputAction.CallbackContext context) { }
        public virtual void OnDeleteBlock(InputAction.CallbackContext context) { }
        public virtual void OnMoveUp(InputAction.CallbackContext context) { }
        public virtual void OnMoveDown(InputAction.CallbackContext context) { }
        public virtual void OnCameraMove(InputAction.CallbackContext context) { }
        public virtual void OnBlockHeightChange(InputAction.CallbackContext context) { }

    }
}
