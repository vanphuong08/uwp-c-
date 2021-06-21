﻿using Food3.Adapters;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food3.Models
{
    class Carts : CartService
    {
        // chứa các funtion service về Cart
        public bool AddToCart(CartItem item)
        {
            SQLiteHelper sQLiteHelper = SQLiteHelper.createInstance_Cart();
            SQLiteConnection sQLiteConnection = sQLiteHelper.sQLiteConnection;
            string sqlCommand = "insert into Cartss(id,name,image,price,qty) values(?,?,?,?,?)";
            var stt = sQLiteConnection.Prepare(sqlCommand);
            stt.Bind(1, item.id);
            stt.Bind(2, item.name);
            stt.Bind(3, item.image);
            stt.Bind(4, item.price);
            stt.Bind(5, item.qty);
            var result = stt.Step();
            return result == SQLiteResult.OK;
        }

        public bool ClearCart()
        {
            SQLiteConnection sQLiteConnection = SQLiteHelper.createInstance_Cart().sQLiteConnection;
            string sqlCommand = "delete from Cartss;";
            var stt = sQLiteConnection.Prepare(sqlCommand);
            var result = stt.Step();
            return result == SQLiteResult.OK;
        }

        public bool DeleteItem(CartItem item)
        {
            SQLiteConnection sQLiteConnection = SQLiteHelper.createInstance_Cart().sQLiteConnection;
            string sqlCommand = "delete from Cartss where id = ?;";
            var stt = sQLiteConnection.Prepare(sqlCommand);
            stt.Bind(1, item.id);
            var result = stt.Step();
            return result == SQLiteResult.OK;
        }
        public bool DeleteAllItem()
        {
            SQLiteConnection sQLiteConnection = SQLiteHelper.createInstance_Cart().sQLiteConnection;
            string sqlCommand = "delete from Cartss";
            var stt = sQLiteConnection.Prepare(sqlCommand);
            
            var result = stt.Step();
            return result == SQLiteResult.OK;
        }

        public List<CartItem> GetCarts()
        {
            SQLiteConnection sQLiteConnection = SQLiteHelper.createInstance_Cart().sQLiteConnection;
            string sqlCommand = "select * from Cartss;";
            var stt = sQLiteConnection.Prepare(sqlCommand);
            List<CartItem> list = new List<CartItem>();
            while (SQLiteResult.ROW == stt.Step())
            {
                int id = Convert.ToInt32(stt[0]);
                string name = (string)stt[1];
                string image = (string)stt[2];
                int price = Convert.ToInt32(stt[3]);
                int qty = Convert.ToInt32(stt[4]);
                list.Add(new CartItem(id, name, image, price, qty));
            }
            return list;
        }

        public bool UpdateQty(CartItem item, int newQty)
        {
            if (newQty > 0)
            {
                SQLiteConnection sQLiteConnection = SQLiteHelper.createInstance_Cart().sQLiteConnection;
                string sqlCommand = "update Cartss Set qty = ? where id = ?;";
                var stt = sQLiteConnection.Prepare(sqlCommand);
                stt.Bind(1, newQty);
                stt.Bind(2, item.id);
                var result = stt.Step();
                return result == SQLiteResult.OK;
            }
            else
            {
                return DeleteItem(item);
            }
        }

        
    }
}
