using MegaBuy.PurchaseHistories.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MegaBuy.PurchaseHistories
{
    public class PurchaseGenerator
    {
        private bool _createReturnable;

        private bool MightBeBoughtAtMegaBuy = true;
        private bool MightBeBoughtElsewhere = true;
        private int MaximumDaysSinceDelivery = 90;
        private int MinimumDaysSinceDelivery = 0;
        private bool MightBeSoldAsIs = true;
        private bool MightBeNotSoldAsIs = true;
        private bool MightBeDelivered = true;
        private bool MightBeNotDelivered = true;
        private decimal MaximumPrice = 0;
        private decimal MinimumPrice = 0;
        private bool MightHaveShippingAddress = true;
        private bool MightNotHaveShippingAddress = true;
        private List<ProductCategory> PossibleCategories = Enum.GetValues(typeof(ProductCategory)).Cast<ProductCategory>().ToList();

        public PurchaseGenerator(bool createReturnable)
        {
            _createReturnable = createReturnable;
        }

        public void MustNotBeBoughtAtMegaBuy()
        {
            if (_createReturnable)
                MightBeBoughtAtMegaBuy = false;
            else
                MightBeBoughtElsewhere = false;
        }

        public void MustNotBeBoughtElsewhere()
        {
            if (!_createReturnable)
                MightBeBoughtAtMegaBuy = false;
            else
                MightBeBoughtElsewhere = false;
        }

        public void MustNotBeSoldAsIs()
        {
            if (_createReturnable)
                MightBeSoldAsIs = false;
            else
                MightBeNotSoldAsIs = false;
        }

        public void MustBeSoldAsIs()
        {
            if (!_createReturnable)
                MightBeBoughtAtMegaBuy = false;
            else
                MightBeBoughtElsewhere = false;
        }

        public void MustNotBeDelivered()
        {
            if (_createReturnable)
                MightBeDelivered = false;
            else
                MightBeNotDelivered = false;
        }

        public void MustBeDelivered()
        {
            if (!_createReturnable)
                MightBeBoughtAtMegaBuy = false;
            else
                MightBeBoughtElsewhere = false;
        }

        public void MustNotHaveShippingAddress()
        {
            if (_createReturnable)
                MightHaveShippingAddress = false;
            else
                MightNotHaveShippingAddress = false;
        }

        public void MustHaveShippingAddress()
        {
            if (!_createReturnable)
                MightBeBoughtAtMegaBuy = false;
            else
                MightBeBoughtElsewhere = false;
        }
    }
}
