using CM.Common.Interface;

namespace CM.Common.Data
{
    public static class DataBaseFactory
    {
        public static IDataBase GetDataBase(DataBaseType dataBaseType)
        {
            if (dataBaseType == DataBaseType.main)
                return new MySQL();
            else
                return null;
        }
    }

    public enum DataBaseType
    {
        main
    }
}
