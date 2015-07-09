/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.Messaging.Email.Helpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace MixERP.Net.Messaging.Email
{
    public sealed class Processor
    {
        public string Catalog { get; set; }

        public Processor(string catalog)
        {
            this.Catalog = catalog;
        }

        public void Send(string sendTo, string subject, string body, IEnumerable<Attachment> attachments)
        {
            Config config = new Config(this.Catalog);

            EmailMessage email = new EmailMessage
            {
                FromName = config.FromName,
                FromEmail = config.FromEmail,
                Subject = subject,
                SentTo = sendTo,
                Message = body,
                Type = Type.Outward,
                EventDateUTC = DateTime.UtcNow,
                Status = Status.Unknown
            };

            SmtpHost host = new SmtpHost
            {
                Address = config.SmtpHost,
                Port = config.SmtpPort,
                EnableSSL = config.EnableSsl,
                DeliveryMethod = config.DeliveryMethod,
                PickupDirectory = config.PickupDirectory
            };

            ICredentials credentials = new SmtpCredentials
            {
                Username = config.SmtpUsername,
                Password = config.SmtpUserPassword
            };

            this.Send(email, host, credentials, attachments);
        }

        public void Send(EmailMessage email, SmtpHost host, ICredentials credentials,
            IEnumerable<Attachment> attachments)
        {
            if (string.IsNullOrWhiteSpace(email.SentTo))
            {
                throw new ArgumentNullException(email.SentTo);
            }

            if (string.IsNullOrWhiteSpace(email.Message))
            {
                throw new ArgumentNullException(email.Message);
            }

            string[] addresses = email.SentTo.Split(',');
            foreach (string address in addresses)
            {
                Validator validator = new Validator(address);
                validator.Validate();

                if (!validator.IsValid)
                {
                    return;
                }
            }

            addresses = addresses.Distinct().ToArray();
            email.SentTo = string.Join(",", addresses);
            email.Status = Status.Executing;

            ThreadPool.QueueUserWorkItem(callback =>
            {
                MailAddress sender = new MailAddress(email.FromEmail, email.FromName);

                using (MailMessage mail = new MailMessage(email.FromEmail, email.SentTo))
                {
                    foreach (var attachment in attachments)
                    {
                        mail.Attachments.Add(attachment);
                    }

                    mail.From = sender;
                    using (SmtpClient smtp = new SmtpClient(host.Address, host.Port))
                    {
                        smtp.DeliveryMethod = host.DeliveryMethod;
                        smtp.PickupDirectoryLocation = host.PickupDirectory;

                        smtp.EnableSsl = host.EnableSSL;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(credentials.Username, credentials.Password);
                        try
                        {
                            mail.Subject = email.Subject;
                            mail.Body = email.Message;
                            mail.IsBodyHtml = true;
                            mail.SubjectEncoding = Encoding.UTF8;
                            email.Status = Status.Completed;

                            smtp.Send(mail);
                        }
                        catch (SmtpException ex)
                        {
                            email.Status = Status.Failed;
                            Log.Warning(@"Could not send email to {To}. {Ex}. ", email.SentTo, ex);
                        }
                    }
                }
            });
        }
    }
}