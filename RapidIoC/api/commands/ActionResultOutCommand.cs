namespace cpGames.core.RapidIoC
{
    public delegate T_Result ActionResultOutDelegate<out T_Result, T_Out>(out T_Out @out);
    public delegate T_Result ActionResultOutDelegate<out T_Result, in T_In, T_Out>(T_In @in, out T_Out @out);
    public delegate T_Result ActionResultOutDelegate<out T_Result, in T_In_1, in T_In_2, T_Out>(T_In_1 in1, T_In_2 in2, out T_Out @out);
}