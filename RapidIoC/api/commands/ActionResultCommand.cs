namespace cpGames.core.RapidIoC
{
    public delegate T_Result ActionResultDelegate<out T_Result>();
    public delegate T_Result ActionResultDelegate<out T_Result, in T_In>(T_In @in);
    public delegate T_Result ActionResultDelegate<out T_Result, in T_In_1, in T_In_2>(T_In_1 in1, T_In_2 in2);
}