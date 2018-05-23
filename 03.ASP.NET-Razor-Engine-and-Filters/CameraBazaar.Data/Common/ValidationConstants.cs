namespace CameraBazaar.Data.Common
{
    public static class ValidationConstants
    {
        public const int QuantityMinLength = 0;

        public const int QuantityMaxLength = 100;

        public const int MinShutterSpeedMinLength = 0;

        public const int MinShutterSpeedMaxLength = 100;

        public const int MaxShutterSpeedMinLength = 2000;

        public const int MaxShutterSpeedMaxLength = 8000;

        public const int MaxIsoMinLength = 200;

        public const int MaxIsoMaxLength = 409600;

        public const int VideoResulutionMinLength = 1;

        public const int VideoResulutionMaxLength = 15;

        public const int ImageUrlMaxLength = 2000;

        public const int DescriptionMaxLength = 6000;
    }
}
