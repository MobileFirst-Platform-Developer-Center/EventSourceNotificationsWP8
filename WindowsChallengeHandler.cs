/**
* Copyright 2015 IBM Corp.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
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
