using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMS.Core.Entities;

namespace ATP2.FMS.ViewModel
{
    public class Profile
    {
        public int UserId { get; set; }

        public string FristName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string ConPassword { get; set; }

        public DateTime DateofBrith { get; set; }

        public DateTime JoinDate { get; set; }

        public string ProPic { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public string UserType { get; set; }

        public double Balance { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyCode { get; set; }

        public string Position { get; set; }
        public List<PostAProject> PostAProject = new List<PostAProject>();

        public List<int> CommunicationSkill = new List<int>();
        private int communicationSkill;

        public int _communictionSkill
        {
            get
            {
                var com = 0;
                foreach (int v in CommunicationSkill)
                {
                    com += v;
                }
                if (CommunicationSkill.Count!=0)
                return com / CommunicationSkill.Count;
                else
                {
                    return 0;
                }
            }
            set { communicationSkill = value; }
        }

        public List<int> Reliability = new List<int>();
        private int reliability;

        public int _reliability
        {
            get
            {
                var c = 0;
                foreach (int v in Reliability)
                {
                    c += v;
                }

                if (Reliability.Count != 0)
                    return c / Reliability.Count;
                else
                {
                    return 0;
                }
            }
            set { reliability = value; }
        }
        public List<int> OnWord = new List<int>();
        private int onword;

        public int _onword
        {
            get
            {
                var c = 0;
                foreach (int v in OnWord)
                {
                    c += v;
                }

                if (OnWord.Count != 0)
                    return c / OnWord.Count;
                else
                {
                    return 0;
                }
            }
            set { onword = value; }
        }
        public List<int> Behaviour = new List<int>();
        private int behaviour;

        public int _behaviour
        {
            get
            {
                var c = 0;
                foreach (int v in Behaviour)
                {
                    c += v;
                }

                if (Behaviour.Count != 0)
                    return c / Behaviour.Count;
                else
                {
                    return 0;
                }
            }
            set { behaviour = value; }
        }
        private double totalRating;

        public double TotalRating
        {
            get { return Math.Round((((((_communictionSkill + _reliability + _onword + _behaviour) + 0.0) / 4)*5)/100),2); }
        }

        public int CcommunicationSkill { get; set; }
        public int Oonword { get; set; }
        public int Rreliability { get; set; }
        public int Bbehaviour { get; set; }
        public double tot { get; set; }
        public Profile creation(UserInfo userInfo, OwnerInfo ownerInfo, List<RatingOwner> ratingOwner, List<PostAProject> postAProject)
        {
            var profile = new Profile();
            profile.UserId = userInfo.UserId;
            profile.FristName = userInfo.FristName;
            profile.LastName = userInfo.LastName;
            profile.Email = userInfo.Email;
            profile.DateofBrith = userInfo.DateofBrith;
            profile.JoinDate = userInfo.JoinDate;
            profile.ProPic = userInfo.ProPic;
            profile.City = userInfo.City;
            profile.State = userInfo.State;
            profile.Country = userInfo.Country;
            profile.UserType = userInfo.UserType;
            profile.Balance = userInfo.Balance;
            profile.CompanyName = ownerInfo.CompanyName;
            profile.CompanyAddress = ownerInfo.CompanyAddress;
            profile.CompanyCode = ownerInfo.CompanyCode;
            profile.Position = ownerInfo.Position;
            profile.PostAProject = postAProject;
            foreach (var v in ratingOwner)
            {
                CommunicationSkill.Add(v.CommunicationSkill);
                Reliability.Add(v.Reliability);
                OnWord.Add(v.OnWord);
                Behaviour.Add(v.Behaviour);
            }

            profile.CcommunicationSkill = _communictionSkill;
            profile.Oonword = _onword;
            profile.Rreliability = _reliability;
            profile.Bbehaviour = _behaviour;
            profile.tot = TotalRating;
            return profile;
        }

        public UserInfo EditUserInfo(Profile profile, UserInfo userInfo)
        {
            userInfo.FristName = profile.FristName;
            userInfo.LastName = profile.LastName;
            userInfo.City = profile.City;
            userInfo.State = profile.State;
            userInfo.Country = Country;
            if (profile.ConPassword != null)
            {
                userInfo.Password = profile.Password;
            }

            return userInfo;
        }
        public OwnerInfo EditOwnerInfo(Profile profile, OwnerInfo ownerInfo)
        {
            ownerInfo.CompanyName = profile.CompanyName;
            ownerInfo.CompanyAddress = profile.CompanyAddress;
            ownerInfo.CompanyCode = profile.CompanyCode;
            ownerInfo.Position = profile.Position;
            

            return ownerInfo;
        }
    }
}