using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.ProfilePicture {
    public class ProfilePictureBindModel {

        public string UrlImg {
            get; set;
        }

        public List<ProfilePictureBindModel> Convert(List<Picture> pictures) {
            List<ProfilePictureBindModel> urls = new List<ProfilePictureBindModel>();

            foreach(Picture picture in pictures) {
                var img = new ProfilePictureBindModel();
                img.UrlImg = picture.Url;
                urls.Add(img);
            }
            return urls;
        }
    }
}