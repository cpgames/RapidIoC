using System;
using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    public class AddActionResultOutCommandModel<T_Result, T_Out> : AddCommandBaseModel<ICommandResultOut<T_Result, T_Out>>
    {
        #region Methods
        public static implicit operator AddActionResultOutCommandModel<T_Result, T_Out>(ActionResultOutCommand<T_Result, T_Out> command)
        {
            var createKeyOutcome = Rapid._idContainer.CreateKey(out var key);
            if (!createKeyOutcome)
            {
                throw new Exception(createKeyOutcome.ErrorMessage);
            }
            return new AddActionResultOutCommandModel<T_Result, T_Out>
            {
                _key = key,
                _command = command,
                _once = false
            };
        }
        #endregion
    }

    public class AddActionResultOutCommandModel<T_Result, T_In, T_Out> : AddCommandBaseModel<ICommandResultOut<T_Result, T_In, T_Out>>
    {
        #region Methods
        public static implicit operator AddActionResultOutCommandModel<T_Result, T_In, T_Out>(ActionResultOutCommand<T_Result, T_In, T_Out> command)
        {
            var createKeyOutcome = Rapid._idContainer.CreateKey(out var key);
            if (!createKeyOutcome)
            {
                throw new Exception(createKeyOutcome.ErrorMessage);
            }
            return new AddActionResultOutCommandModel<T_Result, T_In, T_Out>
            {
                _key = key,
                _command = command,
                _once = false
            };
        }
        #endregion
    }

    public class AddActionResultOutCommandModel<T_Result, T_In_1, T_In_2, T_Out> : AddCommandBaseModel<ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out>>
    {
        #region Methods
        public static implicit operator AddActionResultOutCommandModel<T_Result, T_In_1, T_In_2, T_Out>(ActionResultOutCommand<T_Result, T_In_1, T_In_2, T_Out> command)
        {
            var createKeyOutcome = Rapid._idContainer.CreateKey(out var key);
            if (!createKeyOutcome)
            {
                throw new Exception(createKeyOutcome.ErrorMessage);
            }
            return new AddActionResultOutCommandModel<T_Result, T_In_1, T_In_2, T_Out>
            {
                _key = key,
                _command = command,
                _once = false
            };
        }
        #endregion
    }
}