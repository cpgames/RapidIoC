using System;
using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    public class AddActionResultCommandModel<T_Result> : AddCommandBaseModel<ICommandResult<T_Result>>
    {
        #region Methods
        public static implicit operator AddActionResultCommandModel<T_Result>(ActionResultCommand<T_Result> command)
        {
            var createKeyOutcome = Rapid.KeyFactoryCollection.Create(SignalBase.UidGenerator, out var key);
            if (!createKeyOutcome)
            {
                throw new Exception(createKeyOutcome.ErrorMessage);
            }
            return new AddActionResultCommandModel<T_Result>
            {
                _key = key,
                _command = command,
                _once = false
            };
        }
        #endregion
    }

    public class AddActionResultCommandModel<T_Result, T_In> : AddCommandBaseModel<ICommandResult<T_Result, T_In>>
    {
        #region Methods
        public static implicit operator AddActionResultCommandModel<T_Result, T_In>(ActionResultCommand<T_Result, T_In> command)
        {
            var createKeyOutcome = Rapid.KeyFactoryCollection.Create(SignalBase.UidGenerator, out var key);
            if (!createKeyOutcome)
            {
                throw new Exception(createKeyOutcome.ErrorMessage);
            }
            return new AddActionResultCommandModel<T_Result, T_In>
            {
                _key = key,
                _command = command,
                _once = false
            };
        }

        public static implicit operator AddActionResultCommandModel<T_Result, T_In>(ActionResultDelegate<T_Result, T_In> action)
        {
            return new ActionResultCommand<T_Result, T_In>(action);
        }
        #endregion
    }

    public class AddActionResultCommandModel<T_Result, T_In_1, T_In_2> : AddCommandBaseModel<ICommandResult<T_Result, T_In_1, T_In_2>>
    {
        #region Methods
        public static implicit operator AddActionResultCommandModel<T_Result, T_In_1, T_In_2>(ActionResultCommand<T_Result, T_In_1, T_In_2> command)
        {
            var createKeyOutcome = Rapid.KeyFactoryCollection.Create(SignalBase.UidGenerator, out var key);
            if (!createKeyOutcome)
            {
                throw new Exception(createKeyOutcome.ErrorMessage);
            }
            return new AddActionResultCommandModel<T_Result, T_In_1, T_In_2>
            {
                _key = key,
                _command = command,
                _once = false
            };
        }
        #endregion
    }
}