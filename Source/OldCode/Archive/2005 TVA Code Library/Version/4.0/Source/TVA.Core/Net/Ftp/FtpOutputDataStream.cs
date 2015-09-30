﻿//*******************************************************************************************************
//  OutputDataStream.cs
//  Copyright © 2008 - TVA, all rights reserved - Gbtc
//
//  Build Environment: C#, Visual Studio 2008
//  Primary Developer: James R. Carroll
//      Office: PSO TRAN & REL, CHATTANOOGA - MR 2W-C
//       Phone: 423/751-2827
//       Email: jrcarrol@tva.gov
//
//  Code Modification History:
//  -----------------------------------------------------------------------------------------------------
//  09/23/2008 - James R. Carroll
//       Generated original version of source code.
//  08/06/2009 - Josh Patterson
//      Edited Comments
//
//*******************************************************************************************************

using System;
using System.Net.Sockets;

namespace TVA.Net.Ftp
{
    /// <summary>
    /// Defines a FTP data output stream for remote files.
    /// </summary>
    public class FtpOutputDataStream : FtpDataStream
    {
        internal FtpOutputDataStream(FtpControlChannel ctrl, TcpClient client)
            : base(ctrl, client)
        {
        }

        /// <summary>
        /// Cannot read from output stream, method is not supported.
        /// </summary>
        /// <exception cref="NotSupportedException">Cannot read from output stream.</exception>
        /// <param name="buffer">A <see cref="Byte"/> array buffer.</param>
        /// <param name="count">An <see cref="Int32"/> offset into the stream to read from.</param>
        /// <param name="offset">An <see cref="Int32"/> number of bytes to read.</param>
        /// <returns>An <see cref="Int32"/> number of bytes read.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Cannot read from output stream, method is not supported.
        /// </summary>
        /// <exception cref="NotSupportedException">Cannot read from output stream.</exception>
        /// <returns>An <see cref="Int32"/> number of bytes read.</returns>
        public override int ReadByte()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns false, cannot read from output stream.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return false;
            }
        }
    }
}