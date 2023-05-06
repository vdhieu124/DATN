using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DuLichV2.Models
{
    public class BookingCart
    {
        public List<BookingCartItem> items { get; set; }
        public BookingCart()
        {
            this.items = new List<BookingCartItem>();
        }
        public void AddToCart(BookingCartItem item, int Quantity)
        {
            var checkExists = items.FirstOrDefault(x => x.TourId == item.TourId);
            if (checkExists != null)
            {
                checkExists.Quantity += Quantity;
                checkExists.TotalPrice = checkExists.Price * checkExists.Quantity;
            } else
            {
                items.Add(item);
            }
        }

        public void Remove(int id)
        {
            var checkExists = items.SingleOrDefault(x => x.TourId == id);
            if (checkExists != null)
            {
                items.Remove(checkExists);
            }
        }
        public void UpdateQuantity(int id, int quantity)
        {
            var checkExists = items.SingleOrDefault(x => x.TourId == id);
            if (checkExists != null)
            {
                checkExists.Quantity = quantity;
                checkExists.TotalPrice = checkExists.Price * checkExists.Quantity;
            }
        }
        public decimal GetTotal()
        {
            return items.Sum(x => x.TotalPrice);
        }
        public int GetTotalQuantity()
        {
            return items.Sum(x => x.Quantity);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
    public class BookingCartItem
    {
        public int TourId { get; set; }
        public string TourName { get; set; }
        public string Alias { get; set; }
        public string CategoryName { get; set; }
        public string TourImg { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}