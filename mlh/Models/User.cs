using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace mlh.Models
{
    public class User
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string availability { get; set; }

        public string phone { get; set; }
        public string email { get; set; }

        public string requests { get; set; }
        public string services { get; set; }

        public string tutors { get; set; }
        public string tutees { get; set; }

        public object getpreview(){
            List<string> req = new List<string>();
            List<string> ser = new List<string>();
            List<KeyValuePair<Guid, Guid>> tut = new List<KeyValuePair<Guid, Guid>>();
            List<KeyValuePair<Guid, Guid>> tute = new List<KeyValuePair<Guid, Guid>> ();

            if (!string.IsNullOrEmpty(requests))
            {
                req = JsonConvert.DeserializeObject<List<string>>(requests);
            }
            if (!string.IsNullOrEmpty(services))
            {
                ser = JsonConvert.DeserializeObject<List<string>>(services);
            }
            if (!string.IsNullOrEmpty(tutors))
            {
                tut = JsonConvert.DeserializeObject<List<KeyValuePair<Guid, Guid>>>(tutors);
            }
            if (!string.IsNullOrEmpty(tutees))
            {
                tute = JsonConvert.DeserializeObject<List<KeyValuePair<Guid, Guid>>>(tutees);
            }

            return new 
            {
                id = id,
                availability = availability,
                username = username,
                password = password,
                requests = req,
                services = ser,
                tutors = tut,
                tutees = tute
            };
        }
        public void addservice(string courseid){
            List<string> ser = new List<string>();
            if (!string.IsNullOrEmpty(services))
            {
                ser = JsonConvert.DeserializeObject<List<string>>(services);
            }
            ser.Add(courseid);
        }

        public void addtutor(string userid,string courseid){
            List<KeyValuePair<string, string>> tut=new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(tutors))
            {
                tut = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(tutors);
            }
            tut.Add(new KeyValuePair<string,string>(userid, courseid));
            tutors = JsonConvert.SerializeObject(tut);
        }
        public void addtutees(string userid, string courseid)
        {
            List<KeyValuePair<string, string>> tut = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(tutees))
            {
                tut = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(tutors);
            }
            tut.Add(new KeyValuePair<string, string>(userid, courseid));
            tutees = JsonConvert.SerializeObject(tut);
        }
        public void removetutor(string userid, string courseid)
        {
            List<KeyValuePair<string, string>> tut = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(tutors))
            {
                tut = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(tutors);
            }
            tut.Remove(tut.Where(x=>x.Key==userid&&x.Value==courseid).FirstOrDefault());
            tutors = JsonConvert.SerializeObject(tut);
        }
        public void removetutee(string userid, string courseid)
        {
            List<KeyValuePair<string, string>> tut = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(tutees))
            {
                tut = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(tutors);
            }
            tut.Remove(tut.Where(x => x.Key == userid && x.Value == courseid).FirstOrDefault());
            tutors = JsonConvert.SerializeObject(tut);
        }
    }
}
