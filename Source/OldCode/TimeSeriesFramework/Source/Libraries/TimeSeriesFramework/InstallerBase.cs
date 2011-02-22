﻿//******************************************************************************************************
//  InstallerBase.cs - Gbtc
//
//  Copyright © 2010, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/eclipse-1.0.php
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  09/22/2010 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using TVA.IO;
using TVA.Identity;

namespace TimeSeriesFramework
{
    /// <summary>
    /// Defines a common installer class for the applications based on the time-series framework.
    /// </summary>
    /// <remarks>
    /// Users may choose to only install service application or its management tools, in either case common
    /// installation steps need to occur, so these steps are managed in this one common base class.
    /// </remarks>
    [RunInstaller(true)]
    public partial class InstallerBase : Installer
    {
        #region [ Properties ]

        /// <summary>
        /// Gets associated application configuration file name.
        /// </summary>
        protected virtual string ConfigurationName
        {
            get
            {
                return null;
            }
        }

        ///// <summary>
        ///// Gets the configuration setup utility file name.
        ///// </summary>
        //protected virtual string SetupUtilityName
        //{
        //    get
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// Gets or sets flag to run ConfigurationSetupUtility after installation.
        ///// </summary>
        //protected bool RunSetupUtility
        //{
        //    get;
        //    set;
        //}

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Called when system settings are loaded.
        /// </summary>
        /// <param name="configurationFile">Open xml document containing configuration settings.</param>
        /// <param name="systemSettingsNode">Xml node containing system settings.</param>
        [SuppressMessage("Microsoft.Design", "CA1059")]
        protected virtual void OnSystemSettingsLoaded(XmlDocument configurationFile, XmlNode systemSettingsNode)
        {
        }

        /// <summary>
        /// Installs the class.
        /// </summary>
        /// <param name="stateSaver">Current state information.</param>
        [SuppressMessage("Microsoft.Security", "CA2122"), SuppressMessage("Microsoft.Globalization", "CA1300")]
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            try
            {
                string targetDir = FilePath.AddPathSuffix(Context.Parameters["DP_TargetDir"]).Replace("\\\\", "\\");
                string installedBitSize = "32bit";

                if (!string.IsNullOrEmpty(ConfigurationName))
                {
                    // Open the configuration file as an XML document.
                    string configFilePath = targetDir + ConfigurationName;

                    if (File.Exists(configFilePath))
                    {
                        XmlDocument configurationFile = new XmlDocument();
                        configurationFile.Load(configFilePath);
                        XmlNode systemSettingsNode = configurationFile.SelectSingleNode("configuration/categorizedSettings/systemSettings");

                        if (systemSettingsNode != null)
                        {
                            // Allow user to add or update custom configuration settings if desired
                            OnSystemSettingsLoaded(configurationFile, systemSettingsNode);

                            // Lookup installed bit size in configuration file, if defined
                            XmlNode installedBitSizeNode = systemSettingsNode.SelectSingleNode("add[@name = 'InstalledBitSize']");

                            if (installedBitSizeNode != null)
                            {
                                installedBitSize = installedBitSizeNode.Attributes["value"].Value;

                                // Default to 32 if no target installation bit size was found
                                if (string.IsNullOrWhiteSpace(installedBitSize))
                                    installedBitSize = "32";

                                installedBitSize += "bit";
                            }
                        }

                        // Save any updates to configuration file
                        configurationFile.Save(configFilePath);
                    }
                }

                //if (RunSetupUtility && !string.IsNullOrEmpty(SetupUtilityName))
                //{
                //    // Run configuration setup utility
                //    Process configurationSetup = null;
                //    string fileName = targetDir + SetupUtilityName;
                //    string arguments = "-install -" + installedBitSize;

                //    try
                //    {
                //        configurationSetup = UserAccountControl.CreateProcessAsAdmin(fileName, arguments);
                //        //if (UserAccountControl.IsUacEnabled)
                //        //{
                //        //    configurationSetup = UserAccountControl.CreateProcessAsStandardUser(fileName, arguments);
                //        //}
                //        //else
                //        //{
                //            //configurationSetup = new Process();
                //            //configurationSetup.StartInfo.FileName = fileName;
                //            //configurationSetup.StartInfo.Arguments = arguments;
                //            //configurationSetup.StartInfo.WorkingDirectory = targetDir;
                //            //configurationSetup.StartInfo.UseShellExecute = false;
                //            //configurationSetup.StartInfo.CreateNoWindow = true;
                //            //configurationSetup.Start();
                //        //}

                //        configurationSetup.WaitForExit();
                //    }
                //    finally
                //    {
                //        if (configurationSetup != null)
                //            configurationSetup.Close();
                //    }
                //}
            }
            catch (Exception ex)
            {
                // Not failing install if we can't perform these steps...
                MessageBox.Show("There was an exception detected during the install process: " + ex.Message);
            }
        }

        #endregion
    }
}
