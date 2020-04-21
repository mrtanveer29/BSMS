using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPApi.Models;
using ERPApi.Models.IRepository;
using ERPApi.Models.StronglyType;

namespace ERPApi.Models.Repository
{
    public class RoleWisePageMappingRepository : IRoleWisePageMappingRepository
    {
        private ERPEntities _entities;

        public RoleWisePageMappingRepository()
        {
            this._entities = new ERPEntities();
        }

        public object GetAllPages()
        {
            var pagelist = _entities.sts_pagelist.ToList();
            return pagelist;
        }
        public object GetAllButtons()
        {
            var buttonlist = _entities.sts_action_process.ToList();
            return buttonlist;
        }

        public object GetAllbuttonsByPageid(int page_id)
        {
            var buttons = (from pwb in _entities.sts_pagewise_button
                           join ap in _entities.sts_action_process
                               on pwb.process_id equals ap.process_id
                           join p in _entities.sts_pagelist
                                on pwb.page_id equals p.page_id
                           where pwb.page_id == page_id

                           select new

                           {
                               page_id = pwb.page_id,
                               page_name = p.page_name,
                               process_id = ap.process_id,
                               process = ap.process

                           }).ToList();

            return buttons;
        }

        public List<sts_role_pagewise_button> GetAllPagesByRole(int? role_id)
        {
            var pagelist = _entities.sts_role_pagewise_button.Where(a=>a.role_id == role_id).ToList();
            return pagelist;
        }

        //public object GetAllRoleWisePageMapping()                 /////////  To Do 21 jan 2016
        //{
        //    //List<sts_pagewise_button> sts_pagewise = _entities.sts_pagewise_button.GroupBy(a => a.page_name).Select(g => new { g.Key });      //ToList();
        //    var sts_pagewise = new object(); // _entities.sts_pagewise_button.GroupBy(a => a.page_name).Select(g => new { g.Key, Count = g.Count() });
        //    return sts_pagewise;
        //}

        public ERPApi.Models.sts_role_pagewise_button GetroleWiseByID(int page_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertRoleWisePage(RoleWisePageMappingModel oRoleWisePageMappingModel)
        {
            try
            {
                _entities.Database.ExecuteSqlCommand(" delete from sts_role_pagewise_button where role_id='" + oRoleWisePageMappingModel.role_id + "' ");

                foreach (var page in oRoleWisePageMappingModel.pages)
                {
                    sts_role_pagewise_button insertRolewisePageMap = new sts_role_pagewise_button
                    {
                        role_id = oRoleWisePageMappingModel.role_id,
                        page_id = int.Parse(page),
                        status = "true"
                    };
                    _entities.sts_role_pagewise_button.Add(insertRolewisePageMap);
                }

                foreach (var buton in oRoleWisePageMappingModel.pagebutton)
                {
                    string[] explodedata = buton.Split('_');
                    sts_role_pagewise_button insertRolewisePageMap = new sts_role_pagewise_button
                    {
                        role_id = oRoleWisePageMappingModel.role_id,
                        page_id = int.Parse(explodedata[0]),
                        status = "true",
                        process_id = int.Parse(explodedata[1])
                    };

                    _entities.sts_role_pagewise_button.Add(insertRolewisePageMap);
                }

                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //private bool checkpagexist(int? role_id, int page)
        //{
        //    try
        //    {
        //        sts_role_pagewise_button sts = _entities.sts_role_pagewise_button.FirstOrDefault(n => n.role_id == role_id && n.page_id == page);
        //        if (sts == "")
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //public bool InsertRoleNPageWiseButton(LMSApi.Models.sts_role_pagewise_button oRoleWisePage)
        //{
        //    try
        //    {
        //        sts_role_pagewise_button sts = _entities.sts_role_pagewise_button.Where(a => a.page_id == oRoleWisePage.page_id && a.role_id == oRoleWisePage.role_id).FirstOrDefault();
        //        //if (sts != null)
        //        //{
        //            sts.process_id = oRoleWisePage.process_id;
        //            _entities.SaveChanges();
        //        //}
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public bool DeleteRoleWisePage(int page_id)
        {
            try
            {
                sts_pagewise_button oSts = _entities.sts_pagewise_button.FirstOrDefault(s => s.page_id == page_id);
                _entities.sts_pagewise_button.Attach(oSts);
                _entities.sts_pagewise_button.Remove(oSts);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateRoleNPagewiseButton(ERPApi.Models.sts_role_pagewise_button oRoleWisePage)
        {
            try
            {
                sts_role_pagewise_button sts = _entities.sts_role_pagewise_button.Find(oRoleWisePage.page_id);
                sts.page_id = oRoleWisePage.page_id;
                sts.process_id = oRoleWisePage.process_id;
                sts.status = oRoleWisePage.status;

                _entities.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}