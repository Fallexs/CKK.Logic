﻿using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;


namespace CKK.Logic.Models
{
    public class Store : Entity, IStore {
        public Store() {
            items = new List<StoreItem>();
        }

        public List<StoreItem> items { get; set; }
        public StoreItem AddStoreItem(Product prod, int quantity) {
            if( quantity <= 0 ) {
                throw new InventoryItemStockTooLowException();

            }

            //checks if stock is empty
            if( items.Count == 0 ) {
                items.Add(new StoreItem(prod, quantity));
                return items [ 0 ];
            }
            //checks if product is already in store
            for( int i = 0; i < items.Count; i++ ) {
                if( items [ i ].Product.Id == prod.Id ) {
                    items [ i ].Quantity = (items [ i ].Quantity + quantity);
                    return items [ i ];
                }
            }
            //adds product if not present in current stock
            items.Add(new StoreItem(prod, quantity));
            return items.Last();


        }
        public StoreItem RemoveStoreItem(int id, int quantity) {
            if( quantity < 0 ) {
                throw new ArgumentOutOfRangeException();
            }

            for( int i = 0; i < items.Count; i++ ) {
                if( items [ i ].Product.Id == id ) {
                    if( items [ i ].Quantity - quantity <= 0 ) {
                        items [ i ].Quantity = (0);
                        return items [ i ];
                    }
                    items [ i ].Quantity = (items [ i ].Quantity - quantity);
                    return items [ i ];
                }
            }
            throw new ProductDoesNotExistException();
        }
        public List<StoreItem> GetStoreItems() {
            return items;
        }
        public StoreItem FindStoreItemById(int id) {
            if( id < 0 ) {
                throw new InvalidIdException();
            }
            List<int> ids = items.Select(x => x.Product.Id).ToList();
            for( int i = 0; i < items.Count; i++ ) {
                if( ids [ i ] == id ) {

                    return items [ i ];
                }
            }
            return null;
        }

    }
}
