using FoodDeliveryApplication.Server.Data;
using FoodDeliveryApplication.Server.IRepository;
using FoodDeliveryApplication.Server.Models;
using FoodDeliveryApplication.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodDeliveryApplication.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Branch> _branches;
        private IGenericRepository<Customer> _customers;
        private IGenericRepository<Delivery> _deliveries;
        private IGenericRepository<MenuItem> _menuItems;
        private IGenericRepository<Order> _orders;
        private IGenericRepository<OrderItem> _orderItems;
        private IGenericRepository<Payment> _payments;
        private IGenericRepository<Staff> _staffs;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Branch> Branches
            => _branches ??= new GenericRepository<Branch>(_context);

        public IGenericRepository<Customer> Customers
            => _customers ??= new GenericRepository<Customer>(_context);

        public IGenericRepository<Delivery> Deliveries
            => _deliveries ??= new GenericRepository<Delivery>(_context);

        public IGenericRepository<MenuItem> MenuItems
            => _menuItems ??= new GenericRepository<MenuItem>(_context);

        public IGenericRepository<Order> Orders
            => _orders ??= new GenericRepository<Order>(_context);

        public IGenericRepository<OrderItem> OrderItems
            => _orderItems ??= new GenericRepository<OrderItem>(_context);

        public IGenericRepository<Payment> Payments
            => _payments ??= new GenericRepository<Payment>(_context);

        public IGenericRepository<Staff> Staffs
            => _staffs ??= new GenericRepository<Staff>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            await _context.SaveChangesAsync();
        }
    }
}
