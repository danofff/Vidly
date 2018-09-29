﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context=new ApplicationDbContext();
        }
        //Get /api/customers
        public IHttpActionResult GetCustomers()
        {
            var customersDtOs=_context.Customers.Include(c=>c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer,CustomerDto>);
            return Ok(customersDtOs);
        }

        //Get /api/customers/id
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c=>c.Id==id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        //Post /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();                
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri+"/"+customer.Id), customerDto );
        }

        //Put api/customers/url
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb==null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();
        }

        //Delete /api/customers/l
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

        }

    }
}
