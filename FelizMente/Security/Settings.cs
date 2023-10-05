namespace FelizMente.Security
{
    public class Settings
    {
        private static string secret = " 807c5552ba66ae775040b9d87167a40ac70d08f521e181fca8f6f1b445b6d15f";

        public static string Secret { get => secret; set => secret = value; }
    }
}
