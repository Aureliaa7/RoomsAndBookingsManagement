using System.Runtime.Serialization;

namespace HRAAB_Management.Business.Enums
{
    public enum RoomTypeCode
    {
        [EnumMember(Value = "SGL")]
        Single,

        [EnumMember(Value = "DBL")]
        Double
    }
}
