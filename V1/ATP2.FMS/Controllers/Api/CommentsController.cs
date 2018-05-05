using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FMS.Core.Entities;
using FMS.Infrastructure;

namespace ATP2.FMS.Api
{
    public class CommentsController : ApiController
    {
        public FMSDbContext _context { get; set; }

        public CommentsController()
        {
            _context = new FMSDbContext();
        }
        [HttpGet]
        public List<COMMENTSEC> Get()
        {
            return _context.commentsecs.ToList();
        }
        [HttpGet]
        public List<COMMENTSEC> Get(int id)
        {
            var list = _context.commentsecs.Where(e => e.ProjectSectionId == id).ToList();
            foreach (var v in list)
            {
                v.UserInfo = _context.userInfos.FirstOrDefault(d => d.UserId == v.UserId);
            }
            return list;
        }
        [HttpPost]
        public COMMENTSEC Post(COMMENTSEC commentsec)
        {
            try
            {
                var objToSave = new COMMENTSEC();
                _context.commentsecs.Add(objToSave);
                objToSave.UserId = commentsec.UserId;
                objToSave.Commt = commentsec.Commt;
                objToSave.ProjectSectionId = commentsec.ProjectSectionId;
                
                _context.SaveChanges();
                return commentsec;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
