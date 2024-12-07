﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Data.Models
{
    public class StoreItem
    {

       
        public int StoreId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        public  Store? Store { get; set; }
        public  Item? Item { get; set; }


    }
}

