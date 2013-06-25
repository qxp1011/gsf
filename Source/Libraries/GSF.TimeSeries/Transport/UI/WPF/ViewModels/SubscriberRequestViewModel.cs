﻿//******************************************************************************************************
//  SubscriberRequestViewModel.cs - Gbtc
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
//  11/29/2012 - Stephen C. Wills
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using GSF.Collections;
using GSF.Data;
using GSF.IO;
using GSF.PhasorProtocols.UI.DataModels;
using GSF.PhasorProtocols.UI.UserControls;
using GSF.Reflection;
using GSF.Security.Cryptography;
using GSF.TimeSeries.UI;
using GSF.TimeSeries.UI.Commands;
using Microsoft.Win32;
using Random = GSF.Security.Cryptography.Random;

namespace GSF.TimeSeries.Transport.UI.ViewModels
{
    internal class SubscriberRequestViewModel : ViewModelBase
    {
        #region [ Members ]

        // Fields
        private string m_publisherAcronym;
        private string m_publisherName;
        private string m_hostname;
        private int m_port;
        private SecurityMode m_securityMode;

        private string m_localCertificateFile;
        private string m_remoteCertificateFile;
        private bool m_isRemoteCertificateSelfSigned;
        private string m_validPolicyErrors;
        private string m_validChainFlags;

        private string m_sharedKey;
        private string m_identityCertificate;
        private string m_validIPAddresses;

