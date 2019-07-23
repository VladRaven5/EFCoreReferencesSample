using System;
using EFCoreReferencesSample.DAL;

namespace EFCoreReferencesSample.Models
{
    public class Car : CarEntity
    {
        private const int LicensePlateLength = 8;

        public Car(string licensePlate, int carAccidents)
        {
            UpdateLicensePlate(licensePlate);
            UpdateCarAccidentsCount(carAccidents);
        }

        //EF requires
        private Car() { }

        public void UpdateLicensePlate(string licensePlate)
        {
            if (licensePlate.Length != LicensePlateLength)
            {
                throw new Exception();
            }

            LicensePlate = licensePlate;
        }

        public void UpdateCarAccidentsCount(int carAccidents)
        {
            //assume error impossible
            if (carAccidents < CarAccidents)
            {
                throw new Exception();
            }

            CarAccidents = carAccidents;
        }
    }
}
