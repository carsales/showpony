﻿// Showpony, a lightweight variant testing library
// Copyright (C) Carsales.com Ltd
// See License.md in the project root for license information.

using System;
using System.Web;

namespace Showpony
{
    internal static class Cookies
    {
        internal static string GetExperimentVariant(HttpRequestBase request, string experiment)
        {
            var cookieName = ShowponyContext.CookiePrefix + experiment;
            if (ShowponyContext.EncryptCookie)
            {
                cookieName = SstCryptographer.Encrypt(cookieName, ShowponyContext.CookieEncryptionPassword);
            }

            cookieName = HttpUtility.UrlEncode(cookieName);

            if (request.Cookies[cookieName] != null &&
                request.Cookies[cookieName].Value != null)
            {
                var variant = request.Cookies[cookieName].Value;
                variant = HttpUtility.UrlDecode(variant);

                if (ShowponyContext.EncryptCookie)
                {
                    variant = SstCryptographer.Decrypt(variant, ShowponyContext.CookieEncryptionPassword);
                }
                return variant;
            }

            return null;
        }

        internal static void SetExperimentVariant(
            HttpRequestBase request, HttpResponseBase response,
            string experiment, string variant)
        {
            var cookieName = ShowponyContext.CookiePrefix + experiment;
            if (ShowponyContext.EncryptCookie)
            {
                cookieName = SstCryptographer.Encrypt(cookieName, ShowponyContext.CookieEncryptionPassword);
                variant = SstCryptographer.Encrypt(variant, ShowponyContext.CookieEncryptionPassword);
            }
            cookieName = HttpUtility.UrlEncode(cookieName);
            variant = HttpUtility.UrlEncode(variant);

            var cookie = new HttpCookie(cookieName, variant)
            {
                Expires = DateTime.Now.AddYears(1)
            };

            request.Cookies.Remove(cookieName);
            request.Cookies.Add(cookie);
            response.Cookies.Remove(cookieName);
            response.Cookies.Add(cookie);
        }

        internal static void DeleteExperimentVariant(HttpResponseBase response, string experiment)
        {
            var cookieName = ShowponyContext.CookiePrefix + experiment;
            if (ShowponyContext.EncryptCookie)
            {
                cookieName = SstCryptographer.Encrypt(cookieName, ShowponyContext.CookieEncryptionPassword);
            }
            cookieName = HttpUtility.UrlEncode(cookieName);

            var cookie = new HttpCookie(cookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            response.Cookies.Add(cookie);
        }
    }
}
