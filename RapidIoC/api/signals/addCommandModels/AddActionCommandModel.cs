using System;
using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    public class AddActionCommandModel : AddCommandBaseModel<ICommand>
    {
        #region Methods
        public static implicit operator AddActionCommandModel(ActionCommand command)
        {
            var createKeyOutcome = Rapid._idContainer.CreateKey(out var key);
            if (!createKeyOutcome)
            {
                throw new Exception(createKeyOutcome.ErrorMessage);
            }
            return new AddActionCommandModel
            {
                _key = key,
                _command = command,
                _once = false
            };
        }

        public static implicit operator AddActionCommandModel(Action action)
        {
            return new ActionCommand(action);
        }
        #endregion
    }

    public class AddActionCommandModel<T_In> : AddCommandBaseModel<ICommand<T_In>>
    {
        #region Methods
        public static implicit operator AddActionCommandModel<T_In>(ActionCommand<T_In> command)
        {
            var createKeyOutcome = Rapid._idContainer.CreateKey(out var key);
            if (!createKeyOutcome)
            {
                throw new Exception(createKeyOutcome.ErrorMessage);
            }
            return new AddActionCommandModel<T_In>
            {
                _key = key,
                _command = command,
                _once = false
            };
        }
        #endregion
    }

    public class AddActionCommandModel<T_In1, T_In2> : AddCommandBaseModel<ICommand<T_In1, T_In2>>
    {
        #region Methods
        public static implicit operator AddActionCommandModel<T_In1, T_In2>(ActionCommand<T_In1, T_In2> command)
        {
            var createKeyOutcome = Rapid._idContainer.CreateKey(out var key);
            if (!createKeyOutcome)
            {
                throw new Exception(createKeyOutcome.ErrorMessage);
            }
            return new AddActionCommandModel<T_In1, T_In2>
            {
                _key = key,
                _command = command,
                _once = false
            };
        }
        #endregion
    }
}