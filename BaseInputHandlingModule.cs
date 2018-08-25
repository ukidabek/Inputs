using UnityEngine;

namespace BaseGameLogic.Inputs
{
    public abstract class BaseInputHandlingModule<T> : MonoBehaviour where T : BaseInputSource
    {
        [SerializeField] protected int _playerID = 0;

        [SerializeField] private T _currentinputSource = null;
        public T CurrentInputSource { get { return _currentinputSource; } }

        [SerializeField] protected BaseInputCollector inputCollector = null;
        public BaseInputCollector InputCollector
        {
            get { return this.inputCollector; }
            protected set { inputCollector = value; }
        }

        protected T ConvertToInputSourceDefinition(BaseInputSource source)
        {
            if (source is T) return source as T;

            return null;
        }

        protected void Start()
        {
            if (InputCollectorManager.Instance != null)
            {
                InputCollector = InputCollectorManager.Instance.GetInputCollector(_playerID);
                _currentinputSource = ConvertToInputSourceDefinition(InputCollector.CurrentInputSourceInstance);

                InputCollector.InputSourceChanged -= InputSourceChanged;
                InputCollector.InputSourceChanged += InputSourceChanged;
            }
        }

        private void OnDestroy()
        {
            if (InputCollector != null)
            {
                InputCollector.InputSourceChanged -= InputSourceChanged;
            }
        }

        private void InputSourceChanged(BaseInputSource source)
        {
            _currentinputSource = ConvertToInputSourceDefinition(source);
        }
    }
}
