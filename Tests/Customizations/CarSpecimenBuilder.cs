using AutoFixture;
using AutoFixture.Kernel;
using System.Reflection;
using CarsApi.Models;
using CarsApi.DTOs;

public class CarSpecimenBuilder : ICustomization
    {
    public void Customize(IFixture fixture)
        {
        fixture.Customizations.Add(new CarPropertySpecimenBuilder());
        }

    private class CarPropertySpecimenBuilder : ISpecimenBuilder
        {
        public object Create(object request, ISpecimenContext context)
            {
            if (request is PropertyInfo pi)
                {
                if ((pi.DeclaringType == typeof(Car) || pi.DeclaringType == typeof(CarDto)) && pi.PropertyType == typeof(string))
                    {
                    switch (pi.Name)
                        {
                        case "Make":
                            return "Default Make";
                        case "Model":
                            return "Default Model";
                        case "Color":
                            return "Default Color";
                        }
                    }
                }
            return new NoSpecimen();
            }
        }
    }
