using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMS.Core.Entities;

namespace ATP2.FMS.ViewModel
{
    public class ProfileWorker
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
        public List<PostAProject> PostAProject = new List<PostAProject>();
        public string ratePerHour { get; set; }

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
                if (CommunicationSkill.Count != 0)
                    return com / CommunicationSkill.Count;
                else
                {
                    return 0;
                }
            }
            set { communicationSkill = value; }
        }

        public List<int> OnTime = new List<int>();
        private int onTime;

        public int _onTime
        {
            get
            {
                var c = 0;
                foreach (int v in OnTime)
                {
                    c += v;
                }

                if (OnTime.Count != 0)
                    return c / OnTime.Count;
                else
                {
                    return 0;
                }
            }
            set { onTime = value; }
        }
        public List<int> OnBudget = new List<int>();
        private int onBudget;

        public int _onBudget
        {
            get
            {
                var c = 0;
                foreach (int v in OnBudget)
                {
                    c += v;
                }

                if (OnBudget.Count != 0)
                    return c / OnBudget.Count;
                else
                {
                    return 0;
                }
            }
            set { onBudget = value; }
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
        public List<int> Completeness = new List<int>();
        private int completeness;

        public int _completeness
        {
            get
            {
                var c = 0;
                foreach (int v in Completeness)
                {
                    c += v;
                }

                if (Completeness.Count != 0)
                    return c / Completeness.Count;
                else
                {
                    return 0;
                }
            }
            set { completeness = value; }
        }
        private double totalRating;

        public double TotalRating
        {
            get { return Math.Round((((((_communictionSkill + _onTime + _onBudget + _behaviour) + 0.0) / 5) * 5) / 100), 2); }
        }

        public int CcommunicationSkill { get; set; }
        public int OonBudget { get; set; }
        public int OonTime { get; set; }
        public int Bbehaviour { get; set; }
        public int Ccompleteness { get; set; }
        public double tot { get; set; }
        public ProfileWorker creation(UserInfo userInfo, WorkerInfo workerInfoData, List<RatingWorker> ratings, List<PostAProject> projects)
        {
            var profile = new ProfileWorker();
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
            profile.ratePerHour = workerInfoData.RatePerHour;
            profile.PostAProject = projects;
            foreach (var v in ratings)
            {
                CommunicationSkill.Add(v.CommunicationSkill);
                OnBudget.Add(v.OnBudget);
                OnTime.Add(v.OnTime);
                Behaviour.Add(v.Behaviour);
                Completeness.Add(v.Completeness);
            }

            profile.CcommunicationSkill = _communictionSkill;
            profile.OonTime = _onTime;
            profile.OonBudget = _onBudget;
            profile.Bbehaviour = _behaviour;
            profile.Ccompleteness = _completeness;
            profile.tot = TotalRating;
            return profile;
        }
        public UserInfo EditUserInfo(ProfileWorker profile, UserInfo userInfo)
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
        public WorkerInfo EditWorkerInfo(ProfileWorker profile, WorkerInfo workerInfo)
        {
            workerInfo.RatePerHour = profile.ratePerHour;


            return workerInfo;
        }
    }
}