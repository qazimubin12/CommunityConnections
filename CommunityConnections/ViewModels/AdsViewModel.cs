﻿using CommunityConnections.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommunityConnections.ViewModels
{
    public class AdsListingViewModel
    {
        public List<Ads> Ads { get; set; }
        public List<Customer> Customers { get; set; }
        //public List<MyListViewModel> MyListViewModel { get; set; }
        public List<Ads> AdsOnPageOne { get; set; }
        public List<Ads> AdsOnPageTwo { get; set; }
        public string SearchTerm { get; set; }
        public List<string> Layouts { get; set; }
        public List<string> StatusList { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime AdDate { get; set; }
        public int PageNo { get; set; }
        public int PageTwo { get; set; }
        public string AdSize { get; set; }
        public string Path { get; set; }
        public string AdStatus { get; set; }
        public string Sort { get; set; }
        public string Book { get; set; }
        public string Repeat { get; set; }
        public string Customer { get; set; }
        public string AddGraphics { get; set; }
        public string CustomSpecification { get; set; }
        public float Discount { get; set; }
        public float Total { get; set; }
        public string Status { get; set; }
        public bool Deluxe { get; set; }
        public string AdDescription { get; set; }

        public int PageOne { get; set; }
        public int Page_Two { get; set; }
    }
    public class AdsActionViewModel
    {
       
        public List<Customer> Customers { get; set; }
        public List<string> StatusList { get; set; }
        public List<string> Layouts { get; set; }
        public int ID { get; set; }
        public string Status { get; set; }

        public DateTime AdDate { get; set; }
        public int PageNo { get; set; }
        public string Name { get; set; }
        public string Layout { get; set; }
        public string AdSize { get; set; }
        public string Path { get; set; }
        public string AdStatus { get; set; }
        public int PageTwo { get; set; }
        public string Book { get; set; }
        public string Repeat { get; set; }
        public string Customer { get; set; }
        public int ChoosePage { get; set; }
        public string AddGraphics { get; set; }
        public string CustomSpecification { get; set; }
        public float Discount { get; set; }
        public float Total { get; set; }
        public bool Delux { get; set; }
    }


   public class AdViewModelActionView
    {
        public AdsListingViewModel ViewModel { get; set; }
        public string Path { get; set; }
    }



}