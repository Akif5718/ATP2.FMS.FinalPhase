using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMS.Core.Entities;
using FMS.Core.Service.Interfaces;

namespace ATP2.FMS.ViewModel
{
    public class RequestedMemberModel
    {
        //private IPostAProjectService _postservice;
        //private IUserInfoService _userservice;
        //private IResponseToAJobService _responseservice;

        //public RequestedMemberModel(IPostAProjectService postservice, IUserInfoService userservice, IResponseToAJobService responseservice)
        //{
        //    _postservice = postservice;
        //    _userservice = userservice;
        //    _responseservice = responseservice;
        //}

        public List<UserInfo> UserInfo = new List<UserInfo>();

        public int PostId { get; set; }

        public string ProjectName { get; set; }

        public string Description { get; set; }

        public List<ResponseToaJob> ResponseToaJob=new List<ResponseToaJob>();
        //public RequestedMemberModel getall(int id)
        //{
        //    RequestedMemberModel requested = new RequestedMemberModel();
        //    var result = _responseservice.GetAll(id);

        //    var result2 = _postservice.GetByID(id);
        //    requested.ProjectName = result2.Data.ProjectName;
        //    requested.Description = result2.Data.Description;
        //    requested.PostId = result2.Data.PostId;
        //    foreach (var user in result)
        //    {
        //        var result3 = _userservice.GetByID(user.WUserId);
        //        requested.UserInfo.Add(result3.Data);

        //    }
        //    return requested;
        //}
    }
}