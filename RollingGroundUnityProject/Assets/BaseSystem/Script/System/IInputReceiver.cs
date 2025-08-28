using UnityEngine;
using UnityEngine.InputSystem;

namespace RollingGround
{
    public interface IInputReceiver
    {
        public virtual void OnMove(InputAction.CallbackContext context) { }
    }
}
