using System.Runtime.Serialization;

namespace SchoolExperienceBaseTypes
{
    [DataContract]
    public class Distance
    {
        private const double MilesToMetres = 1609.3435021075907;
        private const double MetresToMiles = 0.000621371;

        [DataMember]
        public double Metres { get; set; }

        [IgnoreDataMember]
        public double Miles
        {
            get => Metres * MetresToMiles;
            set => Metres = value * MilesToMetres;
        }

        [IgnoreDataMember]
        public double Kilometres
        {
            get => Metres / 1000.0;
            set => Metres = value * 1000.0;
        }

        public override string ToString() => $"{Kilometres:F3} km, {Miles:F1} M";
    }
}
