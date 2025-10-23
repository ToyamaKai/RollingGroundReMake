using UnityEngine;
using UnityEngine.InputSystem;

namespace RollingGround
{
    public interface IInputReceiver
    {
        public virtual void OnMove(InputAction.CallbackContext context) { }
        public virtual void OnBlockSet(InputAction.CallbackContext context) { }
        public virtual void OnDeleteBlock(InputAction.CallbackContext context) { }
    }
}
