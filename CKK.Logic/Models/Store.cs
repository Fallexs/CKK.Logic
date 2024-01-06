﻿using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;
using System.ComponentModel.Design;

namespace CKK.Logic.Models {
    public class Store : Entity, IStore {
        private List<StoreItem> Items { get; set; } = new();
        public List<StoreItem> GetStoreItems() => Items;

        public StoreItem AddStoreItem(Product prod, int quantity) {
            if (quantity <= 0) {
                throw new InventoryItemStockTooLowException();
            } else {
                var Existing = (
                    from item in Items
                    where prod == item.Product
                    select item );
                if( Existing.Any() ) {
                    foreach(var item in Existing) {
                        item.Quantity += quantity;
                        return item;
                    }
                } else {
                    var newItem = new StoreItem(prod, quantity);
                    Items.Add(newItem);
                    return newItem;
                }
                return Existing.Single();
            }
        }

        public StoreItem RemoveStoreItem(int id, int quantity) {
            var Existing = (
                from item in Items
                where id == item.Product.Id
                select item);
            if ( quantity < 0 ) {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Invalid quantity.");
            } else if ( !Existing.Any() ) {
                throw new ProductDoesNotExistException();
            } else {
                foreach (var item in Existing) {
                    item.Quantity -= quantity;
                    if ( item.Quantity < 0 ) {
                        item.Quantity = 0;
                        return item;
                    } else {
                        return item;
                    }
                }
                return Existing.Single();
            }
        }

        public StoreItem FindStoreItemById(int id) {
            try {
                if( id < 0 ) {
                    throw new InvalidIdException();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            var FindByID =
                from e in Items
                where id == e.Product.Id
                select e;
            return FindByID.First();
        }
        

    }
}
