﻿//  ---------------------------------------------------------------------------
//  <copyright file="TDSLogin7OptionFlags3.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
//  ---------------------------------------------------------------------------

namespace TDSClient.TDS.Login7
{
    using System;
    using System.IO;
    using TDSClient.TDS.Interfaces;

    public enum TDSLogin7OptionFlags3ChangePassword : byte
    {
        /// <summary>
        /// Change Password Not Requested
        /// </summary>
        NoChangeRequest,

        /// <summary>
        /// Change Password Requested
        /// </summary>
        RequestChange
    }

    public enum TDSLogin7OptionFlags3SendYukonBinaryXML : byte
    {
        /// <summary>
        /// Send Yukon Binary XML Off
        /// </summary>
        Off,

        /// <summary>
        /// Send Yukon Binary XML On
        /// </summary>
        On
    }

    public enum TDSLogin7OptionFlags3UserInstanceProcess : byte
    {
        /// <summary>
        /// Don't request separate user instance process
        /// </summary>
        DontRequestSeparateProcess,

        /// <summary>
        /// Request separate user instance process
        /// </summary>
        RequestSeparateProcess
    }

    public enum TDSLogin7OptionFlags3UnknownCollationHandling : byte
    {
        /// <summary>
        /// Unknown collation handling off
        /// </summary>
        Off,

        /// <summary>
        /// Unknown collation handling on
        /// </summary>
        On
    }

    public enum TDSLogin7OptionFlags3Extension : byte
    {
        /// <summary>
        /// Extension doesn't exist
        /// </summary>
        DoesntExist,

        /// <summary>
        /// Extensions exist and are specified
        /// </summary>
        Exists
    }

    public class TDSLogin7OptionFlags3 : IPackageable
    {
        /// <summary>
        ///  Gets or sets the ChangePassword Flag.
        ///  Specifies whether the login request SHOULD change password.
        /// </summary>
        public TDSLogin7OptionFlags3ChangePassword ChangePassword { get; set; }

        /// <summary>
        /// Gets or sets the SendYukonBinaryXML Flag.
        /// On if XML data type instances are returned as binary XML.
        /// </summary>
        public TDSLogin7OptionFlags3SendYukonBinaryXML SendYukonBinaryXML { get; set; }

        /// <summary>
        /// Gets or sets the UserInstanceProcess Flag.
        /// On if client is requesting separate process to be spawned as user instance.
        /// </summary>
        public TDSLogin7OptionFlags3UserInstanceProcess UserInstanceProcess { get; set; }

        /// <summary>
        /// Gets or sets the UnknownCollationHandling Flag.
        /// This bit is used by the server to determine if a client is able to
        /// properly handle collations introduced after TDS 7.2. 
        /// </summary>
        public TDSLogin7OptionFlags3UnknownCollationHandling UnknownCollationHandling { get; set; }

        /// <summary>
        /// Gets or sets the Extension Flag.
        /// Specifies whether IBExtension or CBExtension fields are used.
        /// </summary>
        public TDSLogin7OptionFlags3Extension Extension { get; set; }

        public void Pack(MemoryStream stream)
        {
            byte packedByte = (byte)((byte)this.ChangePassword
                | ((byte)this.UserInstanceProcess << 1)
                | ((byte)this.SendYukonBinaryXML << 2)
                | ((byte)this.UnknownCollationHandling << 3)
                | ((byte)this.Extension << 4));

            stream.WriteByte(packedByte);
        }

        public bool Unpack(MemoryStream stream)
        {
            byte flagByte = Convert.ToByte(stream.ReadByte());

            this.ChangePassword = (TDSLogin7OptionFlags3ChangePassword)(flagByte & 0x01);
            this.UserInstanceProcess = (TDSLogin7OptionFlags3UserInstanceProcess)((flagByte >> 1) & 0x01);
            this.SendYukonBinaryXML = (TDSLogin7OptionFlags3SendYukonBinaryXML)((flagByte >> 2) & 0x01);
            this.UnknownCollationHandling = (TDSLogin7OptionFlags3UnknownCollationHandling)((flagByte >> 3) & 0x01);
            this.Extension = (TDSLogin7OptionFlags3Extension)((flagByte >> 4) & 0x01);
            
            return true;
        }
    }
}