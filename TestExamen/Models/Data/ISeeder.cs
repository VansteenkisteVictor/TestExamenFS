using System;
using System.Collections.Generic;

namespace Models.Data
{
    public interface ISeeder
    {
        List<Guid> Lst_RestaurantGuids { get; set; }

        void initDatabase(int nmbrComments);
    }
}