﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace JournalResearcher.DataAccess.Cores
{
    public class OrderExpression
    {
        public int Direction { get; set; }
        public int Column { get; set; }

        public static OrderExpression Deserializer(string whereCondition)
        {
            OrderExpression orderDeserializer = null;
            if (string.IsNullOrEmpty(whereCondition))
            {
                orderDeserializer = JsonConvert.DeserializeObject<OrderExpression>(whereCondition);
            }

            return orderDeserializer ?? new OrderExpression();
        }
    }
}
