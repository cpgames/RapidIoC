using System.Collections.Generic;
using System.Linq;

namespace cpGames.core.RapidIoC.impl
{
    internal class BindingCollection : IBindingCollection
    {
        #region Fields
        private readonly Dictionary<IKey, IBinding> _bindings = new();
        private readonly Dictionary<IKey, IBinding> _discardedBindings = new();
        #endregion

        #region IBindingCollection Members
        public int GetBindingCount(bool includeDiscarded)
        {
            return 
                _bindings.Count + 
                (includeDiscarded ? _discardedBindings.Count : 0);
        }

        public Outcome FindBinding(IKey key, bool includeDiscarded, out IBinding? binding)
        {
            if (_bindings.TryGetValue(key, out binding) ||
                (includeDiscarded && _discardedBindings.TryGetValue(key, out binding)))
            {
                return Outcome.Success();
            }
            return Outcome.Fail($"Binding with key <{key}> not found.");
        }

        public Outcome BindingExists(IKey key)
        {
            return !_bindings.ContainsKey(key) ?
                Outcome.Fail($"Binding with key <{key}> not found.") :
                Outcome.Success();
        }

        public Outcome Bind(IKey key, out IBinding? binding)
        {
            if (!FindBinding(key, false, out binding))
            {
                binding = new Binding(key);
                var addCommandResult = binding.RemovedSignal.AddCommand(() =>
                {
                    if (_discardedBindings.ContainsKey(key))
                    {
                        _discardedBindings.Remove(key);
                    }
                    if (_bindings.ContainsKey(key))
                    {
                        _bindings.Remove(key);
                    }
                }, key, true);
                if (!addCommandResult)
                {
                    return addCommandResult;
                }
                _bindings.Add(key, binding);
            }
            return Outcome.Success();
        }

        public Outcome Unbind(IKey key)
        {
            return
                FindBinding(key, true, out var binding) &&
                UnbindInternal(key, binding!);
        }

        public Outcome ClearBindings()
        {
            var bindingKeys = _bindings.Keys.ToList();
            foreach (var key in bindingKeys)
            {
                var unbindOutcome = Unbind(key);
                if (!unbindOutcome)
                {
                    return unbindOutcome;
                }
            }
            return Outcome.Success();
        }

        public Outcome BindValue(IKey key, object value)
        {
            return
                Bind(key, out var binding) &&
                binding!.SetValue(value);
        }

        public Outcome MoveBindingFrom(IKey key, IBindingCollection collection)
        {
            var clearCommandsOutcome =
                FindBinding(key, false, out var binding) &&
                collection.MoveBindingTo(binding!) &&
                binding!.RemovedSignal.ClearCommands();
            if (!clearCommandsOutcome)
            {
                return clearCommandsOutcome;
            }
            _bindings.Remove(key);
            return Outcome.Success();
        }

        public Outcome MoveBindingTo(IBinding binding)
        {
            return
                Bind(binding.Key, out var localBinding) &&
                localBinding!.Consume(binding);
        }
        #endregion

        #region Methods
        private Outcome UnbindInternal(IKey key, IBinding binding)
        {
            if (!binding.Empty && !binding.Discarded)
            {
                var getBindingOutcome = binding.GetValue<SignalBase>(out var signal);
                if (getBindingOutcome)
                {
                    var clearCommandsOutcome = signal!.ClearCommands();
                    if (!clearCommandsOutcome)
                    {
                        return clearCommandsOutcome;
                    }
                }
                binding.Discarded = true;
                _discardedBindings.Add(key, binding);
            }
            _bindings.Remove(key);
            return Outcome.Success();
        }
        #endregion
    }
}