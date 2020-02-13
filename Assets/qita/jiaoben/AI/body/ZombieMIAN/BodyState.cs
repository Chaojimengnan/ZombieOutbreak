public class BodyState
{
    /// <summary>
    /// 普通物体状态
    /// </summary>
    public enum noromlstate
    {
        RandomMove,                //漫游状态
        GetPoint1,                 //获得目标点1
        GetPoint2,                 //获得目标点2
        GetTarget                  //获得物体目标
    }

    //public zst state = zst.Walkalong;
    public noromlstate my_state = noromlstate.RandomMove;


    public BodyState()
    {
        PutInstate(noromlstate.RandomMove);
    }

    /// <summary>
    /// 切换状态，并且把定值赋值给Dis和Angel
    /// </summary>
    public void PutInstate(noromlstate newstate)
    {
        my_state = newstate;
    }


}
