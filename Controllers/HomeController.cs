using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingsForYou.Models;

namespace WeddingsForYou.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Index(string City, string Category, string placeName, string Address, string Phone, string Email)
        {

              int beverage = 1;
              int cakes = 2;
              int cateres = 3;
              int churches = 4;
              int ceremonyVenues = 5;
              int bridalCars = 6;
              int bridalShoes = 7;
              int florists = 8;
              int danceLessons = 9;
              int mensWear = 10;
              int receptionVenues = 11;
              int photographers = 12;
              int liveMusic = 13;
              int accomodation = 14;

              int IDinDatabase = 0;
              string isRowEmpty = "";

              bool dataisNull = false;
              bool updateOrInsert = false;


              if (ModelState.IsValid)
                {

                  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeddingsForYouDB"].ConnectionString);

                  con.Open();

                  SqlCommand cmd = null;
                  SqlCommand readDirectoryCMD = null;


                  //Read From the Database
                  readDirectoryCMD = new SqlCommand("SELECT * FROM "+City+"", con);

                      readDirectoryCMD.ExecuteNonQuery();

                      SqlDataReader readDirectoryInfo = readDirectoryCMD.ExecuteReader();

                      while (readDirectoryInfo.Read())
                      {
                          if (Category == "Beverages")
                          {
                              try
                              {
                                  isRowEmpty = (string)readDirectoryInfo[beverage];
                                  if (isRowEmpty == "")
                                  {
                                      //If it is Null
                                      dataisNull = true;
                                  }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "Cakes")
                          {
                              try 
                              { 
                                isRowEmpty = (string)readDirectoryInfo[cakes];
                                if(isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "Cateres")
                          {
                              try
                              { 
                                isRowEmpty = (string)readDirectoryInfo[cateres];
                                if (isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "Churches")
                          {
                              try
                              { 
                                isRowEmpty = (string)readDirectoryInfo[churches];
                                if (isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "CeremonyVenues")
                          {
                              try
                              { 
                                isRowEmpty = (string)readDirectoryInfo[ceremonyVenues];
                                if (isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "BridalCars")
                          {
                              try
                              { 
                                isRowEmpty = (string)readDirectoryInfo[bridalCars];
                                if (isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "BridalShoes")
                          {
                              try
                              { 
                                isRowEmpty = (string)readDirectoryInfo[bridalShoes];
                                if (isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "Florists")
                          {
                              try
                              { 
                                isRowEmpty = (string)readDirectoryInfo[florists];
                                if (isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "DanceLessons")
                          {
                              try
                              { 
                                isRowEmpty = (string)readDirectoryInfo[danceLessons];
                                if (isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "MensWear")
                          {
                              try
                              { 
                                isRowEmpty = (string)readDirectoryInfo[mensWear];
                                if (isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "ReceptionVenues")
                          {
                              try
                              { 
                                isRowEmpty = (string)readDirectoryInfo[receptionVenues];
                                if (isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "Photographers")
                          {
                              try
                              { 
                                isRowEmpty = (string)readDirectoryInfo[photographers];
                                if (isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "LiveMusic")
                          {
                              try
                              { 
                                isRowEmpty = (string)readDirectoryInfo[liveMusic];
                                if (isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (Category == "Accomodation")
                          {
                              try
                              { 
                                isRowEmpty = (string)readDirectoryInfo[accomodation];
                                if (isRowEmpty == "")
                                {
                                    //If it is Null
                                    dataisNull = true;
                                }
                              }
                              catch
                              {
                                  //If it is Null
                                  dataisNull = true;
                              }
                          }
                          if (dataisNull == true)
                          {
                              updateOrInsert = true;
                              dataisNull = false;
                              break;
                          }
                          
                          else
                          {
                              IDinDatabase++;
                          }
                      }

                      readDirectoryInfo.Close();


                      IDinDatabase = IDinDatabase + 1;


                      if(updateOrInsert == true)
                      {
                          //Update the New Data onto the Current Line

                            cmd = new SqlCommand("UPDATE "+City+" SET "+Category+"='"+placeName+ "," +Address+ ", Phone:" +Phone+ ", Email:" +Email+"' WHERE Id='"+IDinDatabase+"'", con);
                            updateOrInsert = false;
                      }
                      else
                      {

                          //Insert the New Data onto the New Line

                          if (Category == "Beverages")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','','','','','','','','','','','','','')", con);
                              updateOrInsert = false;
                          }
                          if(Category == "Cakes")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','','','','','','','','','','','','')", con);
                              updateOrInsert = false;
                          }
                          if (Category == "Cateres")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','','','','','','','','','','','','')", con);
                              updateOrInsert = false;
                          }
                          if (Category == "Churches")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','','','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','','','','','','','','','','')", con);
                              updateOrInsert = false;
                          }
                          if (Category == "CeremonyVenues")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','','','','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','','','','','','','','','')", con);
                              updateOrInsert = false;
                          }
                          if (Category == "BridalCars")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','','','','','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','','','','','','','','')", con);
                              updateOrInsert = false;
                          }
                          if (Category == "BridalShoes")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','','','','','','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','','','','','','','')", con);
                              updateOrInsert = false;
                          }
                          if (Category == "Florists")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','','','','','','','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','','','','','','')", con);
                              updateOrInsert = false;
                          }
                          if (Category == "DanceLessons")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','','','','','','','','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','','','','')", con);
                              updateOrInsert = false;
                          }
                          if (Category == "MensWear")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','','','','','','','','','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','','','','')", con);
                              updateOrInsert = false;
                          }
                          if (Category == "ReceptionVenues")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','','','','','','','','','','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','','','')", con);
                              updateOrInsert = false;
                          }
                          if (Category == "Photographers")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','','','','','','','','','','','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','','')", con);
                              updateOrInsert = false;
                          }
                          if (Category == "LiveMusic")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','','','','','','','','','','','','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ','')", con);
                              updateOrInsert = false;
                          }
                          if (Category == "Accomodation")
                          {
                              cmd = new SqlCommand("Insert into " + City + " values('" + IDinDatabase + "','','','','','','','','','','','','','','" + placeName + " ," + Address + " , Phone: " + Phone + " , Email: " + Email + " ')", con);
                              updateOrInsert = false;
                          }

                      }
                  
                  
                  cmd.ExecuteNonQuery();


                   ViewBag.Status = "Supplier Information Submitted Successful";
                 }

               else
                {
                   ViewBag.Status = "Data Submission Un-successful";
                }

            return View();
        }
 
    }
}