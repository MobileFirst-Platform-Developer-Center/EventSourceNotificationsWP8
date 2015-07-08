/*
 * COPYRIGHT LICENSE: This information contains sample code provided in source code form. You may copy, modify, and distribute
 * these sample programs in any form without payment to IBM® for the purposes of developing, using, marketing or distributing
 * application programs conforming to the application programming interface for the operating platform for which the sample code is written.
 * Notwithstanding anything to the contrary, IBM PROVIDES THE SAMPLE SOURCE CODE ON AN "AS IS" BASIS AND IBM DISCLAIMS ALL WARRANTIES,
 * EXPRESS OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, ANY IMPLIED WARRANTIES OR CONDITIONS OF MERCHANTABILITY, SATISFACTORY QUALITY,
 * FITNESS FOR A PARTICULAR PURPOSE, TITLE, AND ANY WARRANTY OR CONDITION OF NON-INFRINGEMENT. IBM SHALL NOT BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR OPERATION OF THE SAMPLE SOURCE CODE.
 * IBM HAS NO OBLIGATION TO PROVIDE MAINTENANCE, SUPPORT, UPDATES, ENHANCEMENTS OR MODIFICATIONS TO THE SAMPLE SOURCE CODE.
 */

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using IBM.Worklight;

namespace EventSourceNotificationsWP8
{
    class WindowsChallengeHandler : ChallengeHandler
    {
        public WindowsChallengeHandler()
            : base("SampleAppRealm")
        {

        }

        public override void handleChallenge(JObject response)
        {
            System.Diagnostics.Debug.WriteLine("Handling Challenge\n");
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MainPage._this.NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
            });
        }

        public override bool isCustomResponse(WLResponse response)
        {
            if (response == null || response.getResponseText() == null ||
                !response.getResponseText().Contains("j_security_check"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void submit(string username, string password)
        {
            Dictionary<String, String> parms = new Dictionary<String, String>();
            parms.Add("j_username", username);
            parms.Add("j_password", password);

            submitLoginForm("j_security_check", parms, null, 10000, "post");
        }

        public override void onFailure(WLFailResponse response)
        {
            submitFailure(response);
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MainPage._this.NavigationService.GoBack();
            });
        }

        public override void onSuccess(WLResponse response)
        {
            submitSuccess(response);
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MainPage._this.NavigationService.GoBack();
            });
        }
    }
}
