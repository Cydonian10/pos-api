using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using PuntoVenta.Database.Entidades;
using PuntoVenta.Modules.Brand.Dtos;
using PuntoVenta.Modules.Customers.Dtos;

namespace PuntoVenta.Database.Mappers
{
    public static class CustomerMapperExtension
    {
        public static CustomerDto ToDto(this Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                DNI = customer.DNI,
                Phone = customer.Phone,
                Points = customer.Points    
            };
        }

        public static Customer ToEntity(this CreateCustomerDto createCustomerDto)
        {
            return new Customer
            {
                Address = createCustomerDto.Address,
                DNI = createCustomerDto?.DNI,
                Name = createCustomerDto?.Name,
                Phone = createCustomerDto?.Phone,
            };
        }

        public static Customer ToEntityUpdate(this Customer customer, CreateCustomerDto createCustomerDto) {

            customer.Name = createCustomerDto.Name;
            customer.Address = createCustomerDto.Address;
            customer.DNI = createCustomerDto.DNI;
            customer.Phone = createCustomerDto.Phone;

            return customer;
        }
    }
}
