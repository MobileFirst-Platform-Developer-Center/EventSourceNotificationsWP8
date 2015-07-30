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
using Microsoft.Phone.Controls;
using IBM.Worklight;

namespace EventSourceNotificationsWP8
{
    class MyResponseListener : WLResponseListener
    {
        private string operation;

        public MyResponseListener(string op)
        {
            operation = op;
        }

        public void onFailure(WLFailResponse response)
        {
            string responseTxt = response.getResponseText();
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MainPage._this.AddTextToReceivedTextBlock(operation + "Failed!\n" + responseTxt);
                MainPage._this.PanoramaControl.SetValue(Panorama.SelectedItemProperty, MainPage._this.Console);
                MainPage._this.Measure(new Size());
            });
        }

        public void onSuccess(WLResponse response)
        {
            string responseTxt = response.getResponseJSON().ToString();
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MainPage._this.AddTextToReceivedTextBlock(operation + "Success\n" + responseTxt);
                MainPage._this.PanoramaControl.SetValue(Panorama.SelectedItemProperty, MainPage._this.Console);
                MainPage._this.Measure(new Size());
            });
        }
    }
}
