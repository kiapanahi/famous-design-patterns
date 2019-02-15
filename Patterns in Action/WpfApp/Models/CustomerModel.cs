using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;
using System.Configuration;
using System.Collections.ObjectModel;

namespace WpfApp.Models
{
    
    // model of the member
    
    public class MemberModel : BaseModel
    {
        IProvider provider;

        // private data members

        int memberId = 0;
        string email;
        string companyName;
        string city;
        string country;
        
        ObservableCollection<OrderModel> _orders;

        public MemberModel(IProvider provider)
        {
            this.provider = provider;
        }

        public int Add()
        {
            provider.AddMember(this); ;
            return 1; 
        }
       
        public int Delete()
        {
            var orders = provider.GetOrders(this.MemberId);
            if (orders == null || orders.Count == 0)
            {
                provider.DeleteMember(this.MemberId);
                return 1;
            }
            else
                return 0;  // nothing deleted because member has orders.
                
        }

        public int Update()
        {
            provider.UpdateMember(this);
            return 1; 
        }

        public int MemberId
        {
            get { ConfirmOnUIThread(); return memberId; }
            set { ConfirmOnUIThread(); if (memberId != value) { memberId = value; Notify("MemberId"); } }
        }

        public string Email
        {
            get { ConfirmOnUIThread(); return email; }
            set { ConfirmOnUIThread(); if (email != value) { email = value; Notify("Email"); } }
        }

        public string CompanyName
        {
            get { ConfirmOnUIThread(); return companyName; }
            set { ConfirmOnUIThread(); if (companyName != value) { companyName = value; Notify("CompanyName"); } }
        }

        public string City
        {
            get { ConfirmOnUIThread(); return city; }
            set { ConfirmOnUIThread(); if (city != value) { city = value; Notify("City"); } }
        }

        public string Country
        {
            get { ConfirmOnUIThread(); return country; }
            set { ConfirmOnUIThread(); if (country != value) { country = value; Notify("Country"); } }
        }

        public ObservableCollection<OrderModel> Orders
        {
            get { ConfirmOnUIThread(); LazyloadOrders(); return _orders; }
            set { ConfirmOnUIThread(); _orders = value; Notify("Orders"); }
        }

        // helper that performs lazy loading of orders

        void LazyloadOrders()
        {
            if (_orders == null) 
            {
                Orders = provider.GetOrders(this.MemberId) ?? new ObservableCollection<OrderModel>();
            }
        }
    }
}
