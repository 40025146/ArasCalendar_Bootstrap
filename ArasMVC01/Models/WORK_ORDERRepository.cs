using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Aras.IOM;

namespace ArasMVC01.Models
{   
	public  class WORK_ORDERRepository : EFRepository<WORK_ORDER>, IWORK_ORDERRepository
	{
        public override IQueryable<WORK_ORDER> All()
        {
            return base.All();
        }
        public IQueryable<WORK_ORDER> FindAll()
        {
            var q =All();
            return q;
        }
        public Item FindAllByItem(Innovator inn)
        {
            Item itmSearch = inn.newItem("Work Order", "get");
            itmSearch = itmSearch.apply();
            return itmSearch;
        }
        public Item FindItem(Item itm)
        {
            itm.setAction("get");
            itm = itm.apply();
            return itm;
        }
        public Item FindItemById(Innovator inn,string itemtype,string id)
        {
            Item itm = inn.getItemById(itemtype, id);
            itm = itm.apply();
            return itm;
        }
        public Item AddItem(Item itm)
        {
//            itm.setAction("add");
            itm = itm.apply();
            return itm;
        }
        public Item EditItem(Item itm)
        {
            itm.setAction("edit");
            itm = itm.apply();
            return itm;
        }
        public Item DeleteItem(Item itm)
        {
            itm.setAction("delete");
            itm = itm.apply();
            return itm;
        }
    }

	public  interface IWORK_ORDERRepository : IRepository<WORK_ORDER>
	{

	}
}