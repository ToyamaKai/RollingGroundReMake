using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace MPLib
{
    public class MMPObject : MonoBehaviour
    {
        private MPObject m_mpObject = null;

        private IMPObject m_objectInterface = null;

        public IMPObject Interface => m_objectInterface;

        public void Injection(MPObject mpObject)
        {
            m_mpObject = mpObject;
            m_objectInterface = m_mpObject;
        }

        protected virtual void ConstructSelf()
        {
            m_mpObject ??= new MPObject();
            m_objectInterface = m_mpObject;
        }

        protected virtual void TerminateSelf()
        {
            m_objectInterface?.Dispose();
            m_mpObject = null;
            m_mpObject = null;
        }

        private void Awake()
        {
            ConstructSelf();
        }

        private void Start()
        {
            m_objectInterface.Initialize();
        }

        private void Update()
        {
            m_objectInterface?.Tick();
        }

        private void OnEnable()
        {
            ConstructSelf();
        }

        private void OnDisable()
        {
            TerminateSelf();
        }
    }
}