        private ICommand m_localBrowseCommand;
        private ICommand m_remoteBrowseCommand;
        private ICommand m_createCommand;
        private string m_subscriberAcronym;
        private string m_subscriberName;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="SubscriberRequestViewModel"/> class.
        /// </summary>
        public SubscriberRequestViewModel()
        {
            m_port = 6172;
            m_securityMode = SecurityMode.TLS;
            Load();
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the string identifier used to identify the publisher.
        /// </summary>
        public string PublisherAcronym
        {
            get
            {
                return m_publisherAcronym;
            }
            set
            {
                m_publisherAcronym = value;
                OnPropertyChanged("Acronym");
            }
        }

        /// <summary>
        /// Gets or sets the name of the publisher.
        /// </summary>
        public string PublisherName
        {
            get
            {
                return m_publisherName;
            }
            set
            {
                m_publisherName = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the host name or IP address of the server hosting the data publisher.
        /// </summary>
        public string Hostname
        {
            get
            {
                return m_hostname;
            }
            set
            {
                m_hostname = value;
                OnPropertyChanged("Hostname");
            }
        }

        /// <summary>
        /// Gets or sets the port that the data publisher is listening on.
        /// </summary>
        public int Port
        {
            get
            {
                return m_port;
            }
            set
            {
                m_port = value;
                OnPropertyChanged("Port");
            }
        }

        /// <summary>
        /// Gets or sets the security mode used by the data publisher.
        /// </summary>
        public SecurityMode SecurityMode
        {
            get
            {
                return m_securityMode;
            }
            set
            {
                m_securityMode = value;
                OnPropertyChanged("SecurityMode");
                OnPropertyChanged("TransportLayerSecuritySelected");
                OnPropertyChanged("GatewaySecuritySelected");
            }
        }

        /// <summary>
        /// Gets or sets the string identifier used to identify the subscriber.
        /// </summary>
        public string SubscriberAcronym
        {
            get
            {
                return m_subscriberAcronym;
            }
            set
            {
                m_subscriberAcronym = value;
                OnPropertyChanged("SubscriberAcronym");
            }
        }

        /// <summary>
        /// Gets or sets the name of the subscriber.
        /// </summary>
        public string SubscriberName
        {
            get
            {
                return m_subscriberName;
            }
            set
            {
                m_subscriberName = value;
                OnPropertyChanged("SubscriberName");
            }
        }

        /// <summary>
        /// Gets the flag that indicates whether the user has selected TLS as the security mode.
        /// </summary>
        public bool TransportLayerSecuritySelected
        {
            get
            {
                return m_securityMode == SecurityMode.TLS;
            }
        }

        /// <summary>
        /// Gets or sets the path to the local certificate used to identify the subscriber.
        /// </summary>
        public string LocalCertificateFile
        {
            get
            {
                return m_localCertificateFile;
            }
            set
            {
                m_localCertificateFile = value;
                OnPropertyChanged("LocalCertificateFile");
            }
        }

        /// <summary>
        /// Gets or sets the path to the remote certificate used to identify the publisher.
        /// </summary>
        public string RemoteCertificateFile
        {
            get
            {
                return m_remoteCertificateFile;
            }
            set
            {
                m_remoteCertificateFile = value;
                OnPropertyChanged("RemoteCertificateFile");
            }
        }

        /// <summary>
        /// Gets or sets the flag that indicates whether the remote certificate is a self-signed certificate.
        /// </summary>
        public bool RemoteCertificateIsSelfSigned
        {
            get
            {
                return m_isRemoteCertificateSelfSigned;
            }
            set
            {
                m_isRemoteCertificateSelfSigned = value;
                OnPropertyChanged("SelfSigned");
            }
        }

        /// <summary>
        /// Gets or sets the list of valid policy errors that can occur during remote certificate validation.
        /// </summary>
        public string ValidPolicyErrors
        {
            get
            {
                return m_validPolicyErrors;
            }
            set
            {
                m_validPolicyErrors = value;
                OnPropertyChanged("ValidPolicyErrors");
            }
        }

        /// <summary>
        /// Gets or sets the list of valid chain flags which can be set during remote certificate validation.
        /// </summary>
        public string ValidChainFlags
        {
            get
            {
                return m_validChainFlags;
            }
            set
            {
                m_validChainFlags = value;
                OnPropertyChanged("ValidChainFlags");
            }
        }

        /// <summary>
        /// Gets the flag that indicates whether the user has selected Gateway security.
        /// </summary>
        public bool GatewaySecuritySelected
        {
            get
            {
                return m_securityMode == SecurityMode.Gateway;
            }
        }

        /// <summary>
        /// Gets or sets the shared key sent to the publisher for Gateway security encryption.
        /// </summary>
        public string SharedKey
        {
            get
            {
                return m_sharedKey;
            }
            set
            {
                m_sharedKey = value;
                OnPropertyChanged("SharedKey");
            }
        }

        /// <summary>
        /// Gets or sets the identity certificate exchanged during Gateway security authentication.
        /// </summary>
        public string IdentityCertificate
        {
            get
            {
                return m_identityCertificate;
            }
            set
            {
                m_identityCertificate = value;
                OnPropertyChanged("IdentityCertificate");
            }
        }

        /// <summary>
        /// Gets or sets the set of valid IP addresses used by the publisher in order to validate the subscriber's identity.
        /// </summary>
        public string ValidIPAddresses
        {
            get
            {
                return m_validIPAddresses;
            }
            set
            {
                m_validIPAddresses = value;
                OnPropertyChanged("ValidIPAddresses");
            }
        }

        /// <summary>
        /// Gets the command that executes when the user chooses to browse for a local certificate.
        /// </summary>
        public ICommand LocalBrowseCommand
        {
            get
            {
                if ((object)m_localBrowseCommand == null)
                    m_localBrowseCommand = new RelayCommand(BrowseLocalCertificateFile, () => true);

                return m_localBrowseCommand;
            }
        }

        /// <summary>
        /// Gets the command that executes when the user chooses to browse for a remote certificate.
        /// </summary>
        public ICommand RemoteBrowseCommand
        {
            get
            {
                if ((object)m_remoteBrowseCommand == null)
                    m_remoteBrowseCommand = new RelayCommand(BrowseRemoteCertificateFile, () => true);

                return m_remoteBrowseCommand;
            }
        }

        /// <summary>
        /// Gets the command that executes when the user chooses to create the authentication request.
        /// </summary>
        public ICommand CreateCommand
        {
            get
            {
                if ((object)m_createCommand == null)
                    m_createCommand = new RelayCommand(CreateAuthenticationRequest, () => true);

                return m_createCommand;
            }
        }

        #endregion

        #region [ Methods ]

        private void Load()
        {
            string companyAcronym;

            // Try to populate defaults for subscriber acronym and name
            // using company information from the openPDC configuration file
            if (TryGetCompanyAcronym(out companyAcronym))
            {
                SubscriberAcronym = companyAcronym;
                SubscriberName = string.Format("{0} Subscription Authorization", companyAcronym);
            }

            // Connect to database to retrieve company information for current node
            using (AdoDataConnection database = new AdoDataConnection(CommonFunctions.DefaultSettingsCategory))
            {
                try
                {
                    string query = database.ParameterizedQueryString("SELECT Company.Acronym, Company.Name FROM Company, Node WHERE Company.ID = Node.CompanyID AND Node.ID = {0}", "id");
                    DataRow row = database.Connection.RetrieveRow(database.AdapterType, query, database.CurrentNodeID());

                    PublisherAcronym = row.Field<string>("Acronym");
                    PublisherName = row.Field<string>("Name");

                    // Generate a default shared secret password for subscriber key and initialization vector
                    byte[] buffer = new byte[4];
                    Random.GetBytes(buffer);

                    string generatedSecret = Convert.ToBase64String(buffer).RemoveCrLfs();

                    if (generatedSecret.Contains("="))
                        generatedSecret = generatedSecret.Split('=')[0];

                    SharedKey = generatedSecret;

                    // Generate an identity for this subscriber
                    AesManaged sa = new AesManaged();
                    sa.GenerateKey();
                    IdentityCertificate = Convert.ToBase64String(sa.Key);

                    // Generate valid local IP addresses for this connection
                    IEnumerable<IPAddress> addresses = Dns.GetHostAddresses(Dns.GetHostName()).OrderBy(key => key.AddressFamily);
                    ValidIPAddresses = addresses.ToDelimitedString("; ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, "Subscriber Request", MessageBoxButton.OK);
                }

                try
                {
                    Dictionary<string, string> settings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                    IPAddress[] hostIPs = null;
                    IEnumerable<IPAddress> localIPs;

                    settings = database.ServiceConnectionString().ParseKeyValuePairs();
                    localIPs = Dns.GetHostAddresses("localhost").Concat(Dns.GetHostAddresses(Dns.GetHostName()));

                    if (settings.ContainsKey("server"))
                        hostIPs = Dns.GetHostAddresses(settings["server"].Split(':')[0]);

                    // Check to see if entered host name corresponds to a local IP address
                    if (hostIPs == null)
                        MessageBox.Show("Failed to find service host address. Secure key exchange may not succeed." + Environment.NewLine + "Please make sure to run manager application with administrative privileges on the server where service is hosted.", "Subscription Request", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else if (!hostIPs.Any(ip => localIPs.Contains(ip)))
                        MessageBox.Show("Secure key exchange may not succeed." + Environment.NewLine + "Please make sure to run manager application with administrative privileges on the server where service is hosted.", "Subscription Request", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch
                {
                    MessageBox.Show("Please make sure to run manager application with administrative privileges on the server where service is hosted.", "Subscription Request", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void BrowseLocalCertificateFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.FileName = LocalCertificateFile;
            fileDialog.DefaultExt = ".cer";
            fileDialog.Filter = "Certificate files|*.cer|All Files|*.*";

            if (fileDialog.ShowDialog() == true)
                LocalCertificateFile = fileDialog.FileName;
        }

        private void BrowseRemoteCertificateFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.FileName = RemoteCertificateFile;
            fileDialog.DefaultExt = ".cer";
            fileDialog.Filter = "Certificate files|*.cer|All Files|*.*";

            if (fileDialog.ShowDialog() == true)
                RemoteCertificateFile = fileDialog.FileName;
        }

        private void CreateAuthenticationRequest()
        {
            try
            {
                ExportAuthorizationRequest();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Popup(ex.Message + Environment.NewLine + "Inner Exception: " + ex.InnerException.Message, "Subscription Request Exception:", MessageBoxImage.Error);
                    CommonFunctions.LogException(null, "Subscription Request", ex.InnerException);
                }
                else
                {
                    Popup(ex.Message, "Subscription Request Exception:", MessageBoxImage.Error);
                    CommonFunctions.LogException(null, "Subscription Request", ex);
                }
            }
        }

        // Export the authorization request.
        private void ExportAuthorizationRequest()
        {
            const string messageFormat = "Data subscription adapter \"{0}\" already exists. Unable to create subscription request.";

            Device device;

            if (!string.IsNullOrWhiteSpace(PublisherAcronym))
            {
                // Check if the device already exists
                device = GetDeviceByAcronym(PublisherAcronym.Replace(" ", ""));

                if ((object)device != null)
                    throw new Exception(string.Format(messageFormat, device.Acronym));

                // Save the associated device
                if (TryCreateRequest())
                    SaveDevice();
            }
            else
            {
                MessageBox.Show("Acronym is a required field. Please provide value.");
            }
        }

        private bool TryCreateRequest()
        {
            try
            {
                // Generate authorization request
                SaveFileDialog saveFileDialog;
                Stream requestStream;

                AuthenticationRequest request;
                string[] keyIV = null;

                saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = ".srq";
                saveFileDialog.Filter = "Subscription Requests|*.srq|All Files|*.*";

                if (saveFileDialog.ShowDialog() == true)
                {
                    request = new AuthenticationRequest();

                    // Set up the request
                    request.Acronym = SubscriberAcronym;
                    request.Name = SubscriberName;
                    request.ValidIPAddresses = ValidIPAddresses;

                    // Cipher key only applies to Gateway security
                    if (SecurityMode == SecurityMode.Gateway)
                    {
                        // Export cipher key to common crypto cache
                        if (!ExportCipherKey(SharedKey, 256))
                            throw new Exception("Failed to export cipher keys from common key cache.");

                        // Reload local crypto cache and get key and IV
                        // that go into the authentication request
                        Cipher.ReloadCache();
                        keyIV = Cipher.ExportKeyIV(SharedKey, 256).Split('|');

                        // Set up crypto settings in the request
                        request.SharedSecret = SharedKey;
                        request.AuthenticationID = IdentityCertificate;
                        request.Key = keyIV[0];
                        request.IV = keyIV[1];
                    }

                    // Certificate only applies to TLS security
                    if (SecurityMode == SecurityMode.TLS)
                        request.CertificateFile = File.ReadAllBytes(LocalCertificateFile);

                    // Create the request
                    using (requestStream = File.OpenWrite(saveFileDialog.FileName))
                    {
                        Serialization.Serialize(request, SerializationFormat.Binary, ref requestStream);
                    }

                    // Send ReloadCryptoCache to service
                    if (SecurityMode == SecurityMode.Gateway)
                        ReloadServiceCryptoCache();

                    return true;
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("Error creating authorization request: {0}", ex.Message);
                Popup(message, "Subscription Request Error", MessageBoxImage.Error);
                CommonFunctions.LogException(null, "Subscription Request", ex);
            }

            return false;
        }

        // Gets the device from the database with the given acronym for the currently selected node.
        private Device GetDeviceByAcronym(string acronym)
        {
            AdoDataConnection database = null;
            string nodeID;

            try
            {
                database = new AdoDataConnection(CommonFunctions.DefaultSettingsCategory);
                nodeID = database.CurrentNodeID().ToString();
                return Device.GetDevice(database, string.Format(" WHERE NodeID = '{0}' AND Acronym = '{1}'", nodeID, acronym));
            }
            finally
            {
                if ((object)database != null)
                    database.Dispose();
            }
        }

        // Attempt to get the company acronym stored in the the configuration file.
        private bool TryGetCompanyAcronym(out string acronym)
        {
            XDocument document;

            // Initial value if acronym
            // never gets set explicitly
            acronym = null;

            try
            {
                // Check all application config files for company info
                foreach (string configFilePath in FilePath.GetFileList(FilePath.GetAbsolutePath("*.exe.config")))
                {
                    try
                    {
                        // Load the configuration file and search for CompanyAcronym
                        document = XDocument.Load(configFilePath);

                        if ((object)document.Root != null)
                        {
                            acronym = document.Root
                                .Descendants("systemSettings")
                                .Elements("add")
                                .Where(e => (string)e.Attribute("name") == "CompanyAcronym")
                                .Select(e => (string)e.Attribute("value"))
                                .SingleOrDefault();
                        }
                    }
                    catch
                    {
                        // Ignore exceptions here - simply check the next config file
                    }

                    if ((object)acronym != null)
                        break;
                }

                // Indicate success or failure
                return ((object)acronym != null);
            }
            catch
            {
                // Company info retrieval failed
                return false;
            }
        }

        // Exports the given cipher key from the common key cache.
        private bool ExportCipherKey(string password, int keySize)
        {
            ProcessStartInfo configCrypterInfo = new ProcessStartInfo();
            Process configCrypter;

            configCrypterInfo.FileName = FilePath.GetAbsolutePath("ConfigCrypter.exe");
            configCrypterInfo.Arguments = string.Format("-password {0} -keySize {1}", password, keySize);
            configCrypterInfo.CreateNoWindow = true;

            configCrypter = Process.Start(configCrypterInfo);
            configCrypter.WaitForExit();

            return configCrypter.ExitCode == 0;
        }

        // Send service command to reload crypto cache.
        private void ReloadServiceCryptoCache()
        {
            try
            {
                CommonFunctions.SendCommandToService("ReloadCryptoCache");
            }
            catch (Exception ex)
            {
                string message = "Unable to notify service about updated crypto cache:" + Environment.NewLine;

                if (ex.InnerException != null)
                {
                    message += ex.Message + Environment.NewLine;
                    message += "Inner Exception: " + ex.InnerException.Message;
                    Popup(message, "Subscription Request Exception:", MessageBoxImage.Information);
                    CommonFunctions.LogException(null, "Subscription Request", ex.InnerException);
                }
                else
                {
                    message += ex.Message;
                    Popup(message, "Subscription Request Exception:", MessageBoxImage.Information);
                    CommonFunctions.LogException(null, "Subscription Request", ex);
                }
            }
        }

        // Associate the given device with the
        // authorization request and save it.
        private void SaveDevice()
        {
            const string connectionStringFormat = "interface=0.0.0.0; compression=false; autoConnect=true; securityMode={0}; " +
                "localport=6175; transportprotocol=udp; commandChannel={{server={1}:{2}; interface=0.0.0.0}}; {3}";

            Device device;
            DeviceUserControl deviceUserControl;

            SslPolicyErrors validPolicyErrors;
            X509ChainStatusFlags validChainFlags;
            string securitySpecificSettings = string.Empty;

            if (SecurityMode == SecurityMode.Gateway)
            {
                securitySpecificSettings = string.Format("sharedSecret={0}; authenticationID={{{1}}}", SharedKey, IdentityCertificate);
            }
            else if (SecurityMode == SecurityMode.TLS)
            {
                if (!Enum.TryParse(ValidPolicyErrors, out validPolicyErrors))
                    validPolicyErrors = SslPolicyErrors.None;

                if (!Enum.TryParse(ValidChainFlags, out validChainFlags))
                    validChainFlags = X509ChainStatusFlags.NoError;

                if (RemoteCertificateIsSelfSigned)
                {
                    validPolicyErrors |= SslPolicyErrors.RemoteCertificateChainErrors;
                    validChainFlags |= X509ChainStatusFlags.UntrustedRoot;
                }

                securitySpecificSettings = string.Format("localCertificate={0}; remoteCertificate={1}; validPolicyErrors={2}; validChainFlags={3}",
                    LocalCertificateFile, RemoteCertificateFile, validPolicyErrors, validChainFlags);
            }

            device = new Device();
            device.Acronym = PublisherAcronym.Replace(" ", "");
            device.Name = PublisherName;
            device.Enabled = false;
            device.IsConcentrator = true;
            device.ProtocolID = GetGatewayProtocolID();
            device.ConnectionString = string.Format(connectionStringFormat, SecurityMode, Hostname, Port, securitySpecificSettings);

            Device.Save(null, device);

            device = Device.GetDevice(null, "WHERE Acronym = '" + device.Acronym + "'");
            deviceUserControl = new DeviceUserControl(device);
            CommonFunctions.LoadUserControl(deviceUserControl, "Manage Device Configuration");
        }

        // Get the Gateway Transport protocol ID by querying the database.
        private int? GetGatewayProtocolID()
        {
            const string query = "SELECT ID FROM Protocol WHERE Acronym = 'GatewayTransport'";
            AdoDataConnection database = null;
            object queryResult;

            try
            {
                database = new AdoDataConnection(CommonFunctions.DefaultSettingsCategory);
                queryResult = database.Connection.ExecuteScalar(query);
                return (queryResult != null) ? Convert.ToInt32(queryResult) : 8;
            }
            finally
            {
                if ((object)database != null)
                    database.Dispose();
            }
        }

        #endregion
    }
}
